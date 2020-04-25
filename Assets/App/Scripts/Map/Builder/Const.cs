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

            Floor       = 1 << 1,   // 床
			Wall		= 1 << 2,	// 壁
			StepUp		= 1 << 3,	// 上り階段
			StepDown	= 1 << 4,   // 下り階段
		}

		/// <summary>
		/// ビルダーの種類
		/// </summary>
		public enum BuilderType
		{ 
			None	= 0,

			Split,
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
