//
// BattlePhaseEnemyThink.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.02
//

using Cysharp.Threading.Tasks;
using Ling;

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// 敵の思考
	/// </summary>
	public class BattlePhaseEnemyThink : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Chara.CharaManager _charaManager;
		private Map.MapManager _mapManager;
		private Utility.Async.WorkTimeAwaiter _timeAwaiter;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		protected override void AwakeInternal()
		{
			_charaManager = Resolve<Chara.CharaManager>();
			_mapManager = Resolve<Map.MapManager>();
		}

		public override void Init()
		{
			_timeAwaiter = new Utility.Async.WorkTimeAwaiter();
			_timeAwaiter.Setup(0.1f);

			ThinkAIProcessesAsync().Forget();
		}

		public override void Proc()
		{
		}

		#endregion


		#region private 関数

		private async UniTask ThinkAIProcessesAsync()
		{
			// 敵は生成された順番から思考する
			// 階層の浅い順から思考を開始する
			foreach (var pair in _charaManager.EnemyControlDict)
			{
				foreach (var enemy in pair.Value)
				{
					// 敵が持つAIによって行動を自由に変更する
					await enemy.ThinkAIProcess(_timeAwaiter);
				}
			}

			// すべて終わったら次に行く
			Change(BattleScene.Phase.CharaProcessExecute);
		}

		#endregion
	}
}
