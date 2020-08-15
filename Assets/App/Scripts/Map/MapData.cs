//
// MapData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.05
//

using Ling.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Map
{
	/// <summary>
	/// １階層の情報(サイズや見た目)を持つ
	/// </summary>
	public class MapData
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public int Width => TileDataMap.Width;
		public int Height => TileDataMap.Height;

		/// <summary>
		/// マップデータ
		/// </summary>
		public Map.TileDataMap TileDataMap { get; private set; }

		/// <summary>
		/// マップタイル情報を持つ
		/// </summary>
		public Common.Tile.MapTile MapTileRenderData { get; private set; }

		/// <summary>
		/// マップ作成時のビルダー
		/// </summary>
		public Map.Builder.IBuilder Builder { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(Map.Builder.IBuilder builder, Map.TileDataMap tileDataMap)
		{
			Builder = builder;
			TileDataMap = tileDataMap;

			var mapTile = Resources.Load<Common.Tile.MapTile>("Tiles/SimpleMapTile");
			if (mapTile == null)
			{
				Utility.Log.Error("MapTileリソースが見つかりません");
			}

			// 複製を作成する
			MapTileRenderData = UnityEngine.Object.Instantiate(mapTile) as Common.Tile.MapTile;

			// タイル情報の再設定
			MapTileRenderData.SetTileDataMap(tileDataMap);
		}

		/// <summary>
		/// 部屋のランダムな座標を取得する
		/// </summary>
		public Vector2Int GetRandomPosInRoom()
		{
			var values = TileDataMap.RoomMap.Values;
			var pos = values.ElementAt(Utility.Random.Range(values.Count));

			return pos.GetRandom();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
