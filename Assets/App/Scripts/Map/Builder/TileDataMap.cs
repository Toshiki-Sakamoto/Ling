//
// MapData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.21
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Map.Builder
{
	/// <summary>
	/// <see cref="TileData"/>を管理する
	/// </summary>
	public class TileDataMap
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private TileData[] _tileData;

		#endregion


		#region プロパティ

		public int Width { get; private set; }
		public int Height { get; private set; }

		public int Size => Width * Height;

		public TileData[] Tiles => _tileData;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Initialize(int width, int height)
		{
			Width = width;
			Height = height;

			_tileData = new TileData[width * height];
		}

		public void AllTilesSetWall()
		{
			_tileData.ForEach((ref TileData tileData_) => tileData_.SetWall());
		}

		/// <summary>
		/// [x, y] から指定したタイル情報を返す
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public ref TileData GetTile(int x, int y)
		{
			Utility.Log.Assert(x >= 0 && x <= Width && y >= 0 && y <= Height, "範囲から飛び出してます");

			return ref _tileData[y * Width + x];
		}


		#endregion


		#region private 関数

		#endregion
	}
}
