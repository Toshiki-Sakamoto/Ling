//
// ProcessAttack.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.11
//

using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Utility.Extensions;
using System.Collections.Generic;
using System;
using UniRx;
using Zenject;
using MessagePipe;

namespace Ling.Chara.Process
{
	/// <summary>
	/// 通常攻撃プロセス
	/// </summary>
	public class ProcessAttack : Utility.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Chara.CharaManager _charaManager;
		[Inject] private Utility.IEventManager _eventManager;
		[Inject] private IPublisher<Chara.EventKilled> _killedEvent;

		private Chara.ICharaController _unit;   // 攻撃対象のキャラ
		private List<Chara.ICharaController> _targets = new List<ICharaController>();   // ターゲット
		private List<Chara.ICharaController> _deadChara = new List<ICharaController>();
		private Vector2Int _targetPos;
		private bool _ignoreIfNoTarget;
		

		#endregion


		#region プロパティ

		/// <summary>
		/// ターゲットが存在するか
		/// </summary>
		public bool ExistsTarget => !_targets.IsNullOrEmpty();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void SetChara(Chara.ICharaController unit, bool ignoreIfNoTarget, in Vector2Int targetPos)
		{
			_unit = unit;
			_ignoreIfNoTarget = ignoreIfNoTarget;
			_targetPos = targetPos;
		}

		public void SetTargets(List<Chara.ICharaController> targets) =>
			_targets = targets;

		protected override void ProcessStartInternal()
		{
			AttackAsync().Forget();
		}

		#endregion


		#region private 関数

		private async UniTask AttackAsync()
		{
			// ターゲットがいない場合何もしない
			if (!_ignoreIfNoTarget && !ExistsTarget)
			{
				ProcessFinish();
				return;
			}

			// ターゲット方向を見る
			_unit.Model.SetDirectionByTargetPos(_targetPos);

			var view = _unit.View;
			var dir = _unit.Model.Dir.Value;
			var movePos = new Vector3(dir.x * 0.3f, 0f, dir.y * 0.3f);

			var moveSequence = DOTween.Sequence()
				.Append(view.transform.DOMove(movePos, 0.1f).SetRelative(true))
				.Append(view.transform.DOMove(movePos * -1, 0.1f).SetRelative(true));

			// ダメージ計算
			foreach (var target in _targets)
			{	
				// HPをへらす
				// Note: 今は計算処理をここに書くが、別の場所に逃がすかどうか
				var damage = Chara.Calculator.CharaAttackCalculator.Calculate(_unit, target);
				await target.Damage(damage);

				if (target.Status.IsDead.Value)
				{
					_deadChara.Add(target);
				}
			}

			await moveSequence;

			// 主人公が死んだ場合、ゲームオーバー処理となる
			if (_charaManager.IsPlayerDead)
			{
				return;
			}

			foreach (var target in _deadChara)
			{
#if false
				if (!target.View.IsAnimationPlaying) continue;

				// 1フレーム必ず待機する
				await UniTask.WaitUntil(() => target.View.IsAnimationPlaying);
#endif

				// 倒した情報を送る
				_killedEvent.Publish(new EventKilled { unit = _unit, opponent = target });
			}

			if (!_deadChara.IsNullOrEmpty())
			{
				// 死んだキャラが居る場合レベルアップ処理
				//var processLevelUp = SetNext<ProcessLevelUp>();
				//processLevelUp.Setup(_unit, _deadChara);
			}

			ProcessFinish();
		}

		#endregion
	}
}
