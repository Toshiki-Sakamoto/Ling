//
// CharaEquipControl.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.09
//

namespace Ling.Chara
{
	/// <summary>
	/// キャラの装備関連の操作を行う
	/// </summary>
	public class CharaEquipControl
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		/// <summary>
		/// 装備中
		/// </summary>
		private MasterData.Equipment.WeaponMaster _weapon;
		private MasterData.Equipment.ShieldMaster _shield;

		private CharaStatus _status;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(CharaStatus status)
		{
			_status = status;

			_status.EquipAttack.SetMax(999);
			_status.EquipDefence.SetMax(999);
		}

		/// <summary>
		/// 武器を装備
		/// </summary>
		public void AttachWeapon(MasterData.Equipment.WeaponMaster weapon)
		{
			if (_weapon != null)
			{
				DetachWeapon();
			}

			_weapon = weapon;

			_status.EquipAttack.AddCurrent(_weapon.Attack);
			_status.EquipDefence.AddCurrent(_weapon.Defense);
		}

		/// <summary>
		/// 盾を装備
		/// </summary>
		public void AttachShield(MasterData.Equipment.ShieldMaster shield)
		{
			if (_shield != null)
			{
				DetachShield();
			}

			_shield = shield;

			_status.EquipAttack.AddCurrent(shield.Attack);
			_status.EquipDefence.AddCurrent(shield.Defense);
		}


		/// <summary>
		/// 武器を外す
		/// </summary>
		public void DetachWeapon()
		{
			if (_weapon == null) 
			{
				Utility.Log.Warning("外す装備がない");
				return;
			}

			_status.EquipAttack.SubCurrent(_weapon.Attack);
			_status.EquipDefence.SubCurrent(_weapon.Defense);
		}

		/// <summary>
		/// 盾を外す
		/// </summary>
		public void DetachShield()
		{
			if (_shield == null) 
			{
				Utility.Log.Warning("外す装備がない");
				return;
			}

			_status.EquipAttack.SubCurrent(_shield.Attack);
			_status.EquipDefence.SubCurrent(_shield.Defense);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
