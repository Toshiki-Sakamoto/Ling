//
// ItemRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.21
//

using Ling.MasterData.Item;
using Utility.MasterData;
using Zenject;

namespace Ling.MasterData.Repository.Item
{
	/// <summary>
	/// アイテム全てをまとめたリポジトリ
	/// </summary>
	/// <remarks>
	/// カテゴリごとに検索することも可能
	/// </remarks>
	public class ItemRepositoryContainer : Utility.MasterData.MasterRepositoryContainer<Const.Item.Category, ItemMaster>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public BookRepository Book => FindRepository<BookRepository>(Const.Item.Category.Book);
		public FoodRepository Food => FindRepository<FoodRepository>(Const.Item.Category.Food);

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Update(BookRepository book, FoodRepository food)
		{
			AddRepository(Const.Item.Category.Book, book);
			AddRepository(Const.Item.Category.Food, food);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
