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
			CharaCreate,
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[Inject] private Map.Builder.IManager _builderManager = null;
		[Inject] private Map.Builder.BuilderFactory _builderFactory = null;

		[SerializeField] private BattleView _view = null;

		private Utility.PhaseObj<Phase> _phase = new Utility.PhaseObj<Phase>();

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


			var builderData = new Map.Builder.BuilderData();

			var builder = _builderFactory.Create(Map.Builder.BuilderConst.BuilderType.Split);
			builder.Initialize(20, 20);

			_builderManager.SetData(builderData);
			_builderManager.SetBuilder(builder);

			_builderManager.Builder.Execute();
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

		#endregion
	}
}