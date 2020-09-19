//
// ProcessAttack.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.11
//

using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Ling.Utility.Extensions;
using System.Collections.Generic;

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
		
		private Chara.CharaManager _charaManager;
		private Chara.ICharaController _unit;	// 攻撃対象のキャラ
		private List<Chara.ICharaController> _targets = new List<ICharaController>();	// ターゲット
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

		public ProcessAttack()
		{
			_charaManager = _diContainer.Resolve<Chara.CharaManager>();
		}

		#endregion


		#region public, protected 関数

		public void SetChara(Chara.ICharaController unit, bool ignoreIfNoTarget)
		{
			_unit = unit;
			_ignoreIfNoTarget = ignoreIfNoTarget;
		}

		public void SetTargetPos(in Vector2Int targetPos)
		{
			_targetPos = targetPos;
		}

		protected override void ProcessStartInternal()
		{
			SearchTargetUnit();

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

			await view.transform.DOMove(movePos, 0.1f).SetRelative(true);

			// ダメージ計算をここで行う
			foreach (var target in _targets)
			{
				CalcDamage(target);
			}

			await view.transform.DOMove(movePos * -1, 0.1f).SetRelative(true);

			// 主人公が死んだ場合、ゲームオーバー処理となる
			if (_charaManager.IsPlayerDead)
			{
			}
			else if (_deadChara.Count > 0)
			{
				// 死んだキャラが居る場合レベルアップ処理
				var processLevelUp = SetNext<ProcessLevelUp>();
				processLevelUp.Setup(_unit, _deadChara);
			}

			ProcessFinish();
		}

		/// <summary>
		/// ダメージ計算を行う
		/// </summary>
		private void CalcDamage(Chara.ICharaController target)
		{
			// 座標に攻撃対象がいるか
			if (!ExistsTarget) return;

			target.Status.SubHP(1);

			// 死んだエフェクト待機
			if (target.Status.IsDead.Value)
			{
				
			}

			// 死んだ場合、キャラが死んだときの処理を行う
			if (target.Model.Status.IsDead.Value)
			{
				_deadChara.Add(target);
			}
		}

		/// <summary>
		/// ターゲットを検索する
		/// </summary>
		private void SearchTargetUnit()
		{
			var charaManager = _diContainer.Resolve<Chara.CharaManager>();
			_targets.Add(charaManager.FindCharaInPos(_unit.Model.MapLevel, _targetPos));
		}

		#endregion
	}
}
