// 
// MapView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.03
// 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Zenject;
using Utility;

namespace Ling.Map
{
	/// <summary>
	/// ダンジョンマップView
	/// </summary>
	public class MapView : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		public System.Action<Map.GroundTilemap, int> OnStartGroundmapData { get; set; }
		public System.Action<Map.GroundTilemap, int> OnUpdateItem { get; set; }

		#endregion


		#region private 変数

		[SerializeField] private Transform _playerRoot = default;
		[SerializeField] private Transform _effectRoot = default;
		[SerializeField] private List<Map.GroundTilemap> _groundMaps = default;

		[Inject] private MasterData.IMasterHolder _masterHolder = default;
		[Inject] private IEventManager _eventManager = default;

		private List<Map.GroundTilemap> _usedItems = new List<Map.GroundTilemap>();
		private List<Map.GroundTilemap> _unusedItems = new List<Map.GroundTilemap>();
		private int _currentMapIndex = 0;
		private List<string> _sortingMap = new List<string>()
			{
				Const.SortingLayer.TileMap04,
				Const.SortingLayer.TileMap03,
				Const.SortingLayer.TileMap02,
				Const.SortingLayer.TileMap01,
			};

		#endregion


		#region プロパティ

		public Transform PlayerRoot => _playerRoot;
		public Transform EffectRoot => _effectRoot;

		/// <summary>
		/// 現在のTilemap
		/// </summary>
		public Tilemap CurrentTilemap => GetTilemap(_currentMapIndex);

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 指定したMapIDからVIEWを作成する
		/// </summary>
		/// <param name="mapID"></param>
		public void Startup(MapModel mapModel, int startMapIndex, int maxNum)
		{
			_usedItems.Clear();
			_unusedItems.Clear();

			// 未使用リストに追加する
			foreach (var elm in _groundMaps)
			{
				PushUnusedItem(elm);
			}

			// 1以下は無い
			var createStartIndex = Mathf.Max(startMapIndex - mapModel.AddMap, 1);

			for (int i = createStartIndex, count = startMapIndex + mapModel.AddMap; i <= count; ++i)
			{
				// 未使用リストから使用リストに追加する
				var tilemapData = PopUnusedItem();

				PushUsedItem(tilemapData);

				OnStartGroundmapData?.Invoke(tilemapData, i);
			}

			// 位置等を強制的に決める
			ForceTransformAdjustment(startMapIndex);

			_currentMapIndex = startMapIndex;
		}

		public void SetMapIndex(int mapIndex)
		{
			_currentMapIndex = mapIndex;
		}

		/// <summary>
		/// 新しいマップを作成
		/// </summary>
		public void CreateMapView(int createMapIndex)
		{
			// 新しいマップを作成する
			var tilemapData = PopUnusedItem();

			PushUsedItem(tilemapData);

			// 初期位置に配置する
			ForceTransformAdjustment(_currentMapIndex);

			OnUpdateItem?.Invoke(tilemapData, createMapIndex);
		}

		/// <summary>
		/// 指定した階層のマップのTilemapを取得する
		/// </summary>
		public Map.GroundTilemap FindGroundTilemap(int level)
		{
			var mapData = _groundMaps.Find(map_ => map_.Level == level);
			if (mapData == null)
			{
				Utility.Log.Error($"指定されたマップ階層(MapIndex)が見つからない {level}");
				return null;
			}

			return mapData;
		}

		/// <summary>
		/// アイテムを指定したレベルのマップに設定する
		/// </summary>
		public void SetItem(int level)
		{

		}

		/// <summary>
		/// 敵を指定したレベルのマップに設定する
		/// </summary>
		public void SetEnemy(Chara.ViewBase enemy, int level)
		{
			var root = GetEnemyRoot(level);
			var groundTilemap = FindGroundTilemap(level);

			// 親とsortingLayerの設定
			enemy.transform.SetParent(root, worldPositionStays: false);
			enemy.SetSortingLayerAndOrder(groundTilemap.LayerName, (int)OrderType.Chara);
		}

		public void SetSortingLayerAndOrder(GameObject gameObject, int level, OrderType orderType = OrderType.Chara)
		{
			var sortingLayerChanger = gameObject.GetComponent<Utility.Renderer.SortingLayerChanger>();
			if (sortingLayerChanger == null)
			{
				Utility.Log.Error("SortingLayerChangerがアタッチされていない");
				return;
			}

			// 親の設定
			var root = default(Transform);
			switch (orderType)
			{
				case OrderType.Item:
					root = GetItemRoot(level);
					break;

				default:
					Utility.Log.Error($"指定されたタイプの親がいない {orderType}");
					return;
			}

			gameObject.transform.SetParent(root, worldPositionStays: true);

			var groundTilemap = FindGroundTilemap(level);
			sortingLayerChanger.SetLayerNameAndOrder(groundTilemap.LayerName, (int)orderType);
		}

		/// <summary>
		/// 指定階層のアイテムルートを取得する
		/// </summary>
		/// <param name="level"></param>
		/// <returns></returns>
		public Transform GetItemRoot(int level) =>
			FindGroundTilemap(level)?.ItemRoot;

		/// <summary>
		/// 指定階層の敵ルートを取得する
		/// </summary>
		public Transform GetEnemyRoot(int level) =>
			FindGroundTilemap(level)?.EnemyRoot;

		/// <summary>
		/// 指定したIndexのTilemapを検索する
		/// </summary>
		public Tilemap GetTilemap(int level) =>
			FindGroundTilemap(level)?.Tilemap;


		/// <summary>
		/// 指定したMapIndexを中心としてMapを並び直す
		/// </summary>
		/// <param name="startLevel"></param>
		public void ForceTransformAdjustment(int startLevel)
		{
			var currentIndex = _usedItems.FindIndex(tile_ => tile_.Level == startLevel);
			var height = _masterHolder.Const.MapDiffHeight;

			for (int i = 0; i < _usedItems.Count; ++i)
			{
				var diff = i - currentIndex;

				_usedItems[i].SetLocalPosition(new Vector3(0.0f, 0.0f, height * diff));

				// SortingLayerの設定
				var sortingName = _sortingMap[i];
				_usedItems[i].SetSortingLayer(sortingName);
			}
		}

		/// <summary>
		/// 指定した配列に存在しない余分なMapを削除する
		/// </summary>
		public List<int> RemoveExtraTilemap(List<int> indexes)
		{
			var result = new List<int>();

			foreach (var tilemap in _usedItems.ToArray())
			{
				if (indexes.Exists(index_ => index_ == tilemap.Level))
				{
					continue;
				}

				// 削除する
				PushUnusedItem(tilemap);

				// todo: 落とし物もここで破棄
				ResetDropItemController(tilemap);

				// 削除したことを伝える
				_eventManager.Trigger(new EventRemoveMap { level = tilemap.Level });

				result.Add(tilemap.Level);
			}

			return result;
		}

		public DropItemController FindDropItemController(int index)
		{
			var groundTilemap = FindGroundTilemap(index);

			return GetDropItemController(groundTilemap);
		}

		#endregion


		#region private 関数

		/// <summary>
		/// 未使用リストに追加する
		/// </summary>
		/// <param name="groundTilemap"></param>
		private void PushUnusedItem(Map.GroundTilemap groundTilemap)
		{
			groundTilemap.Clear();

			// リストにあるなら取り除く
			_usedItems.Remove(groundTilemap);
			_unusedItems.Remove(groundTilemap);

			_unusedItems.Add(groundTilemap);
		}

		/// <summary>
		/// 未使用リストから一つデータを取得する
		/// </summary>
		/// <returns></returns>
		private Map.GroundTilemap PopUnusedItem()
		{
			var result = _unusedItems[0];

			_unusedItems.RemoveAt(0);

			return result;
		}

		/// <summary>
		/// 使用リストに追加する
		/// </summary>
		/// <param name="groundTilemap"></param>
		private void PushUsedItem(Map.GroundTilemap groundTilemap)
		{
			_usedItems.Add(groundTilemap);

			groundTilemap.SetActive(true);
		}

		private Map.GroundTilemap PopUsedItem()
		{
			var result = _usedItems[0];

			_usedItems.RemoveAt(0);

			return result;
		}


		/// <summary>
		/// todo: 現状はここで落とし物の管理を行う
		/// </summary>
		private void ResetDropItemController(Map.GroundTilemap groundTilemap)
		{
			var controller = GetDropItemController(groundTilemap);
			controller.ReleaseAll();
		}

		private DropItemController GetDropItemController(Map.GroundTilemap groundTilemap)
		{
			return groundTilemap.GetComponent<DropItemController>();
		}

		#endregion
	}
}