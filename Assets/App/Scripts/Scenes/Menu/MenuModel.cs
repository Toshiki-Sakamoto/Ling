﻿// 
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

		#endregion


		#region プロパティ

		public MenuDefine.Group Group { get; private set; }

		public List<MenuCategoryData> CategoryData { get; private set; } = new List<MenuCategoryData>();

		#endregion


		#region public, protected 関数

		public void SetArgument(MenuArgument argument)
		{
			Group = argument.Group;

			CreateMenuList();
		}

		#endregion


		#region private 関数

		private void CreateMenuList()
		{
			switch (Group)
			{
				case MenuDefine.Group.Menu:
					CategoryData.Add(CreateBagCategoryData());
					break;

				case MenuDefine.Group.Shop:
					break;
			}
		}

		/// <summary>
		/// 持ち物
		/// </summary>
		private MenuCategoryData CreateBagCategoryData()
		{
			var data = new MenuCategoryData(MenuDefine.Category.Bag);

			return data;
		}

		#endregion


		#region MonoBegaviour

		#endregion
	}
}