//
// MenuCategoryBag.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.26
//

using UnityEngine;
using Zenject;
using Ling.UserData;
using System.Linq;
using System.Collections.Generic;

namespace Ling.Scenes.Menu.Category
{
	/// <summary>
	/// 持ち物カテゴリー
	/// </summary>
	public class MenuCategoryBag : MenuCategoryBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private IUserDataHolder _userDataHolder = default;

		[SerializeField] private Panel.MenuItemListView _menuItemListView = default;

		private List<Common.Item.ItemEntity> _itemEntities;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup()
		{
			var itemRepository = _userDataHolder.ItemRepository;

			// アイテム一覧を表示する
			_itemEntities = itemRepository.Entities.ConvertAll(entity => entity.Entity);
			_menuItemListView.Setup(_itemEntities);
		}


		/// <summary>
		/// アクティブ状態にする
		/// </summary>
		public override void Activate()
		{
			gameObject.SetActive(true);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
