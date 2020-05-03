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
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.MiniMap
{
	/// <summary>
	/// 
	/// </summary>
	public class MiniMapControl
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private MiniMapView _view;
		private Common.Tile.MiniMapTile _miniMapTile;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		public MiniMapControl()
		{
			var view = GameManager.Instance.Resolve<BattleView>();
			_view = view.MiniMap;

			_miniMapTile = Resources.Load<Common.Tile.MiniMapTile>("Tiles/MiniMapTile");
			if (_miniMapTile == null)
			{
				Utility.Log.Error("MiniMapTileリソースが見つかりません");
			}
		}

		#endregion


		#region public, protected 関数


		public void Setup(Map.Builder.TileDataMap tileDataMap)
		{
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
