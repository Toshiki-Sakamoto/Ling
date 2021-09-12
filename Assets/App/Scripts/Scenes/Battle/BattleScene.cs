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
using Ling.Chara;
using Ling.Common.Scene;
using Zenject;
using Ling.MasterData.Repository;
using Ling.Scenes.Battle.Phases;
using Ling.Scenes.Battle.ProcessContainer;
using Ling.Common.Scene.Battle;
using Ling.Main;
using MessagePipe;

namespace Ling.Scenes.Battle
{
	public enum Phase
	{
		None,
		
		DataLoad,
		Start,
		Load,
		FloorSetup,
		CharaSetup,

		// Menu
		MenuAction,

		// Player
		PlayerActionStart,
		PlayerActionProcess,
		PlayerActionEnd,

		// Enemy
		EnemyAction,
		EnemyTink,

		Move,
		Exp,
		CharaProcessExecute,
		CharaProcessEnd,
		NextStage,
		Adv,
		UseItem,
		Equip,	// 装備着脱
		ItemGet,	// アイテムを拾う
	}

	public class BattleSceneArgument : Argument
	{
		// セーブデータから再開する
		public bool IsResume = true;
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

		// イベント
		[Inject] private ISubscriber<Chara.EventKilled> _killedEvent;

#if DEBUG
		[Inject] private Utility.DebugConfig.DebugConfigManager _debugConfigManager;
#endif

		private bool _isInitialized;
		private bool _isResume;

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
			var battleArgument = Argument as BattleSceneArgument;
			
			// デバッグ用のコード直指定でバトルを始める
			var stageMaster = _masterHolder.StageRepository.FindByStageType(Const.StageType.First);

			// コンパイルは知らせずに値を切り替える方法
			// 案1. EditorPrefs
			// 案2. Menu.GetChecked(menuPath);
#if UNITY_EDITOR
			_isResume = battleArgument?.IsResume ?? Common._Debug.Editor.CommonDebugMenu.IsForceResumeChecked;
#else
			_isResume = battleArgument?.IsResume ?? false;
#endif

			var param = new BattleModel.Param
				{
					stageMaster = stageMaster,
					IsResume = _isResume,
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

			RegistPhase<BattlePhaseDataLoad>(Phase.DataLoad);
			RegistPhase<BattlePhaseStart>(Phase.Start);
			RegistPhase<BattlePhaseLoad>(Phase.Load);
			RegistPhase<BattlePhaseFloorSetup>(Phase.FloorSetup);

			// Menu
			RegistPhase<BattlePhaseMenuAction>(Phase.MenuAction);

			// Player
			RegistPhase<BattlePhasePlayerActionStart>(Phase.PlayerActionStart);
			RegistPhase<BattlePhasePlayerAction>(Phase.PlayerActionProcess);
			RegistPhase<BattlePhaseItemGet>(Phase.ItemGet);

			RegistPhase<BattlePhaseAdv>(Phase.Adv);
			RegistPhase<BattlePhaseNextStage>(Phase.NextStage);
			RegistPhase<BattlePhaseEnemyThink>(Phase.EnemyTink);
			RegistPhase<BattlePhaseCharaProcessExecuter>(Phase.CharaProcessExecute);
			RegistPhase<BattlePhaseCharaProcessEnd>(Phase.CharaProcessEnd);
			RegistPhase<BattlePhaseExp>(Phase.Exp);
			RegistPhase<BattlePhaseCharaMove>(Phase.Move);
			RegistPhase<BattlePhaseUseItem>(Phase.UseItem);
			RegistPhase<BattlePhaseEquip>(Phase.Equip);

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
			_killedEvent
				.Subscribe(ev => 
				{
					if (ev.unit == null || ev.opponent == null) return;
					if (ev.unit == ev.opponent) return;

					// プレイヤーが死んだ場合は何もしない
					if (ev.opponent == _charaManager.Player) return;

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
				}).AddTo(this);
			
			
			// 中断データから再開する場合
			if (_isResume)
			{
				StartPhase(Phase.DataLoad);
			}
			else
			{
				// Phase開始
				StartPhase(Phase.Start);
			}

			// デバッグ登録
#if DEBUG
			var battleDebug = new Utility.DebugConfig.DebugMenuItem.Data("バトルデバッグ");
			var debugPrefabItem = Utility.DebugConfig.DebugPrefabData.CreateAtPath("Prefabs/_Debug/Battle/DebugBattleSkillTest",
				(gameObj, parent) =>
				{
					return _diContainer.InstantiatePrefab(gameObj, parent);
				});

			battleDebug.Add(debugPrefabItem);

			_debugConfigManager.AddItemDataByRootMenu(battleDebug).AddTo(this);
#endif
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
					OnCamebackByMenu(result);
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
			_charaManager.ChangeNextLevel(_mapManager.CurrentTilemap, _mapManager.CurrentMapData, _model.Level);

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
		/// 敵やアイテムなど必要なオブジェクトをマップに配置する
		/// </summary>
		public void DeployObjectToMap(bool isResume)
		{
			foreach (var pair in _charaManager.EnemyControlDict)
			{
				var level = pair.Key;
				DeployObjectToMap(pair.Value, level, isResume);
			}
		}
		
		public void DeployObjectToMap(EnemyControlGroup enemyControlGroup, int level, bool isReume)
		{
			foreach (var enemy in enemyControlGroup)
			{
				MapControl.SetChara(enemy, level);

				if (!isReume)
				{
					var pos = MapControl.GetRandomPosInRoom(level);
					enemy.Model.InitPos(pos);
				}
			}

			// アイテムを配置する
			if (isReume)
			{
				MapControl.ResumeItemObjectToMap(level);
			}
			else
			{
				MapControl.CreateItemObjectToMap(level);
			}
		}

		#endregion


		#region private 関数

		private void OnCamebackByMenu(BattleResult result)
		{
			if (result == null)
			{
				// 何も選択されなかった
				_phase.ChangePhase(Phase.PlayerActionStart);
				return;
			}

			switch (result.Menu)
			{
				case BattleResult.MenuCategory.UseItem:
					// なにか使用したか
					if (result.UseItemEntity != null)
					{
						var useItemArg = new BattlePhaseUseItem.Arg { Item = result.UseItemEntity };
						_phase.ChangePhase(Phase.UseItem, useItemArg);
					}
					break;

				case BattleResult.MenuCategory.Equip:
					if (result.EquipEntity != null)
					{
						var arg = new BattlePhaseEquip.Arg { Entity = result.EquipEntity };
						_phase.ChangePhase(Phase.Equip, arg);
					}
					break;
			}


			// 何も使用してないならプレイヤー行動に戻す
			_phase.ChangePhase(Phase.PlayerActionStart);
		}

		#endregion


		#region MonoBegaviour


		#endregion
	}
}