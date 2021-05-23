// 
// MenuModel.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.09
// 

using UnityEngine;
using System;
using System.Collections.Generic;
using Ling.Common.Scene.Menu;
using UniRx;

namespace Ling.Scenes.Menu
{
	/// <summary>
	/// Menum Model
	/// </summary>
	public class MenuModel : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public MenuDefine.Group Group { get; private set; }

		public List<MenuCategoryData> CategoryData { get; private set; } = new List<MenuCategoryData>();

		public int SelectedCategoryIndex { get; private set; }

		/// <summary>
		/// 現在選択されているカテゴリデータ
		/// </summary>
		public ReactiveProperty<MenuCategoryData> SelectedCategoryData { get; } = new ReactiveProperty<MenuCategoryData>();

		#endregion


		#region public, protected 関数

		public void SetArgument(MenuArgument argument)
		{
			Group = argument.Group;

			CreateMenuList();
		}

		public void SetSelectedCategoryIndex(int index)
		{
			SelectedCategoryIndex = index;

			SelectedCategoryData.Value = CategoryData[SelectedCategoryIndex];
		}

		#endregion


		#region private 関数

		private void CreateMenuList()
		{
			switch (Group)
			{
				case MenuDefine.Group.Menu:
					CategoryData.Add(new MenuCategoryData(MenuDefine.Category.Bag));
					CategoryData.Add(new MenuCategoryData(MenuDefine.Category.Equip));
					CategoryData.Add(new MenuCategoryData(MenuDefine.Category.Other));
					break;

				case MenuDefine.Group.Shop:
					break;
			}
		}

		#endregion


		#region MonoBegaviour

		#endregion
	}
}