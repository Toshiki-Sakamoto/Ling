//
// MinMaxRangeAttributeDrawer.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.06.29
//

using Ling.Utility.Attribute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using MinMaxAttribute = Ling.Utility.Attribute.MinMaxAttribute;

namespace Ling.Utility.Editor.Attribute
{
	/// <summary>
	/// <see cref="Common.Attribute.MinMaxAttribute"/>の描画
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

			// Sliderの変更
			EditorGUI.BeginChangeCheck();

			// 変数名(Default)を出すかどうか
			if (!string.IsNullOrEmpty(attribute.FieldName))
			{
				label = new GUIContent(attribute.FieldName);
			}

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

			// インデントを一度リセットする
			int indentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			var rects = Utility.Editor.Drawer.SplitHorizontalRects(labelRect, 2, 10);

			// PropertyFieldを描画する
			void DrawPropertyField(Rect position_, SerializedProperty property_, System.Action onChange_)
			{
				var displayName = property_.displayName;

				EditorGUI.BeginChangeCheck();

				var childLabel = new GUIContent(displayName);

				// ラベルの幅
				EditorGUIUtility.labelWidth = GUI.skin.label.CalcSize(childLabel).x;

				// プロパティの変更
				EditorGUI.PropertyField(position_, property_, childLabel);

				if (EditorGUI.EndChangeCheck())
				{
					onChange_?.Invoke();
				}
			}

			// Min
			DrawPropertyField(rects[0], minProperty, 
				() => 
				{ 
					if (isFloatMin)
					{
						minProperty.floatValue = Mathf.Clamp(minProperty.floatValue, attribute.Min, maxProperty.floatValue);
					}
					else
					{
						minProperty.intValue = Mathf.Clamp(minProperty.intValue, Mathf.FloorToInt(attribute.Min), maxProperty.intValue);
					}
				});

			// Max
			DrawPropertyField(rects[1], maxProperty, 
				() =>
				{
					if (isFloatMax)
					{
						maxProperty.floatValue = Mathf.Clamp(maxProperty.floatValue, minProperty.floatValue, attribute.Max);
					}
					else
					{
						maxProperty.intValue = Mathf.Clamp(maxProperty.intValue, minProperty.intValue, Mathf.FloorToInt(attribute.Max));
					}
				});

			EditorGUI.indentLevel = indentLevel;
		}

		/// <summary>
		/// 高さ
		/// </summary>
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label) * 2;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
