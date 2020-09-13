//
// ProcessAttack.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.11
//

using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

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
		
		private Chara.ICharaController _unit;	// 攻撃対象のキャラ
		private Chara.ICharaController _target;	// ターゲット
		private Vector2Int _targetPos;
		private bool _ignoreIfNoTarget;

		#endregion


		#region プロパティ

		/// <summary>
		/// ターゲットが存在するか
		/// </summary>
		public bool ExistsTarget => _target != null;

		#endregion


		#region コンストラクタ, デストラクタ

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
			if (_ignoreIfNoTarget && !ExistsTarget) return;

			var view = _unit.View;
			var dir = _unit.Model.Dir.Value;
			var movePos = new Vector3(dir.x * 0.3f, 0f, dir.y * 0.3f);

			await view.transform.DOMove(movePos, 0.1f).SetRelative(true);

			// ダメージ計算をここで行う
			CalcDamage();

			await view.transform.DOMove(movePos * -1, 0.1f).SetRelative(true);

			ProcessFinish();
		}

		/// <summary>
		/// ダメージ計算を行う
		/// </summary>
		private void CalcDamage()
		{
			// 座標に攻撃対象がいるか
			if (!ExistsTarget) return;

			_target.Model.Status.SubHP(1);
		}

		/// <summary>
		/// ターゲットを検索する
		/// </summary>
		private void SearchTargetUnit()
		{
			var charaManager = _diContainer.Resolve<Chara.CharaManager>();
			_target = charaManager.FindCharaInPos(_unit.Model.MapLevel, _targetPos);
		}

		#endregion
	}
}
