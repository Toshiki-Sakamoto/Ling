//
// BattlePhaseNextStage.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.05
//

using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// 
	/// </summary>
	public class BattlePhaseNextStage : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Map.MapManager _mapManager = null;
		[Inject] private Chara.CharaManager _charaManager = null;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		protected override void AwakeInternal()
		{
		}

		public override void PhaseStart()
		{
			BuildNextMapAsync().Forget();
		}

		public override void PhaseUpdate()
		{
		}

		public override void PhaseStop()
		{
		}

		#endregion


		#region private 関数

		private async UniTask BuildNextMapAsync()
		{
			await _mapManager.BuildNextMapAsync();

			var level = _mapManager.LastBuildMapLevel;

			// MapViewを作成する
			_mapManager.CreateMapView(level);

			// マップに敵を生成する
			var enemyModelGroup = await _charaManager.BuildEnemyGroupAsync(level, _mapManager.FindGroundTilemap(level));
			if (enemyModelGroup == null)
			{
				return;
			}

			Scene.DeployEnemyToMap(enemyModelGroup, level);

			// 動きを制御
			var process = _processManager.Attach<Process.ProcessNextStageAnim>();
			process.AddFinishAction(() =>
				{
					ApplyNextLevel();
				});
		}

		private void ApplyNextLevel()
		{
			// 次の階層に移動処理する
			Scene.ApplyNextLevel();

			// 次のステージ移動完了
			Change(Phase.PlayerAction);
		}

		#endregion
	}
}
