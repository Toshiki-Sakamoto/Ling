//
// EquipmentUserData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.05
//

using UnityEngine;
using Ling.MasterData.Skill;

namespace Ling.UserData.Equipment
{
	/// <summary>
	/// 装備データ
	/// </summary>
	[System.Serializable]
	public class EquipmentUserData : Utility.GameData.IGameDataBasic
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private int _id;
		[SerializeField] private Const.Equipment.Category _category;
		[SerializeField] private bool _equipped = false;	// 装備中

		private MasterData.Equipment.EquipmentMaster _master;
		private MasterData.Equipment.WeaponMaster _weaponMaster;
		private MasterData.Equipment.ShieldMaster _shieldMaster;

		#endregion


		#region プロパティ

		public int ID { get => _id; set => _id = value; }

		public string Name => Master.Name;

		public Const.Equipment.Category Category { get => _category; set => _category = value; }

		public bool Equipped => _equipped;

		public MasterData.Equipment.EquipmentMaster Master 
		{
			get 
			{
				if (_master != null) return _master;

				var holder = Common.GameManager.Instance.MasterHolder;
				_master = holder.EquipRepositoryContainer.Find(Category, _id);

				return _master;
			}
		}

		public MasterData.Equipment.WeaponMaster WeaponMaster
		{
			get
			{
				if (_weaponMaster != null) return _weaponMaster;

				var holder = Common.GameManager.Instance.MasterHolder;
				_weaponMaster = holder.EquipRepositoryContainer.Weapon.Find(_id);

				return _weaponMaster;
			}
		}

		public MasterData.Equipment.ShieldMaster ShieldMaster
		{
			get
			{
				if (_shieldMaster != null) return _shieldMaster;

				var holder = Common.GameManager.Instance.MasterHolder;
				_shieldMaster = holder.EquipRepositoryContainer.Shield.Find(_id);

				return _shieldMaster;
			}
		}

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 装備を外す
		/// </summary>
		public void Detach()
		{
			_equipped = false;
		}

		/// <summary>
		/// 装着
		/// </summary>
		public void Attach()
		{
			_equipped = true;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
