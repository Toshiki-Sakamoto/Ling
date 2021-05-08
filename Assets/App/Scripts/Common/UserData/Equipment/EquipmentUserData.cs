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
	public class EquipmentUserData : Utility.GameData.IGameDataBasic
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private int _id;
		[SerializeField] private Const.Equipment.Category _category;

		private MasterData.Equipment.EquipmentMaster _master;

		#endregion


		#region プロパティ

		public int ID { get => _id; set => _id = value; }

		public string Name => Master.Name;

		public Const.Equipment.Category Category { get => _category; set => _category = value; }

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

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
