//
// Const.cs
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
	/// 定数
	/// </summary>
    public class Const
    {
		#region 定数, class, enum


		/// <summary>
		/// タイル情報のフラグ定数
		/// </summary>
		[System.Flags]
		public enum TileFlag : long
		{
			None		= 0,		// なにもない

			Wall		= 1 << 1,	// 壁
			StepUp		= 1 << 2,	// 上り階段
			StepDown	= 1 << 3,   // 下り階段
		}

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        #endregion


        #region プロパティ

        #endregion


		#region コンストラクタ, デストラクタ

		#endregion


        #region public, protected 関数

        #endregion


        #region private 関数

        #endregion
    }
}
