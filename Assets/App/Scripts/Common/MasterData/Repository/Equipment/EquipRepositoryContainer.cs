//
// EquipRepositoryContainer.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.05
//

using Ling.MasterData.Equipment;

namespace Ling.MasterData.Repository.Equipment
{
	/// <summary>
	/// 装備マスタコンテナ
	/// </summary>
	public class EquipRepositoryContainer : Utility.MasterData.MasterRepositoryContainer<Const.Equipment.Category, EquipmentMaster>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public WeaponRepository Weapon => FindRepository<WeaponRepository>(Const.Equipment.Category.Weapon);
		public ShieldRepository Shield => FindRepository<ShieldRepository>(Const.Equipment.Category.Shield);

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Update(WeaponRepository weapon, ShieldRepository shield)
		{
			AddRepository(Const.Equipment.Category.Weapon, weapon);
			AddRepository(Const.Equipment.Category.Shield, shield);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
