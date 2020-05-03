//
// MapControl.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.03
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

namespace Ling.Scenes.Battle.BattleMap
{
	/// <summary>
	/// ダンジョンマップコントロール
	/// </summary>
	public class MapControl
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private MapView _view;
		private Common.Tile.MapTile _mapTile;

		#endregion


		#region プロパティ

		public Tilemap Tilemap => _view.Tilemap;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(Map.Builder.TileDataMap tileDataMap)
		{
			var view = GameManager.Instance.Resolve<BattleView>();
			_view = view.MapView;

			_mapTile = Resources.Load<Common.Tile.MapTile>("Tiles/SimpleMapTile");
			if (_mapTile == null)
			{
				Utility.Log.Error("MapTileリソースが見つかりません");
			}

			// タイル情報の再設定
			_mapTile.SetTileDataMap(tileDataMap);

			var width = tileDataMap.Width;
			var height = tileDataMap.Height;

			_view.Setup(width, height, _mapTile);
		}

		/// <summary>
		/// 指定したセルの中心ワールド座標を取得する
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public Vector3 GetCellCenterWorld(int x, int y)
		{
			var tilemap = _view.Tilemap;

			return tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0));
		}

		#endregion


		#region private 関数

		#endregion
	}
}
