// 
// SortingLayerAttributeDrawer.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.07.15
// 
using Ling.Utility.Attribute;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using Zenject;


namespace Ling.Utility.Editor.Attribute
{
	/// <summary>
	/// <see cref="SortingLayerAttribute"/>を描画する
	/// ドロップダウンで切り替えられるように
	/// </summary>
	[CustomPropertyDrawer(typeof(SortingLayerAttribute))]
	public class SortingLayerAttributeDrawer : PropertyDrawer
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private static SerializedProperty sortingLayer = null;
		private static List<string> layerNameList = null;

		#endregion


		#region プロパティ

		/// <summary>
		/// 保存されているSortingLayerを返す
		/// </summary>
		public static SerializedProperty SortingLayer
		{
			get
			{
				if (sortingLayer == null)
				{
					var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset"));
					sortingLayer = tagManager.FindProperty("m_SortingLayers");
				}

				return sortingLayer;
			}
		}


		private List<string> AllSoringLayer
		{
			get
			{
				if (layerNameList != null) return layerNameList;

				layerNameList = new List<string>();

				for (int i = 0; i < SortingLayer.arraySize; ++i)
				{
					var tag = SortingLayer.GetArrayElementAtIndex(i);
					layerNameList.Add(tag.displayName);
				}

				return layerNameList;
			}
		}

		#endregion


		#region public, protected 関数

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var list = AllSoringLayer;
			var selectedIndex = list.FindIndex(tag => tag.Equals(property.stringValue));
			if (selectedIndex == -1)
			{
				selectedIndex = list.FindIndex(tag => tag.Equals("Default"));
			}

			selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, list.ToArray());

			property.stringValue = list[selectedIndex];
		}

		#endregion


		#region private 関数


		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
		}

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update()
		{
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		void OnDestroy()
		{
		}

		#endregion
	}
}