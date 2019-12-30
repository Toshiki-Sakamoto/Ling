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

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private bool[] _buttonPressed = default;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var buttonsIntValue = 0;
			var enumLength = property.enumNames.Length;
			var labelWidth = EditorGUIUtility.labelWidth;
			var buttonWidth = (position.width - labelWidth) / enumLength;

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

				var buttonPos = new Rect(position.x + labelWidth + buttonWidth, position.y, buttonWidth, position.height);

				_buttonPressed[i] = GUI.Toggle(buttonPos, _buttonPressed[i], property.enumNames[i], "Button");

				if (_buttonPressed[i])
				{
					buttonsIntValue += 1 << i;
				}
			}

			// 変更があれば上書きを行う
			if (EditorGUI.EndChangeCheck())
			{
				property.intValue = buttonsIntValue;
			}
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
		void OnDestoroy()
		{
		}

		#endregion
	}
#endif
}