//
// FieldNameDrawer.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.06.24
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

using Zenject;
using Ling.Utility.Attribute;

namespace Ling.Utility.Editor.Attribute
{
	/// <summary>
	/// <see cref="Common.Attribute.FieldNameAttribute"/>のPropertyDrawer
	/// </summary>
	[UnityEditor.CustomPropertyDrawer(typeof(FieldNameAttribute))]
	public class FieldNameDrawer : UnityEditor.PropertyDrawer
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

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(position, property, new GUIContent(attribute.Name));
		}

		#endregion


		#region private 関数

		private new FieldNameAttribute attribute =>
			base.attribute as FieldNameAttribute;

		#endregion
	}
}
