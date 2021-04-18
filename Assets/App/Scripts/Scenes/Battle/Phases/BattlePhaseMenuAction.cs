//
// BattlePhaseMenuAction.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.12
//

using Ling.Common.Scene;
using Zenject;
using Ling.Common.Scene.Menu;

namespace Ling.Scenes.Battle.Phases
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

		[Inject] private IExSceneManager _sceneManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseStart() 
		{
			// メニューシーンを開く
			_sceneManager.AddScene(SceneID.Menu, MenuArgument.CreateAtMenu());
		}

		public override void PhaseUpdate() 
		{
		}

		public override void PhaseStop() 
		{ 
		}

		#endregion


		#region private 関数

		#endregion
	}
}
