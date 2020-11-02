//
// BattlePhaseEnemyThink.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.02
//

using Cysharp.Threading.Tasks;
using Ling;
using System.Collections.Generic;
using Ling.Utility.Extensions;

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// 敵の思考
	/// </summary>
	public class BattlePhaseEnemyThink : BattlePhaseBase
	{
		#region 定数, class, enum

		public class Argument : Utility.PhaseArgBase
		{
			public List<Chara.ICharaController> Targets; // 特定のターゲットを指定する場合
			public BattleScene.Phase nextPhase;	// 行動終了後指定した特定のフェーズに移行したい場合
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Argument _argment;
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
			_argment = Arg as Argument;

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
			if (_argment?.Targets?.IsNullOrEmpty() ?? false)
			{
				var targets = _argment.Targets;
				
				// 特定のターゲットのみ思考させる
				foreach (var target in targets)
				{
					// 敵じゃなければ何もしない
					if (target is Chara.EnemyControl enemy)
					{
						await enemy.ThinkAIProcess(_timeAwaiter);
					}
				}

				// すべて終わったら次に行く
				Change(_argment.nextPhase);
			}
			else
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
		}

		#endregion
	}
}
