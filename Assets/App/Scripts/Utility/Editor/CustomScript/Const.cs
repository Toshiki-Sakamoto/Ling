using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Editor.CustomScript
{
	public static class Const
	{
		// メニューのパス
		public const string MENU_PATH = "Assets/Create/Custom Script/";

		// テンプレートの拡張子
		public const string EXT_TEMPLATE_SCRIPT = ".txt";

		// スクリプトの拡張子
		public const string EXT_SCRIPT = ".cs";

		/// <summary>
		/// テンプレートスクリプトがある場所
		/// </summary>
		public const string TEMPLATE_SCRIPT_DIRECTORY_PATH = "Assets/ScriptTemplates";


		public static class TemplateTag
		{
			public const string PRODUCT_NAME = "#PRODUCTNAME#";
			public const string AUTHOR = "#AUTHOR#";
			public const string DATA = "#DATA#";
			public const string SUMMARY = "#SUMMARY#";
			public const string SCRIPT_NAME = "#SCRIPTNAME#";
			public const string NAMESPACE = "#NAMESPACE#";
			public const string PARAM1 = "#PARAM1#";
		};
	}
}
