//
// GroundTilemap.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.25
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Zenject;

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
		[SerializeField] private Transform _enemyRoot = default;
		[SerializeField] private Utility.Renderer.SortingLayerChanger _sortingChanger = default;
		[SerializeField] private Chara.EnemyControlGroup _enemyControlGroup = default;

		[Inject] private MasterData.MasterManager _masterManager = default;

		#endregion


		#region プロパティ

		public Tilemap Tilemap => _tilemap;

		public int Level => _mapLevel;

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
		public void BuildMap(int mapIndex, int width, int height, Common.Tile.MapTile tile)
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
			for (int y = 0; y <= height; ++y)
			{
				for (int x = 0; x <= width; ++x)
				{
					_tilemap.SetTile(new Vector3Int(x, y, 0), tile);
				}
			}

			SetGridName(mapIndex);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
