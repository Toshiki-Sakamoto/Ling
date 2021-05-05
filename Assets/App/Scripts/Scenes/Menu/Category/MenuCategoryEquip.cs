//
// MenuCategoryEquip.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.05
//

using UnityEngine;
using Zenject;
using Ling.UserData;
using System.Linq;
using System.Collections.Generic;

namespace Ling.Scenes.Menu.Category
{
	/// <summary>
	/// 装備一覧
	/// </summary>
	public class MenuCategoryEquip : MenuCategoryBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private IUserDataHolder _userDataHolder = default;

		[SerializeField] private Panel.MenuEquipmentListView _menuEquipmentListView = default;

		private List<UserData.Equipment.EquipmentUserData> _entities;

		#endregion


		#region プロパティ

		/// <summary>
		/// アイテムを使用した時
		/// </summary>
		public System.Action<UserData.Equipment.EquipmentUserData> OnEquipped { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup()
		{
			var repository = _userDataHolder.EquipmentRepository;

			// アイテム一覧を表示する
			_entities = repository.Entities;
			_menuEquipmentListView.Setup(_entities);

			_menuEquipmentListView.OnClick = itemEntity => 
				{
					// アイテムを使用したか装備したか
					OnEquipped?.Invoke(itemEntity);
				};
		}


		/// <summary>
		/// アクティブ状態にする
		/// </summary>
		public override void Activate()
		{
			gameObject.SetActive(true);
			_menuEquipmentListView.gameObject.SetActive(true);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
