// 
// QuickStart.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.19
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Linq;

namespace Ling.Common.Editor
{
	/// <summary>
	/// ショートカット
	/// </summary>
	public static class ShortCut
	{
		/// <summary>
		/// StartUpシーンを起動する
		/// </summary>
		[MenuItem("Tools/ShortCut/Quick Start %Q")]
		public static void QuickStart()
		{
			// 現在のシーンに変更が入っていたら保存するか確認
			if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				// 特定シーンを開く
				var guid = AssetDatabase.FindAssets("t:Scene Startup").First();
				var filePath = AssetDatabase.GUIDToAssetPath(guid);

				EditorSceneManager.OpenScene(filePath);

				// 実行
				EditorApplication.ExecuteMenuItem("Edit/Play");
			}
		}
	}
}