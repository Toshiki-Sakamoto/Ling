// 
// SortOrderChanger.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.13
// 

using UnityEngine;
using Utility.Attribute;
using System;
using UniRx;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Utility.UI
{
	/// <summary>
	/// 決められた値にSortOrderを変更する
	/// </summary>
	[RequireComponent(typeof(Canvas))]
	public class SortOrderChanger : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField, SortOrderValueAttribute, OnValueChanged(nameof(OnSortOrderNameChanged))]
		private string _sortOrderName = default;

		[ShowInInspector, ReadOnly] private int _value = default;

		#endregion


		#region プロパティ

		public static SortOrderSettings Settings => SortOrderSettings.Settings;

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		private void OnSortOrderNameChanged()
		{
			Apply();
		}

		private void Apply()
		{
			if (Settings == null) return;

			// 値を検索する
			_value = Settings.Find(_sortOrderName);

			// Canvasを取得
			var canvas = GetComponent<Canvas>();
			canvas.sortingOrder = _value;
		}

#if UNITY_EDITOR
		[Button("設定ファイル選択")]
		private void OnClickOpenSettings()
		{
			EditorGUIUtility.PingObject(Settings);
		}
#endif			

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			Apply();
		}

		#endregion
	}
}