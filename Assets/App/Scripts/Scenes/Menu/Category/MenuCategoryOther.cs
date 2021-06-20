//
// MenuCategoryOther.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.20
//

using UnityEngine;
using Zenject;
using Ling.UserData;
using System.Collections.Generic;
using UniRx;

namespace Ling.Scenes.Menu.Category
{
	/// <summary>
	/// その他
	/// </summary>
	public class MenuCategoryOther : MenuCategoryBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Utility.UserData.IUserDataManager _userDataManager = default;
		[Inject] private Utility.IEventManager _eventManager = default;

		[SerializeField] private Panel.MenuOtherView _view = default;


		#endregion


		#region プロパティ

		/// <summary>
		/// アイテムを使用した時
		/// </summary>
//		public System.Action<UserData.Equipment.EquipmentUserData> OnEquipped { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup()
		{
			_view.Setup();

			_view.SaveButton
				.OnClickAsObservable()
				.Subscribe(_ => 
				{
					// 現在のUserDataすべてを保存する
					_userDataManager.SaveAll();
					
					_eventManager.Trigger(new Utility.SaveData.EventSaveCall());
				})
				.AddTo(this);;
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
