//
// BattleResult.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.19
//

namespace Ling.Common.Scene.Battle
{
	/// <summary>
	/// BattleScene Result
	/// </summary>
	public class BattleResult : Scene.SceneResult
	{
		#region 定数, class, enum

		// メニューシーンから戻ってきた時
		public enum MenuCategory
		{
			UseItem,	// アイテム使用
			Equip,		// アイテム装備
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public MenuCategory Menu { get; set; }

		/// <summary>
		/// 使用したアイテム情報
		/// </summary>
		public Common.Item.ItemEntity UseItemEntity { get; set; }

		/// <summary>
		/// 装備or外す
		/// </summary>
		public UserData.Equipment.EquipmentUserData EquipEntity { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// アイテムを使用した時
		/// </summary>
		public static BattleResult CreateAtItemUse(Common.Item.ItemEntity itemEntity) =>
			new BattleResult { Menu = MenuCategory.UseItem, UseItemEntity = itemEntity };

		public static BattleResult CreateAtEquip(UserData.Equipment.EquipmentUserData entity) =>
			new BattleResult { Menu = MenuCategory.Equip, EquipEntity = entity };

		#endregion


		#region private 関数

		#endregion
	}
}
