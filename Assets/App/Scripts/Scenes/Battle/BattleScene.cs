// 
// Scene.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.13
// 

using System;
using UniRx;
using UnityEngine;
using Cysharp.Threading.Tasks;

using Ling.Common.Scene;
using Zenject;
using Ling.MasterData.Repository;
using Ling.Scenes.Battle.Phases;
using Ling.Scenes.Battle.ProcessContainer;

namespace Ling.Scenes.Battle
{
	public enum Phase
	{
		None,

		Start,
		Load,
		FloorSetup,
		CharaSetup,

		// Menu
		MenuAction,

		// Player
		PlayerAction,
		PlayerAttack,
		PlayerActionProcess,
		PlayerActionEnd,

		// Enemy
		EnemyAction,
		EnemyTink,

		Move,
		Reaction,
		Exp,
		CharaProcessExecute,
		CharaProcessEnd,
		NextStage,
		Adv,
		UseItem,
	}

	/// <summary>
	/// Battle
	/// </summary>
	public class BattleScene : Common.Scene.ExSceneBase
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private BattleView _view = default;
		[SerializeField] private Transform _debugRoot = default;

		[Inject] private BattleModel _model = null;
		[Inject] private Map.MapManager _mapManager = null;
		[Inject] private Chara.CharaManager _charaManager = null;
		[Inject] private MasterData.IMasterHolder _masterHolder = default;

		private bool _isInitialized;

		#endregion


		#region プロパティ

		public override Common.Scene.SceneID SceneID => Common.Scene.SceneID.Battle;

		/// <summary>
		/// 自分のシーンに必要なシーンID
		/// 自シーン読み込み後になければ読み込みを行う
		/// </summary>
		public override DependenceData[] Dependences =>
			new DependenceData[]
			{
				DependenceData.CreateAtLoaded(Common.Scene.SceneID.Status),
			};

		/// <summary>
		/// BattleScene大元のView
		/// </summary>
		public BattleView View => _view;

		/// <summary>
		/// MapControl
		/// </summary>
		public Map.MapControl MapControl => _mapManager.MapControl;

		public ProcessContainer<ProcessType> ProcessContainer { get; } = new ProcessContainer<ProcessType>();

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 遷移後まずは呼び出される
		/// </summary>
		/// <returns></returns>
		public override IObservable<Base> ScenePrepareAsync() =>
			Observable.Return(this);


		/// <summary>
		/// 正規手順でシーンが実行されたのではなく
		/// 直接起動された場合StartSceneよりも前に呼び出される
		/// </summary>
		public override UniTask QuickStartSceneAsync()
		{
			// デバッグ用のコード直指定でバトルを始める
			var stageMaster = _masterHolder.StageRepository.FindByStageType(Const.StageType.First);

			var param = new BattleModel.Param
			{
				stageMaster = stageMaster
			};

			_model.Setup(param);

			return default(UniTask);
		}

		/// <summary>
		/// シーンが開始される時
		/// </summary>
		public override void StartScene()
		{
			if (_isInitialized) return;

			RegistPhase<BattlePhaseStart>(Phase.Start);
			RegistPhase<BattlePhaseLoad>(Phase.Load);
			RegistPhase<BattlePhaseFloorSetup>(Phase.FloorSetup);

			// Menu
			RegistPhase<BattlePhaseMenuAction>(Phase.MenuAction);

			// Player
			RegistPhase<BattlePhasePlayerAction>(Phase.PlayerAction);
			RegistPhase<BattlePhasePlayerAttack>(Phase.PlayerAttack);
			RegistPhase<BattlePhasePlayerActionProcess>(Phase.PlayerActionProcess);
			RegistPhase<BattlePhasePlayerActionEnd>(Phase.PlayerActionEnd);

			RegistPhase<BattlePhaseAdv>(Phase.Adv);
			RegistPhase<BattlePhaseNextStage>(Phase.NextStage);
			RegistPhase<BattlePhaseEnemyThink>(Phase.EnemyTink);
			RegistPhase<BattlePhaseCharaProcessExecuter>(Phase.CharaProcessExecute);
			RegistPhase<BattlePhaseCharaProcessEnd>(Phase.CharaProcessEnd);
			RegistPhase<BattlePhaseReaction>(Phase.Reaction);
			RegistPhase<BattlePhaseExp>(Phase.Exp);
			RegistPhase<BattlePhaseCharaMove>(Phase.Move);
			RegistPhase<BattlePhaseUseItem>(Phase.UseItem);

			ProcessContainer.Setup(_processManager, gameObject);

			_isInitialized = true;

			// 行動終了時等、特定のタイミングでフェーズを切り替える
			_eventManager.Add<EventChangePhase>(this,
				_ev =>
				{
					// 行動終了時のPhase切り替えの予約
					_model.NextPhaseMoveReservation = _ev.phase;
				});

			_eventManager.Add<EventAddProcessContainer>(this,
				ev => 
				{
					ProcessContainer.Add(ev.Type, ev.Process);
				});

			// 経験値獲得処理
			_eventManager.Add<Chara.EventKilled>(this, 
				ev => 
				{
					if (ev.unit == null || ev.opponent == null) return;
					if (ev.unit == ev.opponent) return;

					// 獲得量
					var exp = ev.opponent.Model.Exp;

					var expProcess = ProcessContainer.Find<Process.ProcessAddExp>(ProcessType.Exp, process => ev.unit == process.Chara);
					if (expProcess != null)
					{
						expProcess.Add(exp);
					}
					else
					{
						expProcess = _diContainer.Instantiate<Process.ProcessAddExp>();
						expProcess.Setup(ev.unit, exp);
						ProcessContainer.Add(ProcessType.Exp, expProcess);
					}
				});

			// 始めは１階層
			View.UIHeaderView.SetLevel(_model.Level);

			UnityEngine.Random.InitState(1);

			// Phase開始
			StartPhase(Phase.Start);
		}

		public override void UpdateScene()
		{
		}

		/// <summary>
		/// シーンが停止/一時中断される時
		/// </summary>
		public override void StopScene() { }

		/// <summary>
		/// シーン遷移前に呼び出される
		/// </summary>
		/// <returns></returns>
		public override IObservable<Unit> StopSceneAsync() =>
			Observable.Return(Unit.Default);

		/// <summary>
		/// あるシーンから戻ってきた時
		/// </summary>
		public override void CamebackScene(Base closedScene)
		{
			// メニューならPhaseを変更させる
			var sceneID = closedScene.SceneData.SceneID;
			var result = SceneData.Result as Common.Scene.Battle.BattleResult;

			switch (sceneID)
			{
				case SceneID.Menu:
					// なにか使用したか
					if (result.UseItemEntity == null)
					{
						// 何も使用してないならプレイヤー行動に戻す
						_phase.ChangePhase(Phase.PlayerAction);
					}
					else
					{
						var useItemArg = new BattlePhaseUseItem.Arg { Item = result.UseItemEntity };
						_phase.ChangePhase(Phase.UseItem, useItemArg);
					}
					break;
			}
		}


		/// <summary>
		/// 次のレベルに移動する
		/// </summary>
		public void ApplyNextLevel()
		{
			_model.NextLevel();

			// 移動した階層を今の階層とする
			_mapManager.ChangeNextLevel(_model.Level);

			// キャラクタ管理者
			_charaManager.ChangeNextLevel(_mapManager.CurrentTilemap, _model.Level);

			// 次の階層に行った
			View.UIHeaderView.SetLevel(_model.Level);

			_eventManager.Trigger(new EventChangeNextStage
			{
				level = _mapManager.CurrentMapIndex,
				tilemap = _mapManager.CurrentTilemap
			});
		}

		/// <summary>
		/// 予約していたフェーズに移動する
		/// </summary>
		/// <returns>遷移したらtrue</returns>
		public bool MoveToResercationPhase()
		{
			if (_model.NextPhaseMoveReservation == Phase.None) return false;

			var nextPhase = _model.NextPhaseMoveReservation;
			_model.NextPhaseMoveReservation = Phase.None;

			_phase.ChangePhase(nextPhase, null);

			return true;
		}


		/// <summary>
		/// 敵グループをマップに配置する
		/// </summary>
		public void DeployEnemyToMap(Chara.EnemyControlGroup enemyGroup, int level)
		{
			foreach (var enemy in enemyGroup)
			{
				var pos = MapControl.GetRandomPosInRoom(level);

				enemy.Model.SetMapLevel(level);
				MapControl.SetChara(enemy, level);
				enemy.Model.InitPos(pos);
			}
		}

		#endregion


		#region private 関数


		#endregion


		#region MonoBegaviour


		#endregion
	}
}