//
// EquipmentUserDataRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.05
//

using Ling.UserData.Equipment;
using System.Collections.Generic;
using System.Linq;
using System;
using Ling;

namespace Ling.UserData.Repository
{
	/// <summary>
	/// 装備ユーザーデータ
	/// </summary>
	public class EquipmentUserDataRepository : Utility.UserData.UserDataRepository<EquipmentUserDataRepository, EquipmentUserData>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private IEnumerable<EquipmentUserData> _weapons;
		private IEnumerable<EquipmentUserData> _shileds;

		#endregion


		#region プロパティ

#if DEBUG
		protected override bool EnableDebugMode => false;
#endif

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

#if DEBUG
		protected override void DebugAddFinished()
		{
			var entities = new EquipmentUserData[]
				{
					new EquipmentUserData { ID = 1, Category = Const.Equipment.Category.Weapon },
					new EquipmentUserData { ID = 1, Category = Const.Equipment.Category.Shield },
				};

			Add(entities);
		}
#endif

		/// <summary>
		/// 引数のアイテムを装備させる
		/// </summary>
		public (EquipmentUserData detach, EquipmentUserData attach) Equip(EquipmentUserData target)
		{
			var equippedData = default(EquipmentUserData);

			if (target.Category == Const.Equipment.Category.Weapon)
			{
				equippedData = GetEquippedWeapon();
			}
			else
			{
				equippedData = GetEquippedShield();
			}

			// 装備済みのアイテムであれば装備を外すだけ
			// 装備しているものと違う場合、外す→装着を行う
			equippedData?.Detach();

			if (equippedData != target)
			{
				target.Attach();

				return (equippedData, target);
			}

			// 装着はしなかった
			return (equippedData, null);
		}

		/// <summary>
		/// 装備済みの武器データを返す
		/// </summary>
		public EquipmentUserData GetEquippedWeapon() =>
			_weapons.FirstOrDefault(entity => entity.Equipped);

		public EquipmentUserData GetEquippedShield() =>
			_shileds.FirstOrDefault(entity => entity.Equipped);

		public override void OnFirstLoad()
		{
			var entities = new EquipmentUserData[]
				{
					new EquipmentUserData { ID = 1, Category = Const.Equipment.Category.Weapon, Uniq = Utility.UniqKey.Create() },
					new EquipmentUserData { ID = 1, Category = Const.Equipment.Category.Shield, Uniq = Utility.UniqKey.Create() },
				};

			Add(entities);
		}

		#endregion


		#region private 関数
		protected override void AddFinishedInternal()
		{
			// 武器と盾に分ける
			_weapons = Entities.Where(entity => entity.Category == Const.Equipment.Category.Weapon);
			_shileds = Entities.Where(entity => entity.Category == Const.Equipment.Category.Shield);
		}

		#endregion
	}
}
