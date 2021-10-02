//
// BattlePhaseExp.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.30
//

using Cysharp.Threading.Tasks;
using Zenject;
using System.Threading;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// 経験値処理
	/// </summary>
	public class BattlePhaseExp : BattlePhaseBase
	{
		#region 定数, class, enum

		public class Arg : Utility.PhaseArgument
		{
			public Phase NextPhase;
			public BattlePhaseEnemyThink.Arg EnemyThinkArg;

			/// <summary>
			/// 終了後、敵の行動に移動する
			/// </summary>
			public static Arg CreateAtEnemyThink(BattlePhaseEnemyThink.Arg enemyThinkArg) =>
				new Arg { NextPhase = Phase.EnemyTink, EnemyThinkArg = enemyThinkArg };

			/// <summary>
			/// 終了後、再度キャラの行動処理に移す
			/// </summary>
			public static Arg CreateAtCharaProcessExecuter() =>
				new Arg { NextPhase = Phase.CharaProcessExecute };

			public Utility.PhaseArgument GetNextArgument()
			{
				switch (NextPhase)
				{
					case Phase.EnemyTink:
						return EnemyThinkArg;

					default:
						return null;
				}
			}
		}

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
				ChangeByArgument();
				return;
			}
		}

		public override async UniTask PhaseStartAsync(CancellationToken token)
		{
			await Scene.ProcessContainer.UniTaskExecuteOnceAsync(ProcessType.Exp, token: token);

			ChangeByArgument();
		}

		#endregion


		#region private 関数

		private void ChangeByArgument()
		{
			var arg = Argument as Arg;

			Change(arg.NextPhase, arg.GetNextArgument());
		}

		#endregion
	}
}
