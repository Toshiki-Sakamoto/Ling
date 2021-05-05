//
// BattlePhasePlayerSkill.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.04
//

using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Utility.Extensions;
using Zenject;
using System.Threading;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// プレイヤー側のスキル実行
	/// </summary>
	public class BattlePhasePlayerSkill : BattlePhaseBase
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
			if (!Scene.ProcessContainer.Exists(ProcessType.PlayerSkill))
			{
				Change(Phase.EnemyTink);
				return;
			}
		}

		public override async UniTask PhaseStartAsync(CancellationToken token)
		{
			await Scene.ProcessContainer.UniTaskExecuteOnceAsync(ProcessType.PlayerSkill, token: token);

			Change(Phase.EnemyTink);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
