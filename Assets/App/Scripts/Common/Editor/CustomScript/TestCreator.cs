//
// TestCreator.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.29
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using UnityEditor;

namespace Ling.Common.Editor.CustomScript
{
	/// <summary>
	/// Testスクリプト作成
	/// </summary>
	public class TestCreator : Creator
	{
		#region 定数, class, enum

		private const string TEMPLATE_SCRIPT_NAME = "TestClass";

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

		[MenuItem(Const.MENU_PATH + TEMPLATE_SCRIPT_NAME)]
		private static void CreateScript()
		{
			ShowWindow(TEMPLATE_SCRIPT_NAME);
		}

		#endregion
	}
}
