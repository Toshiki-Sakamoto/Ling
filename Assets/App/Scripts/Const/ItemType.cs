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

			Food,   // 食べ物
			Book,   // 本 (読むと効果が発揮するもの)
			Weapon, // 武器
			Shield, // 盾
		}

		/// <summary>
		/// 食べ物の種類
		/// </summary>
		public enum Food
		{
			Onigiri,   // おにぎり

			Herb,		// ハーブ
		}

		/// <summary>
		/// 本の種類
		/// </summary>
		public enum Book
		{
			Fire,
		}

		/// <summary>
		/// 武器の種類
		/// </summary>
		public enum Weapon
		{
			WoodSword
		}

		/// <summary>
		/// 盾の種類
		/// </summary>
		public enum Shield
		{
			WoodShield,
		}
	}
}
