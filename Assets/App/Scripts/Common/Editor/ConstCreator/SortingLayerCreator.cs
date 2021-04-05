//
// SortingLayerCreator.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.19
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Ling.Common.Editor.DefineCreator
{
	/// <summary>
	/// SortingLayerの定数スクリプトを作成する
	/// </summary>
	public class SortingLayerCreator
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		[MenuItem("Tools/DefineCreator/SortingLayer")]
		public static void Create()
		{
			var param = new ConstScriptCreator.Param<string>();
			param.needsInsertComments = false;

			var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset"));
			var sortingLayerProperty = tagManager.FindProperty("m_SortingLayers");

			for (int i = 0; i < sortingLayerProperty.arraySize; ++i)
			{
				var tag = sortingLayerProperty.GetArrayElementAtIndex(i);

				// 名前と値を同じものを入れる
				param.constPairs.Add(tag.displayName, tag.displayName);
			}

			// 定数作成クラスにあとは任せる
			ConstScriptCreator.Create("SortingLayer", "SortingLayer名", param);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
