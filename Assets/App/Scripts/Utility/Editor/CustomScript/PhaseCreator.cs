using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace Ling.Utility.Editor.CustomScript
{
	public class PhaseCreator : CreatorEditorWindow<PhaseCreator>
	{
		private const string TEMPLATE_SCRIPT_NAME = "PhaseClass";

		protected override bool UseParam1 => true;
		protected override string Param1Title => "シーン名";


		[MenuItem(TEMPLATE_SCRIPT_NAME, menuItem = Const.MENU_PATH + TEMPLATE_SCRIPT_NAME, priority = 1)]
		private static void CreateScript()
		{
			ShowWindow(TEMPLATE_SCRIPT_NAME);
		}
	}
}
