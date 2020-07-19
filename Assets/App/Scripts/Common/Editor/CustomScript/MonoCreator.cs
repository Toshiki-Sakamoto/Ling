using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace Ling.Common.Editor.CustomScript
{
    public class MonoCreator : CreatorEditorWindow
    {
        private const string TEMPLATE_SCRIPT_NAME = "MonoClass";

        [MenuItem(TEMPLATE_SCRIPT_NAME, menuItem = Const.MENU_PATH + TEMPLATE_SCRIPT_NAME, priority = 1)]
        private static void CreateScript()
        {
            ShowWindow(TEMPLATE_SCRIPT_NAME);
        }
    }
}