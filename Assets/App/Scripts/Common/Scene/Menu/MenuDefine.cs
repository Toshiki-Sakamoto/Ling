//
// MenuDefine.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.11
//

namespace Ling.Common.Scene.Menu
{
	/// <summary>
	/// 定数
	/// </summary>
	public class MenuDefine
	{
		/// <summary>
		/// メニューグループ
		/// </summary>
		public enum Group
		{
			Menu, 
			Shop,
		}

		/// <summary>
		/// 各要素のカテゴリ
		/// </summary>
		public enum Category
		{
			Bag,		// 持ち物(今はアイテムのみとする)
			Equip,		// 装備一覧
			Shop,		// ショップ:買う
			Other,		// その他
		}
	}

	public static class MenuDefineExtensions
	{
		public static string GetCategoryName(this MenuDefine.Category self)
		{
			switch (self)
			{
				case MenuDefine.Category.Bag: return "もちもの";
				case MenuDefine.Category.Equip: return "装備";
				case MenuDefine.Category.Shop: return "ショップ";
				case MenuDefine.Category.Other: return "その他";

				default: return "None";
			}
		}
	}
}
