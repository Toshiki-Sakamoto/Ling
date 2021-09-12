﻿//
// BattlePhasePlayerAction.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
//

using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;
using Utility.Extensions;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// プレイヤーの通常攻撃
	/// </summary>
	public class BattlePhasePlayerAttack : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Chara.CharaManager _charaManager;
		[Inject] private Map.MapManager _mapManager;
		private Chara.PlayerControl _player;
		
		private List<Chara.ICharaController> _targets = new List<Chara.ICharaController>(); // ターゲット

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
			_player = _charaManager.Player;
			_player.SetFollowCameraEnable(false);

			_targets.Clear();

			// プレイヤーの向きから攻撃先を決める
			// (武器によって範囲変えたり)
			var position = _player.Model.CellPosition;
			var dir = _player.Model.Dir;
			var targetPos = position.Value + dir.Value;

			SearchTargetUnit(targetPos);

			var attackProcess = _player.AddAttackProcess<Chara.Process.ProcessAttack>();
			attackProcess.SetChara(_player, ignoreIfNoTarget: true, targetPos: targetPos);
			attackProcess.SetTargets(_targets);

			_player.ExecuteAttackProcess();
		}

		/// <summary>
		/// 非同期
		/// </summary>
		public async override UniTask PhaseStartAsync(CancellationToken token)
		{
			await _player.WaitAttackProcess();

			// メッセージが出ている場合は待機
			await _battleManager.WaitMessageSending();

			_player.SetFollowCameraEnable(true);

			// 死んでいる敵は除外する
			_targets.RemoveAll(target => target.Status.IsDead.Value);

			// ターゲットがいない場合は普通に敵思考に回す
			if (!_targets.IsNullOrEmpty())
			{
				// 攻撃した敵が生きている場合、最優先で行動させる
				// 敵思考に移行する
				var argument = new BattlePhaseEnemyThink.Arg();
				argument.Targets = _targets;
				argument.nextPhase = Phase.PlayerActionStart;

				// 経験値処理に移動
				Change(Phase.Exp, BattlePhaseExp.Arg.CreateAtEnemyThink(argument));
			}
			else
			{
				Change(Phase.Exp, BattlePhaseExp.Arg.CreateAtEnemyThink(null));
			}
		}

		public override void PhaseStop()
		{
		}

		#endregion


		#region private 関数

		/// <summary>
		/// ターゲットを検索する
		/// </summary>
		private void SearchTargetUnit(in Vector2Int targetPos)
		{
			var target = _charaManager.FindCharaInPos(_player.Model.MapLevel, targetPos);
			if (target == null) return;

			_targets.Add(target);
		}

		#endregion
	}
}
