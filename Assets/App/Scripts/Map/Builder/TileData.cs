//
// TileData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.23
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
	/// マップの中の一マスのデータ
	/// </summary>
	public struct TileData
	{
		public bool isWall;     // 壁ならtrue
		public bool isStepUp;   // 上階段ならtrue
	}


	/// <summary>
	/// TileData拡張機能
	/// </summary>
	public static class TileDataExtensions
	{
		/// <summary>
		/// 初期化する
		/// </summary>
		/// <param name="tileData"></param>
		public static void Initialize(this TileData tileData)
		{
			tileData.isStepUp = false;
			tileData.isWall = false;
		}

		/// <summary>
		/// 壁にする
		/// </summary>
		public static void SetWall(this TileData tileData)
		{
			
		}
	}
}
