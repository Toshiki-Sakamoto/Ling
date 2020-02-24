//
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
			var style = new GUIStyle(GUI.skin.label);
			style.wordWrap = true;

			GUILayout.Label($"◆FilePath \n{filePath}", style);


			EditorGUI.BeginChangeCheck();



			if (EditorGUI.EndChangeCheck())
			{

			}
		}

		#endregion
	}
}
