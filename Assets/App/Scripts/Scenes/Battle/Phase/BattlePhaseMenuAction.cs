//
// BattlePhaseMenuAction.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.12
//

using Ling.Common.Scene;

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// 通常メニュー開いて閉じるまで
	/// </summary>
	public class BattlePhaseMenuAction : BattlePhaseBase
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private IExSceneManager _sceneManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void Init() 
		{
			// メニューシーンを開く
			_sceneManager = Resolve<IExSceneManager>();

			_sceneManager.AddScene(Scene, SceneID.Menu, Menu.MenuArgument.CreateAtMenu());
		}

		public override void Proc() 
		{
		}

		public override void Term() 
		{ 
		}

		#endregion


		#region private 関数

		#endregion
	}
}
