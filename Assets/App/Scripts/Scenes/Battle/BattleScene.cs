// 
// Scene.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.13
// 

using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

using Ling.Common.Scene;
using Zenject;


namespace Ling.Scenes.Battle
{
	/// <summary>
	/// 
	/// </summary>
	public class BattleScene : Common.Scene.Base 
    {
		#region 定数, class, enum

		public enum Phase
		{
			None,

			Start,
			Load,
			FloorSetup,
			CharaSetup,
			PlayerAction,
			PlayerActionProcess,
			PlayerActionEnd,
			EnemyAction,
			NextStage,
			Adv,
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private BattleView _view = null;

		[Inject] private BattleModel _model = null;
		[Inject] private MapManager _mapManager = null;
		[Inject] private CharaManager _charaManager = null;

		private bool _isInitialized;
		private Utility.PhaseScene<Phase, BattleScene> _phase = new Utility.PhaseScene<Phase, BattleScene>();

		#endregion


		#region プロパティ

		public BattleView View => _view;


		#endregion


		#region public, protected 関数

		/// <summary>
		/// 遷移後まずは呼び出される
		/// </summary>
		/// <returns></returns>
		public override IObservable<Unit> ScenePrepareAsync() =>
			Observable.Return(Unit.Default);

		/// <summary>
		/// シーンが開始される時
		/// </summary>
		public override void StartScene()
		{
			if (_isInitialized) return;

			_phase.Add(Phase.Start, new Battle.Phase.BattlePhaseStart());
			_phase.Add(Phase.Load, new Battle.Phase.BattlePhaseLoad());
			_phase.Add(Phase.FloorSetup, new Battle.Phase.BattlePhaseFloorSetup());
			_phase.Add(Phase.CharaSetup, new Battle.Phase.BattlePhaseCharaSetup());
			_phase.Add(Phase.PlayerAction, new Battle.Phase.BattlePhasePlayerAction());
			_phase.Add(Phase.PlayerActionProcess, new Battle.Phase.BattlePhasePlayerActionProcess());
			_phase.Add(Phase.PlayerActionEnd, new Battle.Phase.BattlePhasePlayerActionEnd());
			_phase.Add(Phase.Adv, new Battle.Phase.BattlePhaseAdv());
			_phase.Add(Phase.NextStage, new Battle.Phase.BattlePhaseNextStage());

			_phase.Start(this, Phase.Start);

			_isInitialized = true;


			// 行動終了時等、特定のタイミングでフェーズを切り替える
			_eventManager.Add<EventChangePhase>(this, 
				_ev => 
				{
					// 行動終了時のPhase切り替えの予約
					_model.NextPhaseMoveReservation = _ev.phase;
				});

			// 始めは１階層
			View.UIHeaderView.SetLevel(_model.Level);

			UnityEngine.Random.InitState(1);
		}

		public override void UpdateScene()
		{
		}

		/// <summary>
		/// シーン終了時
		/// </summary>
		public override void StopScene() { }

		/// <summary>
		/// シーン遷移前に呼び出される
		/// </summary>
		/// <returns></returns>
		public override IObservable<Unit> SceneStopAsync(Argument nextArgument) =>
			Observable.Return(Unit.Default);


		/// <summary>
		/// 次のレベルに移動する
		/// </summary>
		public void ApplyNextLevel()
		{
			_model.NextLevel();

			// 移動した階層を今の階層とする
			_mapManager.ChangeNextLevel(_model.Level);

			// キャラクタ管理者
			_charaManager.ChangeNextLevel(_mapManager.CurrentTilemap);

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

			_phase.Change(nextPhase, null);

			return true;
		}

		#endregion


		#region private 関数


		#endregion


		#region MonoBegaviour

		private void Start()
		{
			/////
			QuickStart();
		}

		private void Update()
		{
			_phase.Update();
		}

		#endregion
	}
}