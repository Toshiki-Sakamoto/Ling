//
// MenuCategoryDataFactory.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.11
//

namespace Ling.Scenes.Menu
{
	/// <summary>
	/// MenuCategoryData
	/// </summary>
	public class MenuCategoryDataFactory
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

		public MenuCategoryData Create(MenuDefine.Category category)
		{
			switch (category)
			{
				case MenuDefine.Category.Bag:
					return CreateBagCategoryData();

				case MenuDefine.Category.Shop:
					return CreateShopCategoryData();

				case MenuDefine.Category.Setting:
					return CreateSettingCategoryData();

				default:
					Utility.Log.Error($"Categoryが不正 {category.ToString()}");
					return null;
			}
		}

		#endregion


		#region private 関数



		/// <summary>
		/// 持ち物
		/// </summary>
		private MenuCategoryData CreateBagCategoryData()
		{
			var data = new MenuCategoryData(MenuDefine.Category.Bag);

			return data;
		}

		/// <summary>
		/// 買う
		/// </summary>
		private MenuCategoryData CreateShopCategoryData()
		{
			var data = new MenuCategoryData(MenuDefine.Category.Shop);

			return data;
		}

		/// <summary>
		/// 設定
		/// </summary>
		private MenuCategoryData CreateSettingCategoryData()
		{
			var data = new MenuCategoryData(MenuDefine.Category.Setting);

			return data;
		}

		#endregion
	}
}
