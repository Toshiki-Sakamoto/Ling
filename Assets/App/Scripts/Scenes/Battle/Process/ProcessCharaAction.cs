//
// ProcessCharaAction.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.30
//

using System.Collections.Generic;
using Utility;
using Cysharp.Threading.Tasks;

namespace Ling.Scenes.Battle.Process
{
	/// <summary>
	/// キャラクタの攻撃(スキルなど)のプロセスを呼び出す
	/// </summary>
	public class ProcessCharaAction : Utility.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		/// <summary>
		/// 対象キャラクタ
		/// </summary>
		private Chara.ICharaActionController _chara;


		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		public ProcessCharaAction(Chara.ICharaActionController chara)
		{
			_chara = chara;
		}

		#endregion


		#region public, protected 関数

		protected override void ProcessStartInternal()
		{
			// 特技がなければ即終了
			

			OnProcessAsync().Forget();
		}

		#endregion


		#region private 関数

		private async UniTask OnProcessAsync()
		{
			_chara.ExecuteAttackProcess();

			await _chara.WaitAttackProcess();

			// 終了
			ProcessFinish();
		}

		#endregion
	}
}
