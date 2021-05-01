//
// BattelPhaseReaction.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.30
//

using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// 攻撃を受けたときのリアクション処理
	/// </summary>
	public class BattlePhaseReaction : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseStart()
		{
			// 何もなければ次に行く
			if (!Scene.ProcessContainer.Exists(ProcessType.Reaction))
			{
				Change(Phase.CharaProcessExecute);
				return;
			}
		}

		/// <summary>
		/// 非同期
		/// </summary>
		public override async UniTask PhaseStartAsync(CancellationToken token)
		{
			await Scene.ProcessContainer.ExecuteAsync(ProcessType.Reaction, token);
		}

		public override void PhaseUpdate()
		{
		}

		public override void PhaseStop()
		{
		}

		#endregion


		#region private 関数

		#endregion
	}
}
