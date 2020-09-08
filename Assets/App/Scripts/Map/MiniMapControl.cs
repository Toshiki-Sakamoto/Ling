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

using Zenject;

namespace Ling.Map
{
	/// <summary>
	/// 
	/// </summary>
	public class MiniMapControl : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private MiniMapView _view = default;
		
		private Common.Tile.MapTile _miniMapTile;

		#endregion


		#region プロパティ

		public MiniMapView View => _view;
		public Tilemap Tilemap => null;// _view.Tilemap;

		#endregion


		#region コンストラクタ, デストラクタ


		#endregion


		#region public, protected 関数


		public void Setup(Map.TileDataMap tileDataMap)
		{
			_miniMapTile = Resources.Load<Common.Tile.MapTile>("Tiles/MiniMapTile");
			if (_miniMapTile == null)
			{
				Utility.Log.Error("MiniMapTileリソースが見つかりません");
			}

			// タイル情報の再設定
			_miniMapTile.SetTileDataMap(tileDataMap);

			var width = tileDataMap.Width;
			var height = tileDataMap.Height;

			_view.Setup(width, height, _miniMapTile);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
