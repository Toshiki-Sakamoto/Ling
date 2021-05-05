//
// WeaponMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.05
///

using UnityEngine;
using Utility.Attribute;

namespace Ling.MasterData.Equipment
{
	/// <summary>
	/// 武器マスタ
	/// </summary>
	[CreateAssetMenu(menuName = "MasterData/WeaponMaster", fileName = "WeaponMaster")]
	public class WeaponMaster : EquipmentMaster
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public override Const.Equipment.Category Type => Const.Equipment.Category.Weapon;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
