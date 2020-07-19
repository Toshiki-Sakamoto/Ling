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
	public class TestCreator : CreatorEditorWindow
	{
		private const string TEMPLATE_SCRIPT_NAME = "TestClass";

		[MenuItem(TEMPLATE_SCRIPT_NAME, menuItem = Const.MENU_PATH + TEMPLATE_SCRIPT_NAME, priority = 1)]
		private static void CreateScript()
		{
			ShowWindow(TEMPLATE_SCRIPT_NAME);
		}
	}
}
