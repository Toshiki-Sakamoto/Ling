//
// Argument.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.16
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Common.Scene
{
	/// <summary>
	/// 
	/// </summary>
	public class Argument
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		/// <summary>
		/// シーンスタックをクリアする`
		/// </summary>
		public bool IsStackClear { get; set; }

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public static Argument Create() =>
			new Argument();

		#endregion


		#region private 関数

		#endregion
	}
}
