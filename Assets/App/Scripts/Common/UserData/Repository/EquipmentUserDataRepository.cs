//
// EquipmentUserDataRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.05
//

using Ling.UserData.Equipment;

namespace Ling.Common.UserData.Repository
{
	/// <summary>
	/// 装備ユーザーデータ
	/// </summary>
	public class EquipmentUserDataRepository : Utility.UserData.UserDataRepository<EquipmentUserData>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

#if DEBUG
		protected override bool EnableDebugMode => true; // todo: 強制ON
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

		#endregion


		#region private 関数

		#endregion
	}
}
