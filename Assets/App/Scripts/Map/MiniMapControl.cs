//
// MiniMapControl.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.02
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using MessagePipe;
using UniRx;

using Zenject;

namespace Ling.Map
{
	public interface IMiniMapView
	{
		void Setup();

		void DeployView(GameObject obj, MiniMapPointObject point);

		void SetActive(bool isActive);

		void SetTileDataMap(Map.TileDataMap tileDataMap);
	}

	/// <summary>
	/// View一つの情報
	/// </summary>
	[System.Serializable]
	public class MiniMapModel
	{
		public int MapLevel;
		public BoolReactiveProperty Used { get; } = new BoolReactiveProperty();
		public IMiniMapView View { get; private set; }

		/// <summary>
		/// ミニマップ上のオブジェクトと１対１に紐付ける
		/// </summary>
		public Dictionary<GameObject, MiniMapPointObject> PointDict { get; } = new Dictionary<GameObject, MiniMapPointObject>();


		public void Setup(IMiniMapView view)
		{
			View = view;

			// 未使用になったら強制非表示
			Used.Where(isOn => !isOn)
				.Subscribe(isOn => 
				{
					View.SetActive(isOn);
				});
		}

		/// <summary>
		/// オブジェクトを追加する
		/// </summary>
		public void AddObject(GameObject followObj, MiniMapPointObject point)
		{
			PointDict.Add(followObj, point);
			View.DeployView(followObj, point);

			point.SetFollowObject(followObj);
		}

		/// <summary>
		/// ポイントを削除する。削除したオブジェクトを返す
		/// </summary>
		public MiniMapPointObject RemoveObject(GameObject obj)
		{
			PointDict.TryGetValue(obj, out var point);
			PointDict.Remove(obj);

			return point;
		}

		/// <summary>
		/// マップを適用する
		/// </summary>
		public void ApplyTileDataMap(int mapLevel, Map.TileDataMap tileDataMap)
		{
			Used.Value = true;
			MapLevel = mapLevel;

			// タイル情報の再設定
			View.SetTileDataMap(tileDataMap);
		}

		/// <summary>
		/// マップを削除する
		/// </summary>
		public void Clear()
		{
			Used.Value = false;
			MapLevel = 0;
		}

		public void ShowView(int mapLevel)
		{
			// 同じレベルの場合表示
			View.SetActive(MapLevel == mapLevel);
		}
	}


	/// <summary>
	/// ミニマップControl
	/// </summary>
	public class MiniMapControl : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private ISubscriber<EventSpawnMapObject> _eventDeployMapObject;
		[Inject] private ISubscriber<EventDestroyMapObject> _eventDestroyMapObject;

		[SerializeField] private MiniMapView[] _views = default;
		[SerializeField, ES3NonSerializable] private MiniMapPoolManager _poolManager = null;

		[SerializeField] private List<MiniMapModel> _models = new List<MiniMapModel>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ


		#endregion


		#region public, protected 関数


		public void Setup()
		{
			foreach (var view in _views)
			{
				view.Setup();

				var model = new MiniMapModel();
				model.Setup(view);

				_models.Add(model);
			}

			// マップにオブジェクトが追加されたとき
			_eventDeployMapObject
				.Subscribe(ev => 	
					{
						AddPointObject(ev.MapLevel, ev.Flag, ev.followObj);
					}).AddTo(this);

			// マップからオブジェクトが削除されたとき
			_eventDestroyMapObject
				.Subscribe(ev => 
					{
						RemovePointObject(ev.MapLevel, ev.followObj);
					}).AddTo(this);
		}

		/// <summary>
		/// 新しくミニマップを設定する
		/// </summary>
		public void ApplyMinimap(int level, Map.TileDataMap tileDataMap)
		{
			var model = _models.Find(view => !view.Used.Value);
			if (model == null)
			{
				Utility.Log.Error("空きがない");
				return;
			}

			model.ApplyTileDataMap(level, tileDataMap);
		}

		/// <summary>
		/// Viewをもとに戻す
		/// </summary>
		public void ClearView(int level)
		{
			var model = FindModelByMapLevel(level);
			model.Clear();
		}

		/// <summary>
		/// 指定したレベルのマップを表示する
		/// </summary>
		public void Show(int level)
		{
			foreach (var model in _models)
			{
				model.ShowView(level);
			}
		}

		#endregion


		#region private 関数

		private MiniMapModel FindModelByMapLevel(int mapLevel)
		{
			var model = _models.Find(model => model.MapLevel == mapLevel);
			if (model == null)
			{
				Utility.Log.Error($"ミニマップが持つModelの中から見つからない {mapLevel}");
			}

			return model;
		}

		/// <summary>
		/// ミニマップ上にオブジェクトを追加する
		/// </summary>
		private void AddPointObject(int mapLevel, Const.TileFlag flag, GameObject followObj)
		{
			var pointObj = _poolManager.Pop<MiniMapPointObject>(flag);
			if (pointObj == null) return;
						
			var model = FindModelByMapLevel(mapLevel);
			model.AddObject(followObj, pointObj);

			// プール管理されているものの場合、プールに戻されるタイミングで削除する
			var poolCallbacker = followObj.GetComponent<Utility.Pool.PoolCallbacker>();
			if (poolCallbacker != null)
			{
				poolCallbacker.OnDetach
					.First()
					.Subscribe(_ => 
					{
						RemovePointObject(mapLevel, followObj);
					}).AddTo(this);
			}

		}

		/// <summary>
		/// ミニマップ上のオブジェクトを削除する
		/// </summary>
		private void RemovePointObject(int mapLevel, GameObject followObj)
		{
			var model = FindModelByMapLevel(mapLevel);
			if (model == null) return;

			var removePointObject = model.RemoveObject(followObj);

			// プールに戻す
			_poolManager.Push(removePointObject.gameObject);
		}

		#endregion
	}
}
