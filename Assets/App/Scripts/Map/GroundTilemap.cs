//
// GroundTilemap.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.25
//

using UnityEngine;
using UnityEngine.Tilemaps;
using Ling.Utility.Extensions;
using Zenject;
using Ling.Utility;

namespace Ling.Map
{
	/// <summary>
	/// GroundGrid一つのデータ
	/// </summary>
	public class GroundTilemap : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private Grid _grid = default;
		[SerializeField] private Tilemap _tilemap = default;
		[SerializeField] private int _mapLevel = default;
		[SerializeField] private Transform _itemRoot = default;
		[SerializeField] private Transform _enemyRoot = default;
		[SerializeField] private Utility.Renderer.SortingLayerChanger _sortingChanger = default;
		[SerializeField] private Chara.EnemyControlGroup _enemyControlGroup = default;

#if DEBUG
		[SerializeField] private Transform _tileDebugUIRoot = default;
		[SerializeField] private _Debug.ScoreUIView _scoreTileViewPrefab = default;

		private _Debug.ScoreUIView[] _scoreTileView = null;
#endif

		[Inject] private MasterData.MasterManager _masterManager = default;
		[Inject] private Utility.IEventManager _eventManager = default;

		#endregion


		#region プロパティ

		public Tilemap Tilemap => _tilemap;

		public int Level => _mapLevel;

		public Transform ItemRoot => _itemRoot;
		
		public Transform EnemyRoot => _enemyRoot;

		/// <summary>
		/// SortingLayer名
		/// </summary>
		public string LayerName => _sortingChanger.LayerName;

		/// <summary>
		/// マップ上の敵キャラ管理
		/// </summary>
		public Chara.EnemyControlGroup EnemyControlGroup => _enemyControlGroup;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 初期化
		/// </summary>
		public void Clear()
		{
			_tilemap.ClearAllTiles();

			_grid.gameObject.SetActive(false);
		}

		public void SetActive(bool isActive)
		{
			_grid.gameObject.SetActive(isActive);
		}

		public void SetGridName(int index)
		{
			_grid.name = $"GroundGrid_{index.ToString()}";
		}

		public void SetLocalPosition(Vector3 pos)
		{
			_grid.transform.localPosition = pos;
		}

		public void SetSortingLayer(string layerName)
		{
			_sortingChanger.LayerName = layerName;
		}

		/// <summary>
		/// マップ情報を作成する
		/// </summary>
		public void BuildMap(int mapIndex, int width, int height, Tile.MapTile tile)
		{
			_tilemap.ClearAllTiles();

			_mapLevel = mapIndex;

			// 見えない壁補正をつける
#if false
			var correct = _masterManager.Const.CorrectionMapSize;
			for (int y = 0, ySize = height + correct.y; y <= ySize; ++y)
			{
				for (int x = 0, xSize = width + correct.x; x <= xSize; ++x)
				{
					_tilemap.SetTile(new Vector3Int(x, y, 0), tile);
				}
			}
#endif
			DebugInitDebugTileUI(width, height);

			for (int y = 0; y <= height; ++y)
			{
				for (int x = 0; x <= width; ++x)
				{
					var cellPosition = new Vector3Int(x, y, 0);
					_tilemap.SetTile(cellPosition, tile);

					DebugSetTileDebugView(cellPosition, width);
				}
			}

			SetGridName(mapIndex);
		}

		#endregion


		#region private 関数


		[System.Diagnostics.Conditional("DEBUG")]
		private void DebugInitDebugTileUI(int width, int height)
		{
#if DEBUG
			if (!_scoreTileView.IsNullOrEmpty())
			{
				foreach (var view in _scoreTileView)
				{
					Destroy(view.gameObject);
				}
			}

			_scoreTileView = new _Debug.ScoreUIView[width * height];

			for (var y = 0; y < height; ++y)
			{
				for (var x = 0; x < width; ++x)
				{
					var index = y * width + x;
					var view = GameObject.Instantiate(_scoreTileViewPrefab, _tileDebugUIRoot);
					view.gameObject.SetActive(false);

					_scoreTileView[index] = view;
				}
			}

			this.AddEventListener<_Debug.EventDebugUIClearAll>(ev_ =>
				{
					if (ev_.mapLevel != _mapLevel) return;

					foreach (var tileView in _scoreTileView)
					{
						tileView.gameObject.SetActive(false);
					}
				});

			this.AddEventListener<_Debug.EventSearchNodeCreated>(ev_ =>
				{
					if (ev_.mapLevel != _mapLevel) return;
					
					var index = ev_.node.pos.y * width + ev_.node.pos.x;

					_scoreTileView[index].SetScore(ev_.node.score);
				});
#endif
		}

		[System.Diagnostics.Conditional("DEBUG")]
		public void DebugSetTileDebugView(in Vector3Int cellPosition, int width)
		{
#if DEBUG
			var index = cellPosition.y * width + cellPosition.x;
			if (index < 0 || index >= _scoreTileView.Length) return;
			
			_scoreTileView[index].SetTileData(_tilemap, cellPosition);
#endif
		}

		#endregion
	}
}
