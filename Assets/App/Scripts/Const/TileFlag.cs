//
// TileFlag.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.26
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Const
{
	/// <summary>
	/// タイル情報のフラグ定数
	/// </summary>
	[System.Flags]
	public enum TileFlag //: long
    {
			None		= 0,		// なにもない

            Floor       = 1 << 1,   // 床
			Wall		= 1 << 2,	// 壁
			StepUp		= 1 << 3,	// 上り階段
			StepDown	= 1 << 4,   // 下り階段
			Road		= 1 << 5,	// 道
			Item		= 1 << 6,	// アイテム
			Trap		= 1 << 7,	// 罠
			Player		= 1 << 8,	// プレイヤー
			Enemy		= 1 << 9,	// エネミー
			Hole		= 1 << 10,	// 穴
	}

	public static class TileFlagExtensions
	{
		/// <summary>
		/// valueのいずれかが含まれているか
		/// </summary>
		public static bool HasAny(this TileFlag self, TileFlag value) =>
			(self & value) != 0;

		public static bool HasStepDown(this TileFlag flag) =>
			flag.HasAny(TileFlag.StepDown);

		public static bool HasFloor(this TileFlag flag) =>
			flag.HasAny(TileFlag.Floor);

		public static bool HasWall(this TileFlag flag) =>
			flag.HasAny(TileFlag.Wall);
	}
}
