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

		#endregion


		#region プロパティ

		public int Width { get; private set; }
		public int Height { get; private set; }

		public int Size => Width * Height;

		public TileData[] Tiles { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Initialize(int width, int height)
		{
			Width = width;
			Height = height;

			Tiles = new TileData[width * height];
		}

		public void AllTilesSetWall()
		{
			Tiles.ForEach((ref TileData tileData_) => tileData_.SetWall());
		}

        /// <summary>
        /// 指定区画を指定フラグで上書きする
        /// </summary>
        public void FillRect(int left, int top, int right, int bottom, Const.TileFlag flag)
        {
            for (int y = top; y <= bottom; ++y)
            {
                for (int x = left; x <= right; ++x)
                {
					ref var tileData = ref GetTile(x, y);
					tileData.AddFlag(flag);
                }
            }
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

			return ref Tiles[y * Width + x];
		}


		#endregion


		#region private 関数

		#endregion
	}
}
