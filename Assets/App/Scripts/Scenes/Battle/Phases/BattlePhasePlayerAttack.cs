//
// BattlePhasePlayerAction.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
//

using UnityEngine;
using System.Collections.Generic;

using Zenject;

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

		public override void PhaseUpdate()
		{
			if (!_player.IsAttackAllProcessEnded()) return;

			_player.SetFollowCameraEnable(true);

			//Change(BattleScene.Phase.PlayerAction);
			// 攻撃した敵が生きている場合、最優先で行動させる
			// 敵思考に移行する
			var argument = new BattlePhaseEnemyThink.Arg();
			argument.Targets = _targets;
			argument.nextPhase = Phase.PlayerAction;

			Change(Phase.EnemyTink, argument);
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
