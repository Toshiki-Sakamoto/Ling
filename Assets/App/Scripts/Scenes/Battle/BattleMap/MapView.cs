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
using UniRx;

using Zenject;
using System;

namespace Ling.Scenes.Battle.BattleMap
{
	/// <summary>
	/// ダンジョンマップView
	/// </summary>
	public class MapView : MonoBehaviour 
    {
		#region 定数, class, enum

		[System.Serializable]
		public class GroundTilemap
		{
			public Grid grid = null;
			public Tilemap tilemap = null;
			public int mapIndex = 0;

			/// <summary>
			/// 初期化
			/// </summary>
			public void Reset()
			{
				tilemap.ClearAllTiles();

				grid.gameObject.SetActive(false);
			}

			public void SetActive(bool isActive)
			{
				grid.gameObject.SetActive(isActive);
			}

			public void SetGridName(int index)
			{
				grid.name = $"GroundGrid_{index.ToString()}";
			}

			public void SetLocalPosition(Vector3 pos)
			{
				grid.transform.localPosition = pos;
			}

			/// <summary>
			/// マップ情報を作成する
			/// </summary>
			/// <param name="mapIndex"></param>
			/// <param name="width"></param>
			/// <param name="height"></param>
			/// <param name="tile"></param>
			public void BuildMap(int mapIndex, int width, int height, Common.Tile.MapTile tile)
			{
				tilemap.ClearAllTiles();

				this.mapIndex = mapIndex;

				for (int y = 0; y <= height; ++y)
				{
					for (int x = 0; x <= width; ++x)
					{
						tilemap.SetTile(new Vector3Int(x, y, 0), tile);
					}
				}

				SetGridName(mapIndex);
			}
		}

		#endregion


		#region public 変数

		public System.Action<GroundTilemap, int> OnStartItem { get; set; }
		public System.Action<GroundTilemap, int> OnUpdateItem { get; set; }

		#endregion


		#region private 変数

		[SerializeField] private List<GroundTilemap> _groundMaps = null;

		[Inject] BattleModel _model = null;

		private List<GroundTilemap> _usedItems = new List<GroundTilemap>();
		private List<GroundTilemap> _unusedItems = new List<GroundTilemap>();

		#endregion


		#region プロパティ

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

			// 0以下は無い
			var createStartIndex = Mathf.Max(startMapIndex - BattleConst.AddShowMap, 0);

			for (int i = createStartIndex, count = startMapIndex + BattleConst.AddShowMap; i <= count; ++i)
			{
				// 未使用リストから使用リストに追加する
				var tilemapData = PopUnusedItem();

				PushUsedItem(tilemapData);

				OnStartItem?.Invoke(tilemapData, i);
			}

			// 位置等を強制的に決める
			ForceTransformAdjustment(startMapIndex);
		}

		/// <summary>
		/// 新しいマップを作成し
		/// 次のマップに移動する
		/// </summary>
		public IObservable<Unit> CreateAndMoveNextMap(int nextMapIndex, int createMapIndex)
		{
			return Observable.Create<Unit>(observer_ =>
				{
					// 新しいマップを作成する
					var tilemapData = PopUnusedItem();

					PushUsedItem(tilemapData);

					OnUpdateItem?.Invoke(tilemapData, createMapIndex);

					// 移動する(アニメーションにすること)
					ForceTransformAdjustment(nextMapIndex);

					// 前のマップを削除する
					var maxShowMapNum = BattleConst.AddShowMap * 2 + 1;

					for (int i = maxShowMapNum, count = _usedItems.Count; i < count; ++i)
					{
						var usedItem = PopUsedItem();

						PushUnusedItem(usedItem);
					}

					// 自分の位置を0に戻す
					transform.localPosition = Vector3.zero;

					// OnNext読んでやらないとSubscribeのonNextが呼ばれないよ
					observer_.OnNext(new Unit());

					return Disposable.Empty;
				});
		}

		/// <summary>
		/// 指定した階層のマップのTilemapを取得する
		/// </summary>
		/// <returns></returns>
		public Tilemap FindTilemap(int mapIndex)
		{
			var mapData = _groundMaps.Find(map_ => map_.mapIndex == mapIndex);
			if (mapData == null)
			{
				Utility.Log.Error($"指定されたマップ階層(MapIndex)が見つからない {mapIndex}");
				return null;
			}

			return mapData.tilemap;
		}


		public void ForceTransformAdjustment(int startMapIndex)
		{
			var currentIndex = _usedItems.FindIndex(tile_ => tile_.mapIndex == startMapIndex);

			for (int i = 0; i < _usedItems.Count; ++i)
			{
				var diff = i - currentIndex;

				_usedItems[i].SetLocalPosition(new Vector3(0.0f, 0.0f, BattleConst.TilemapYPositionDiff * diff));
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
		private void PushUnusedItem(GroundTilemap groundTilemap)
		{
			groundTilemap.Reset();

			_unusedItems.Add(groundTilemap);
		}

		/// <summary>
		/// 未使用リストから一つデータを取得する
		/// </summary>
		/// <returns></returns>
		private GroundTilemap PopUnusedItem()
		{
			var result = _unusedItems[0];

			_unusedItems.RemoveAt(0);

			return result;
		}

		/// <summary>
		/// 使用リストに追加する
		/// </summary>
		/// <param name="groundTilemap"></param>
		private void PushUsedItem(GroundTilemap groundTilemap)
		{
			_usedItems.Add(groundTilemap);

			groundTilemap.SetActive(true);
		}

		private GroundTilemap PopUsedItem()
		{
			var result = _usedItems[0];

			_usedItems.RemoveAt(0);

			return result;
		}

		#endregion
	}
}