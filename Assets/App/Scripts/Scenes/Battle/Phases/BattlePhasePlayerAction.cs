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
	/// プレイヤー側の行動実行
	/// </summary>
	public class BattlePhasePlayerAction : BattlePhaseBase
	{
		#region 定数, class, enum

		public class Arg : Utility.PhaseArgument
		{
			public ProcessType ActionProcessType;
			public Process.IProcessTargetGetter TargetGetter;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Chara.CharaManager _charaManager;

		private Arg _arg;
		private Chara.PlayerControl _player;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseStart()
		{
			_arg = Argument as Arg;

			// なにもないときは終了する
			if (!Scene.ProcessContainer.Exists(_arg.ActionProcessType))
			{
				Change(Phase.EnemyTink);
				return;
			}

			_player = _charaManager.Player;
		}

		public override async UniTask PhaseStartAsync(CancellationToken token)
		{
			await Scene.ProcessContainer.UniTaskExecuteOnceAsync(_arg.ActionProcessType, token: token);

			// メッセージが出ている場合は待機
			await _battleManager.WaitMessageSending();

			// プレイヤーのフォローカメラを戻す
			_player.SetFollowCameraEnable(true);

			// 終了時、攻撃した敵が生きていたらその敵から優先して行動する
			var targets = _arg.TargetGetter.Targets;

			// 死んでいる敵は除外する
			targets.RemoveAll(target => target.Status.IsDead.Value);

			// ターゲットがいない場合は普通に敵思考に回す
			if (!targets.IsNullOrEmpty())
			{
				// 攻撃した敵が生きている場合、最優先で行動させる
				// 敵思考に移行する
				var argument = new BattlePhaseEnemyThink.Arg();
				argument.Targets = targets;
				argument.nextPhase = Phase.PlayerActionStart;

				// 経験値処理に移動
				Change(Phase.Exp, BattlePhaseExp.Arg.CreateAtEnemyThink(argument));
			}
			else
			{
				Change(Phase.Exp, BattlePhaseExp.Arg.CreateAtEnemyThink(null));
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
