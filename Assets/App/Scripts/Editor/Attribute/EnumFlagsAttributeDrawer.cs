// 
// EnumFlagsAttributeDrawer.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2019.12.30
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Ling.Editor.Attribute
{
	/// <summary>
	/// Attributeを作成
	/// </summary>
	/// <remarks>
	/// sealed class は以降どんなクラスも継承できなくなる
	/// </remarks>
	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
	public sealed class EnumFlagsAttribute : PropertyAttribute { }


	/// <summary>
	/// 列挙型をInspectorから簡単に設定できるように
	/// PropertyDrawerはCunstomEditorと違い、特定のクラス全体の見た目をカスタマイズ
	/// するのではなく、特定のプロパティの見た目をカスタマイズする
	/// </summary>
	/// <remarks>
	/// 参考サイト
	/// http://kan-kikuchi.hatenablog.com/entry/BitFlag
	/// </remarks>
#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
	public class EnumFlagsAttributeDrawer : PropertyDrawer
	{
		#region 定数, class, enum

		public const int ButtonMinWidth = 80;

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private bool[] _buttonPressed = default;
		private int _buttonLineNum = 0;	// ボタンの行数

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var labelWidth = EditorGUIUtility.labelWidth;

			if ((position.width - labelWidth) < 0) return;

			var buttonsValue = 0L;
			var enumLength = property.enumNames.Length;
			var buttonHeight = EditorGUIUtility.singleLineHeight;
			var buttonWidth = Mathf.Max((position.width - labelWidth) / enumLength, ButtonMinWidth);
			var buttonLineElmNum = (int)((position.width - labelWidth) / ButtonMinWidth);

			_buttonLineNum = Mathf.Max(enumLength / buttonLineElmNum + 1, 1);

			if (_buttonPressed == null || _buttonPressed.Length != enumLength)
			{
				_buttonPressed = new bool[enumLength];
			}

			var labelPos = new Rect(position.x, position.y, labelWidth, position.height);

			EditorGUI.LabelField(labelPos, label);
			EditorGUI.BeginChangeCheck();

			for (int i = 0; i < enumLength; ++i)
			{
				_buttonPressed[i] = (property.intValue & 1 << i) != 0;

				var buttonPosY = position.y + buttonHeight * (i / buttonLineElmNum);
				var buttonPos = new Rect(position.x + labelWidth + buttonWidth * (i % buttonLineElmNum), buttonPosY, buttonWidth, buttonHeight);

				_buttonPressed[i] = GUI.Toggle(buttonPos, _buttonPressed[i], property.enumNames[i], "Button");

				if (_buttonPressed[i])
				{
					buttonsValue += 1L << i;
				}
			}

			// 変更があれば上書きを行う
			if (EditorGUI.EndChangeCheck())
			{
				property.longValue = buttonsValue;
			}
		}



		/// <summary>
		/// 指定プロパティの高さを決める
		/// </summary>
		/// <param name="property"></param>
		/// <param name="label"></param>
		/// <returns></returns>
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight * _buttonLineNum;
		}

		#endregion


		#region private 関数

		#endregion

	}
#endif
}