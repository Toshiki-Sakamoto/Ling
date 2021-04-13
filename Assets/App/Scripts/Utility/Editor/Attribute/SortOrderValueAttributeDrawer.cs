using System;
using System.Linq;
// 
// SortOrderValueAttributeDrawer.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.13
// 

using Ling.Utility.Attribute;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Ling.Utility.UI;

namespace Ling.Utility.Editor.Attribute
{
	/// <summary>
	/// SortOrderValueAttributeの表示担当
	/// ドロップダウンで切り替えられるようにする
	/// </summary>
	[CustomPropertyDrawer(typeof(SortingLayerAttribute))]
	public class SortOrderValueAttributeDrawer : PropertyDrawer 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private static string[] names = null;

		#endregion


		#region プロパティ

		/// <summary>
		/// 現在保存されている文字列を返す
		/// </summary>
		public static string[] Names
		{
			get
			{
				if (names != null) return names;

				var settings = SortOrderSettings.Load();
				names = settings.Data.Select(data => data.Name).ToArray();

				return names;
			}
		}

		#endregion


		#region public, protected 関数

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var names = Names;
			if (names.Length <= 0) 
			{
				return;
			}
			
			var selectedIndex = Array.FindIndex(names, name => name.Equals(property.stringValue));
			if (selectedIndex == -1)
			{
				// 一番目の値にする
				selectedIndex = 0;
			}

			selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, names);

			property.stringValue = names[selectedIndex];
		}

		#endregion


		#region private 関数

		#endregion
	}
}