//
// EquipmentMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.05
//

using UnityEngine;
using Utility.Attribute;
using Utility.MasterData;

namespace Ling.MasterData.Equipment
{
	/// <summary>
	/// 装備マスタの基本
	/// アイテムであることにするのでItemMasterを継承している
	/// </summary>
	public abstract class EquipmentMaster : Item.ItemMaster
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("攻撃力")]
		private int _attack = default;

		[SerializeField, FieldName("防御力")]
		private int _defense = default;

		#endregion


		#region プロパティ

		public abstract Const.Equipment.Category Type { get; }

		public int Attack => _attack;
		public int Defense => _defense;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
