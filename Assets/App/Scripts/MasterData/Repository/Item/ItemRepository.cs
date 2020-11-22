//
// ItemRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.21
//

using Ling.MasterData.Item;
using System.Collections.Generic;

namespace Ling.MasterData.Repository.Item
{
	/// <summary>
	/// アイテム全てをまとめたリポジトリ
	/// </summary>
	/// <remarks>
	/// カテゴリごとに検索することも可能
	/// </remarks>
	public class ItemRepository : Common.Repotitory.RepositoryContainer<Const.Item.Category, ItemMaster>
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public BookRepository Book { get; private set; }
		public FoodRepository Food { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(BookRepository book, FoodRepository food)
		{
			Book = book;
			Food = food;

			Add(Const.Item.Category.Book, Book);
			Add(Const.Item.Category.Food, Food);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
