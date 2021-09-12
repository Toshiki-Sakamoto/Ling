//
// SkillProcess.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.03
//

using Ling.MasterData.Skill;
using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;
using Ling.Map;
using Utility.Extensions;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Ling.Scenes.Battle.Process
{
	/// <summary>
	/// </summary>
	public class ProcessPlayerAttack : Utility.ProcessBase, Process.IProcessTargetGetter
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private DiContainer _diContainer;

		[Inject] private Chara.CharaManager _charaManager;
		[Inject] private Map.MapManager _mapManager;

		private Chara.PlayerControl _player;

		private Chara.ICharaController _chara, _target;
		private SkillMaster _skill;

		#endregion


		#region プロパティ

		public List<Chara.ICharaController> Targets { get; } = new List<Chara.ICharaController>();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup()
		{
			_player = _charaManager.Player;
			_player.SetFollowCameraEnable(false);
		}

		public void SetTarget(Chara.ICharaController target)
		{
			//_target = target;
		}

		protected override void ProcessStartInternal()
		{
			Execute().Forget();
		}

		public async UniTask Execute()
		{
			// プレイヤーの向きから攻撃先を決める
			// (武器によって範囲変えたり)
			var position = _player.Model.CellPosition;
			var dir = _player.Model.Dir;
			var targetPos = position.Value + dir.Value;

			SearchTargetUnit(targetPos);

			// 実際のアタック処理を行うプロセス
			var attackCore = SetNext<Chara.Process.ProcessAttack>();
			attackCore.SetChara(_player, ignoreIfNoTarget: true, targetPos: targetPos);
			attackCore.SetTargets(Targets);

#if false
			_player.ExecuteAttackProcess();

			await _player.WaitAttackProcess();

			// メッセージが出ている場合は待機
			await BattleManager.Instance.WaitMessageSending();

			_player.SetFollowCameraEnable(true);
#endif
			ProcessFinish();
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

			Targets.Add(target);
		}

		#endregion
	}
}
