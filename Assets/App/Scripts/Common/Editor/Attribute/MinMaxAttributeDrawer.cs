//
// MinMaxRangeAttributeDrawer.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.06.29
//

using Ling.Common.Attribute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Utage;
using Zenject;
using MinMaxAttribute = Ling.Common.Attribute.MinMaxAttribute;

namespace Ling.Common.Editor.Attribute
{
	/// <summary>
	/// <see cref="Common.Attribute.MinMaxRangeAttribute"/>の描画
	/// </summary>
	[CustomPropertyDrawer(typeof(MinMaxAttribute))]
	public class MinMaxAttributeDrawer : PropertyDrawer
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		private new MinMaxAttribute attribute =>
			base.attribute as MinMaxAttribute;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var minProperty = property.FindPropertyRelative(attribute.MinPropertyName);
			var maxProperty = property.FindPropertyRelative(attribute.MaxPropertyName);

			var sliderRect = new Rect(position) { height = position.height * 0.5f };
			var labelRect = new Rect(sliderRect) 
				{ 
					x = sliderRect.x + EditorGUIUtility.labelWidth,
					y = sliderRect.y + sliderRect.height,
					width = sliderRect.width - EditorGUIUtility.labelWidth
				};


			bool isFloatMin = (minProperty.propertyType == SerializedPropertyType.Float);
			bool isFloatMax = (maxProperty.propertyType == SerializedPropertyType.Float);

			float min = isFloatMin ? minProperty.floatValue : minProperty.intValue;
			float max = isFloatMin ? maxProperty.floatValue : maxProperty.intValue;

			EditorGUI.BeginChangeCheck();

			EditorGUI.MinMaxSlider(sliderRect, label, ref min, ref max, attribute.Min, attribute.Max);

			if (EditorGUI.EndChangeCheck())
			{
				if (isFloatMin)
				{
					minProperty.floatValue = min;
				}
				else
				{
					minProperty.intValue = Mathf.FloorToInt(min);
				}

				if (isFloatMax)
				{
					maxProperty.floatValue = max;
				}
				else
				{
					maxProperty.intValue = Mathf.FloorToInt(max);
				}
			}

			// 
			int indentLevel = EditorGUI.indentLevel;

		//
			EditorGUI.indentLevel = 0;

		}

		#endregion


		#region private 関数

		#endregion
	}
}
