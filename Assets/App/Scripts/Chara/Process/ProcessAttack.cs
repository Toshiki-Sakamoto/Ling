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
		
		private Chara.ICharaController _chara;	// 移動対象のキャラ

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void SetChara(Chara.ICharaController chara)
		{
			_chara = chara;
		}

		protected override void ProcessStartInternal()
		{
			AttackAsync().Forget();
		}

		#endregion


		#region private 関数

		private async UniTask AttackAsync()
		{
			var view = _chara.View;
			var dir = _chara.Model.Dir.Value * 0.3f;
			var movePos = new Vector3(dir.x, 0f, dir.y);

			await view.transform.DOMove(movePos, 0.1f).SetRelative(true);
			await view.transform.DOMove(movePos * -1, 0.1f).SetRelative(true);

			ProcessFinish();
		}

		#endregion
	}
}
