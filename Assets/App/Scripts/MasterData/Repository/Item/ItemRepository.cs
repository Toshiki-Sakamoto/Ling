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

		public ItemRepository()
		{
			Utility.EventManager.SafeAdd<MasterLoadedEvent>(this, ev =>
				{
					var manager = ev.Manager;
					
					Book = manager.BookRepository;
					Food = manager.FoodRepository;

					Update(Const.Item.Category.Book, Book);
					Update(Const.Item.Category.Food, Food);
				});
		}

		~ItemRepository()
		{
			Utility.EventManager.SafeAllRemove(this);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
