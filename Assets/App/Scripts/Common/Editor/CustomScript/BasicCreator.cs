using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace Ling.Common.Editor.CustomScript
{
    public class BasicCreator : Creator
    {
        private const string TEMPLATE_SCRIPT_NAME = "BasicClass";

        [MenuItem(Const.MENU_PATH + TEMPLATE_SCRIPT_NAME)]
        private static void CreateScript()
        {
            ShowWindow(TEMPLATE_SCRIPT_NAME);
        }
    }
}
