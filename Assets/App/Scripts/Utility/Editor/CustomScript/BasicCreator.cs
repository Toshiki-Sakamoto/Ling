using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace Utility.Editor.CustomScript
{
	public class BasicCreator : CreatorEditorWindow<BasicCreator>
	{
		private const string TEMPLATE_SCRIPT_NAME = "BasicClass";

		[MenuItem(TEMPLATE_SCRIPT_NAME, menuItem = Const.MENU_PATH + TEMPLATE_SCRIPT_NAME, priority = 1)]
		private static void CreateScript()
		{
			ShowWindow(TEMPLATE_SCRIPT_NAME);
		}
	}
}
