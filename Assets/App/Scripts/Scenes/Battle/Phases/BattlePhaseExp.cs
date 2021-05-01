//
// BattlePhaseExp.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.30
//

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// 経験値処理
	/// </summary>
	public class BattlePhaseExp : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseStart()
		{
			// なにもないときは終了する
			if (!Scene.ProcessContainer.Exists(ProcessType.Exp))
			{
				Change(Phase.CharaProcessEnd);
				return;
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
