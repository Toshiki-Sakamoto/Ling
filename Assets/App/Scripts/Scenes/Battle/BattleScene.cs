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
			Start,
			Load,
			FloorSetup,
			CharaSetup,
			PlayerAction,
			EnemyAction,
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private BattleView _view = null;

		private Utility.PhaseScene<Phase, BattleScene> _phase = new Utility.PhaseScene<Phase, BattleScene>();

		#endregion


		#region プロパティ


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
			_phase.Add(Phase.Start, new Battle.Phase.BattlePhaseStart());
			_phase.Add(Phase.Load, new Battle.Phase.BattlePhaseLoad());
			_phase.Add(Phase.FloorSetup, new Battle.Phase.BattlePhaseFloorSetup());
			_phase.Add(Phase.CharaSetup, new Battle.Phase.BattlePhaseCharaSetup());
			_phase.Add(Phase.PlayerAction, new Battle.Phase.BattlePhasePlayerAction());

			_phase.Start(this, Phase.Start);
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

		#endregion


		#region private 関数


		#endregion


		#region MonoBegaviour

		private void Awake()
		{
			StartScene();
		}

		private void Update()
		{
			_phase.Update();
		}

		#endregion
	}
}