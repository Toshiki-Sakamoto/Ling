//
// ItemType.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.07
//

namespace Ling.Const
{
	/// <summary>
	/// Item 定数
	/// </summary>
	public static class Item
	{
		/// <summary>
		/// アイテムの種類
		/// </summary>
		public enum Category
		{
			None,

			Food,	// 食べ物
			Book,	// 本 (読むと効果が発揮するもの)
			Weapon,	// 武器
			Shield,	// 盾
		}

		/// <summary>
		/// 食べ物の種類
		/// </summary>
		public enum Food
		{
			None, 
			
			Apple,	// りんご
		}

		/// <summary>
		/// 本の種類
		/// </summary>
		public enum Book
		{
			None, 
		}

		/// <summary>
		/// 武器の種類
		/// </summary>
		public enum Weapon
		{
			None, 
		}

		/// <summary>
		/// 盾の種類
		/// </summary>
		public enum Shield
		{
			None, 
		}
	}
}
