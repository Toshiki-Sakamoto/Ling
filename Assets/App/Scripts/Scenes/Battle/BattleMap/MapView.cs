// 
// MapView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.03
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System;
using System.Linq;
using UniRx;

using Zenject;
using Cysharp.Threading.Tasks;
using Ling.Utility;
using System.Runtime.CompilerServices;

namespace Ling.Scenes.Battle.BattleMap
{
	/// <summary>
	/// ダンジョンマップView
	/// </summary>
	public class MapView : MonoBehaviour 
    {
		#region 定数, class, enum

		public enum OrderType : int
		{
			Map, 
			Item,
			Chara
		}

		#endregion


		#region public 変数

		public System.Action<Map.GroundTilemap, int> OnStartGroundmapData { get; set; }
		public System.Action<Map.GroundTilemap, int> OnUpdateItem { get; set; }

		#endregion


		#region private 変数

		[SerializeField] private Transform _playerRoot = default;
		[SerializeField] private List<Map.GroundTilemap> _groundMaps = default;

		[Inject] private BattleModel _model = default;
		[Inject] private MasterData.MasterManager _masterManager = default;
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
			var createStartIndex = Mathf.Max(startMapIndex - BattleConst.AddShowMap, 1);

			for (int i = createStartIndex, count = startMapIndex + BattleConst.AddShowMap; i <= count; ++i)
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
			var height = _masterManager.Const.MapDiffHeight;

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
		public void RemoveExtraTilemap(List<int> indexes)
		{
			foreach (var tilemap in _usedItems.ToArray())
			{
				if (indexes.Exists(index_ => index_ == tilemap.Level))
				{
					continue;
				}

				// 削除する
				PushUnusedItem(tilemap);

				// 削除したことを伝える
				_eventManager.Trigger(new EventRemoveMap { level = tilemap.Level });
			}
		}

		#endregion


		#region private 関数

#if false
		/// <summary>
		/// Tilemapを構築する
		/// </summary>
		/// <param name="mapIndex"></param>
		/// <param name="listIndex"></param>
		private void BuildMapView(MapModel mapModel, int mapIndex, int listIndex)
		{

		}
#endif
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

		#endregion
	}
}