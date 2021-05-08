// 
// MenuScene.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.11.07
// 

using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using Ling.Common.Scene.Menu;
using Zenject;
using Ling.Common.Input;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Ling.Common.Scene.Battle;

namespace Ling.Scenes.Menu
{
	/// <summary>
	/// Menu Scene
	/// </summary>
	public class MenuScene : Common.Scene.Base
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[Inject] private IInputManager _inputManager;

		[SerializeField] private MenuModel _model = default;
		[SerializeField] private MenuView _view = default;

		[Header("メニューカテゴリコントロール")]
		[SerializeField] private Category.MenuCategoryBag _bagControl = default;
		[SerializeField] private Category.MenuCategoryEquip _equipControl = default;

		private List<Category.MenuCategoryBase> _categoryControls = new List<Category.MenuCategoryBase>();

		#endregion


		#region プロパティ

		public override Common.Scene.SceneID SceneID => Common.Scene.SceneID.Menu;

		#endregion


		#region public, protected 関数

		/// <summary>
		/// シーンが開始される時
		/// </summary>
		public override void StartScene() 
		{
			var menuArgument = Argument as MenuArgument;

			_model.SetArgument(menuArgument);

			// カテゴリに応じたクラスを生成する
			foreach (var data in _model.CategoryData)
			{
				SetupCategoryControl(data);
			}


			var viewParam = new MenuView.Param()
				{
					CategoryData = _model.CategoryData,
				};

			_view.Setup(viewParam);

			// View上でカテゴリが変更された
			_view.SelectedIndex
				.Subscribe(index => 
				{
					_model.SetSelectedCategoryIndex(index);

					//_view.SetCategoryData(_model.SelectedCategoryData);
				}).AddTo(this);

			// カテゴリが切り替わった時、カテゴリコントロール上も変化させる
			_model.SelectedCategoryData
				.AsObservable()
				.Subscribe(categoryData => 
				{
					// Controlを変更させる
					ActivateCategoryControl(categoryData);
				});

			// メニューボタンが押されたら閉じる
			var actionInput = _inputManager.Resolve<InputControls.IActionActions>();
			actionInput.Controls.Action.Menu.performed += OnMenuPerformed;

			// 閉じるボタン
			_view.CloseButton.OnClickAsObservable()
				.Subscribe(_ => 
				{
					// 自分をクローズする
					_sceneManager.CloseScene(this);

				}).AddTo(this);
		}

		/// <summary>
		/// StartScene後呼び出される
		/// </summary>
		public override void UpdateScene() 
		{ }

		/// <summary>
		/// シーン終了時
		/// </summary>
		public override void StopScene() 
		{ 
			var actionInput = _inputManager.Resolve<InputControls.IActionActions>();
			actionInput.Controls.Action.Menu.performed -= OnMenuPerformed;
		}

		/// <summary>
		/// 正規手順でシーンが実行されたのではなく
		/// 直接起動された場合StartSceneよりも前に呼び出される
		/// </summary>
		public override UniTask QuickStartSceneAsync()
		{
			Argument = MenuArgument.CreateAtMenu();

			return default(UniTask);
		}

		#endregion


		#region private 関数

		private void OnMenuPerformed(InputAction.CallbackContext context)
		{
			// メニューボタンが押されたら閉じる
			CloseScene();
		}

		/// <summary>
		/// カテゴリに応じたControlクラスのSetupを呼び出す
		/// </summary>
		private void SetupCategoryControl(MenuCategoryData categoryData)
		{
			switch (categoryData.Category)
			{
				case MenuDefine.Category.Bag:
					_bagControl.Setup();
					_categoryControls.Add(_bagControl);

					// アイテムを使用した時
					_bagControl.OnUseItem = itemEntity => UseItem(itemEntity);
					break;

				// 装備一覧
				case MenuDefine.Category.Equip:
					_equipControl.Setup();
					_categoryControls.Add(_equipControl);

					break;
			}
		}

		private void ActivateCategoryControl(MenuCategoryData categoryData)
		{
			InactiveAllCategory();					

			switch (categoryData.Category)
			{
				case MenuDefine.Category.Bag:
					_bagControl.Activate();
					break;
			}
		}

		private void InactiveAllCategory()
		{
			foreach (var control in _categoryControls)
			{
				control.gameObject.SetActive(false);
			}
		}

		/// <summary>
		/// アイテムを使用する
		/// </summary>
		private void UseItem(Common.Item.ItemEntity itemEntity)
		{
			Utility.Log.Print($"アイテムを使用する {itemEntity.ID}, {itemEntity.Name}");

			// シーンを戻る
			var result = BattleResult.CreateAtItemUse(itemEntity);
			_sceneManager.CloseSceneAsync(this, result).Forget();
		}

		#endregion


		#region MonoBegaviour

		#endregion
	}
}