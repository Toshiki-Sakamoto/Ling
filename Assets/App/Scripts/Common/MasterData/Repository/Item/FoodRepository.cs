//
// FoodRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.09
//


using Ling.MasterData.Item;

namespace Ling.MasterData.Repository.Item
{
	/// <summary>
	/// FoodMaster Repository
	/// </summary>
	public class FoodRepository : Utility.MasterData.InheritanceMasterRepository<ItemMaster, FoodMaster>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public FoodMaster Find(Const.Item.Food type) =>
			(FoodMaster)Entities.Find(entity => ((FoodMaster)entity).Type == type);


		#endregion


		#region private 関数

		#endregion
	}
}
