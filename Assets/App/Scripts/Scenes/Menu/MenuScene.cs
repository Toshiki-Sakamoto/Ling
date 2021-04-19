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

		[SerializeField] private MenuModel _model = default;
		[SerializeField] private MenuView _view = default;

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

			var viewParam = new MenuView.Param()
				{
					CategoryData = _model.CategoryData,
				};

			_view.Setup(viewParam);

			// カテゴリが変更された
			_view.SelectedIndex
				.Subscribe(index => 
				{
					_model.SetSelectedCategoryIndex(index);

					_view.SetCategoryData(_model.SelectedCategoryData);
				}).AddTo(this);

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
		{ }

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

		#endregion


		#region MonoBegaviour

		#endregion
	}
}