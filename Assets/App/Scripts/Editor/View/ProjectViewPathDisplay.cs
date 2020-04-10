//
// ProjectViewDisplayPath.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.03.14
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Editor.View
{
	/// <summary>
	/// Project View にファイルのパスを表示する拡張機能
	/// </summary>
	[InitializeOnLoad]
	public class ProjectViewPathDisplay
	{
		#region 定数, class, enum

		private const string MenuItemPath = "Tools/View/ProjectViewPathDisplay/パス表示";
		private const string PrefsKey = "ProjectViewPathDisplay";

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private static GUIStyle _style;
		private static bool _enable;
		private static List<string> _removePathLists = new List<string> 
			{ 
				@"Assets\",
				@"App\",
				@"Scripts\",
			};

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		static ProjectViewPathDisplay()
		{
			EditorApplication.projectWindowItemOnGUI -= OnGUI;
			EditorApplication.projectWindowItemOnGUI += OnGUI;

			_enable = EditorPrefs.GetBool(PrefsKey, false);
		}

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		private static void OnGUI(string guid, Rect selectionRect)
		{
			if (!_enable) return;

			var assetPath = AssetDatabase.GUIDToAssetPath(guid);

			if (string.IsNullOrWhiteSpace(assetPath)) return;

			// ディレクトは除外する
			if (Directory.Exists(assetPath)) return;

			// ファイル名
			var filename = Path.GetFileNameWithoutExtension(assetPath);

			if (_style == null)
			{
				_style = new GUIStyle(EditorStyles.label);
				_style.normal.textColor = new Color(0.4f, 0.4f, 0.4f);
			}

			selectionRect.x += _style.CalcSize(new GUIContent(filename)).x + 30;


			// ディレクトリパスにする
			assetPath = Path.GetDirectoryName(assetPath);

			// いらないパスは削除
			foreach (var path in _removePathLists)
			{
				assetPath = assetPath.Replace(path, "");
			}

			GUI.Label(selectionRect, assetPath, _style);
		}


		/// <summary>
		/// Windowメニューに追加。
		/// 表示非表示を切り替える
		/// </summary>
		[MenuItem(MenuItemPath)]
		private static void ShowWindow()
		{
			var menuChecked = Menu.GetChecked(MenuItemPath);
			_enable = !menuChecked;

			Menu.SetChecked(MenuItemPath, _enable);

			// 保存
			EditorPrefs.SetBool(PrefsKey, _enable);
		}

		#endregion
	}
}
