// 
// MenuModel.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.09
// 

using UnityEngine;
using System;
using System.Collections.Generic;
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

		private MenuCategoryDataFactory _categoryDataFactory = new MenuCategoryDataFactory();

		#endregion


		#region プロパティ

		public MenuDefine.Group Group { get; private set; }

		public List<MenuCategoryData> CategoryData { get; private set; } = new List<MenuCategoryData>();

		public int SelectedCategoryIndex { get; private set; }

		public MenuCategoryData SelectedCategoryData => CategoryData[SelectedCategoryIndex];

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
		}

		#endregion


		#region private 関数

		private void CreateMenuList()
		{
			switch (Group)
			{
				case MenuDefine.Group.Menu:
					CategoryData.Add(_categoryDataFactory.Create(MenuDefine.Category.Bag));
					CategoryData.Add(_categoryDataFactory.Create(MenuDefine.Category.Setting));
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