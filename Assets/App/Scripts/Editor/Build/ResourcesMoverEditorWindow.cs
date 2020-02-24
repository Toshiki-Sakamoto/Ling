﻿//
// ResourceMoverEditorWindow.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.02.23
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Ling.Editor.Build
{
	/// <summary>
	/// 
	/// </summary>
	public class ResourcesMoverEditorWindow : EditorWindow
    {
		#region 定数, class, enum
		#endregion


		#region public, protected 変数

		/// <summary>
		/// 設定用のScriptableObject
		/// </summary>
		public static ResourcesMoverSetting setting;

		/// <summary>
		/// ScriptableObjectのファイルパス
		/// </summary>
		public static string filePath;

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		[MenuItem("Tools/ResourceMove")]
		public static void Open()
		{
			GetWindow<ResourcesMoverEditorWindow>();

			//Utility.Log.Print(sourceFilePath);
			//			setting = AssetDatabase.LoadAssetAtPath<ResourcesMoverSetting>("");

			var guid = AssetDatabase.FindAssets("t:" + nameof(ResourcesMoverSetting)).FirstOrDefault();
			filePath = AssetDatabase.GUIDToAssetPath(guid);
			if (filePath == null)
			{
				return;
			}

			setting = AssetDatabase.LoadAssetAtPath<ResourcesMoverSetting>(filePath);
		}

		#endregion


		#region private 関数

		private void OnGUI()
		{
			var labelStyle = new GUIStyle(GUI.skin.label);
			labelStyle.wordWrap = true;


			using (new GUILayout.VerticalScope(GUI.skin.box))
			{
				GUILayout.Label("設定ファイルパス");
				GUILayout.Label(filePath, labelStyle);
			}

			GUILayout.Space(20);

			using (new GUILayout.VerticalScope(GUI.skin.box))
			{
				using (new GUILayout.HorizontalScope())
				{
					GUILayout.Label($"移動先フォルダ");

					if (GUILayout.Button("選択"))
					{
						var selectedPath = ToAssetPath(EditorUtility.OpenFolderPanel("選択", "Assets", ""));
						if (!string.IsNullOrEmpty(selectedPath))
						{
							setting.destinationPath = selectedPath;
						}
					}
				}

				GUILayout.Label(setting.destinationPath, labelStyle);
			}

			GUILayout.Space(20);

			using (new GUILayout.VerticalScope(GUI.skin.box))
			{
				GUILayout.Label($"移動フォルダリスト");

				var sourceDirectories = setting.sourceDirectories;

				for (int i = 0; i < sourceDirectories.Count; ++i)
				{
					var path = sourceDirectories[i];

					using (new GUILayout.HorizontalScope(GUI.skin.box))
					{
						GUILayout.Label(path);

						GUILayout.FlexibleSpace();

						if (GUILayout.Button("選択"))
						{
							sourceDirectories[i] = ToAssetPath(EditorUtility.OpenFolderPanel("選択", "Assets", ""));
						}

						if (GUILayout.Button("削除"))
						{
							sourceDirectories.RemoveAt(i);
						}
					}
				}

				// 追加
				if (GUILayout.Button("追加"))
				{
					var path = ToAssetPath(EditorUtility.OpenFolderPanel("選択", "Assets", ""));

					sourceDirectories.Add(path);
				}
			}

			GUILayout.Space(20);

			// 保存
			if (GUILayout.Button("保存"))
			{ 
				EditorUtility.SetDirty(setting);

				AssetDatabase.SaveAssets();
			}
		}


		private string ToAssetPath(string fullPath)
		{
			var match = Regex.Match(fullPath, "Assets/.*");
			if (match == null)
			{

			}

			return match.Value;
		}

		#endregion
	}
}
