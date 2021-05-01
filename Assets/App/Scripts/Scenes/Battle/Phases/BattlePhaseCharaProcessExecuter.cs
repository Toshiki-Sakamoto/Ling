//
// BattlePhaseCharaProcess.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.23
//

using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;
using System.Threading;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// キャラクタの行動プロセスをすべて実行する
	/// </summary>
	public class BattlePhaseCharaProcessExecuter : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Chara.CharaManager _charaManager;

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
			// なにもないときは終了する
			if (!Scene.ProcessContainer.Exists(ProcessType.Action))
			{
				Change(Phase.Exp);
				return;
			}
		}


		#endregion


		#region private 関数

		
		/// <summary>
		/// 非同期
		/// </summary>
		public override async UniTask PhaseStartAsync(CancellationToken token)
		{
			await Scene.ProcessContainer.UniTaskExecuteOnceAsync(ProcessType.Action, token: token);

			Change(Phase.Reaction);
		}

#if false
		public async UniTask ExecuteAsync()
		{
			// 終わったら順番に攻撃・特技Processを叩く
			// 特技に積まれているプロセスをすべて順番に叩いていく
			// 積まれているものがなければ終了処理に飛ぶ
			// キャラは自分の特技プロセスを呼び出すプロセスをContainerに追加する


			foreach (var enemyGroupPair in _charaManager.EnemyControlDict)
			{
				foreach (var enemyControl in enemyGroupPair.Value)
				{
					// なにもないときはスルー

					enemyControl.ExecuteAttackProcess();

					await enemyControl.WaitAttackProcess();

					// メッセージが出ている場合は待機
					await _battleManager.WaitMessageSending();
				}
			}

			// すべて終わったらリアクション
		}
#endif
		#endregion
	}
}
