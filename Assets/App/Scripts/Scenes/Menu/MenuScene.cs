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

		#endregion


		#region プロパティ

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
		/// カテゴリに応じたControlクラスを生成し、自分と同じところにアタッチにする
		/// </summary>
		private void SetupCategoryControl(MenuCategoryData categoryData)
		{
			switch (categoryData.Category)
			{
				case MenuDefine.Category.Bag:
					_bagControl.Setup();
					break;
			}
		}

		private void ActivateCategoryControl(MenuCategoryData categoryData)
		{
			switch (categoryData.Category)
			{
				case MenuDefine.Category.Bag:
					_bagControl.Activate();
					break;
			}
		}

		#endregion


		#region MonoBegaviour

		#endregion
	}
}