﻿//
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


namespace Ling.Map.Builder
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
	}

	public static class TileFlagExtensions
	{
	}
}
