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

		/// <summary>
		/// アイテムを使用した時
		/// </summary>
		public System.Action<Common.Item.ItemEntity> OnUseItem { get; set; }

		/// <summary>
		/// アイテムを装備した時
		/// </summary>

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup()
		{
			var itemRepository = _userDataHolder.ItemRepository;

			// アイテム一覧を表示する
			_itemEntities = itemRepository.Entities.ConvertAll(entity => (Common.Item.ItemEntity)entity);
			_menuItemListView.Setup(_itemEntities);

			_menuItemListView.OnClick = itemEntity => 
				{
					// アイテムを使用したか装備したか
					OnUseItem?.Invoke(itemEntity);
				};
		}


		/// <summary>
		/// アクティブ状態にする
		/// </summary>
		public override void Activate()
		{
			gameObject.SetActive(true);
			_menuItemListView.gameObject.SetActive(true);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
