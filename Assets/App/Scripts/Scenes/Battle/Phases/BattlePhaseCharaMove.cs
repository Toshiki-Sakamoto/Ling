//
// BattlePhaseCharaMove.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.30
//

using Zenject;
using Cysharp.Threading.Tasks;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// キャラの移動
	/// </summary>
	public class BattlePhaseCharaMove : BattlePhaseBase
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
			ExecuteAsync().Forget();
		}


		#endregion


		#region private 関数

		public async UniTask ExecuteAsync()
		{
			// まずは移動Processをすべて叩く
			_charaManager.ExecuteMoveProcesses();

			// 移動が終わるまで待機
			await _charaManager.WaitForMoveProcessAsync();

			// すべて終わったら行動プロセスの処理に移動する
			Change(Phase.CharaProcessExecute);
		}

		#endregion
	}
}