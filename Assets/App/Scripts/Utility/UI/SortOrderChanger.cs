// 
// SortOrderChanger.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.13
// 

using UnityEngine;
using Ling.Utility.Attribute;

namespace Ling.Utility.UI
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

		private static SortOrderSettings sortOrderSettings = default;

		[SerializeField, SortOrderValueAttribute]
		private string _sortOrderName = default;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			// 設定がない時生成する
			if (sortOrderSettings == null)
			{
				sortOrderSettings = SortOrderSettings.Load();
				if (sortOrderSettings == null)
				{
					Utility.Log.Error("SortOrderの設定ファイルが存在しない");
					return;
				}
			}

			// Canvasを取得
			var canvas = GetComponent<Canvas>();

			// 値を検索する
			var value = sortOrderSettings.Find(_sortOrderName);
			canvas.sortingOrder = value;
		}

		#endregion
	}
}