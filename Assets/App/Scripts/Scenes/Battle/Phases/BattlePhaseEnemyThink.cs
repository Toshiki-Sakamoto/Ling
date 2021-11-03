//
// BattlePhaseEnemyThink.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.02
//

using Cysharp.Threading.Tasks;
using Ling;
using System.Collections.Generic;
using Utility.Extensions;
using Zenject;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// 敵の思考
	/// </summary>
	public class BattlePhaseEnemyThink : BattlePhaseBase
	{
		#region 定数, class, enum

		public class Arg : Utility.PhaseArgument
		{
			public List<Chara.ICharaController> Targets; // 特定のターゲットを指定する場合
			public Phase nextPhase; // 行動終了後指定した特定のフェーズに移行したい場合
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Chara.CharaManager _charaManager;
		[Inject] private Map.MapManager _mapManager;
		
		private Utility.Async.WorkTimeAwaiter _timeAwaiter;
		private Arg _argment;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseInit()
		{
		}

		public override void PhaseStart()
		{
			_argment = Argument as Arg;

			_timeAwaiter = new Utility.Async.WorkTimeAwaiter();
			_timeAwaiter.Setup(0.1f);

			ThinkAIProcessesAsync().Forget();
		}

		#endregion


		#region private 関数

		private async UniTask ThinkAIProcessesAsync()
		{
			if (_argment?.Targets?.IsNullOrEmpty() ?? false)
			{
				var targets = _argment.Targets;

				// 特定のターゲットのみ思考させる
				foreach (var target in targets)
				{
					// 敵じゃなければ何もしない
					if (target is Chara.EnemyControl enemy)
					{
						await enemy.ThinkAIProcess(_timeAwaiter);

						// スキルプロセスを引っ付ける
						if (enemy.ActionThinkCore.Result != null)
						{
							var skillProcess = enemy.AddAttackProcess<Skill.SkillProcess>();
							skillProcess.Setup(enemy, enemy.ActionThinkCore.SkillMaster);
						}
					}
				}

				// すべて終わったら次に行く
				Change(_argment.nextPhase);
			}
			else
			{
				// 敵は生成された順番から思考する
				// 階層の浅い順から思考を開始する
				foreach (var pair in _charaManager.EnemyControlDict)
				{
					foreach (var enemy in pair.Value)
					{
						// 敵が持つAIによって行動を自由に変更する
						await enemy.ThinkAIProcess(_timeAwaiter);

						// スキルプロセスを引っ付ける
						if (enemy.ActionThinkCore.Result != null)
						{
							var skillProcess = enemy.AddAttackProcess<Skill.SkillProcess>();
							skillProcess.Setup(enemy, enemy.ActionThinkCore.SkillMaster, enemy.ActionThinkCore.Result);
						}

						// プロセスを格納する
						if (enemy.ExistsAttackProcess)
						{
							var process = new Process.ProcessCharaAction(enemy);
							Scene.ProcessContainer.Add(ProcessType.Action, process);
						}
					}
				}

				// すべて終わったら次に行く
				Change(Phase.Move);
			}
		}

		#endregion
	}
}
