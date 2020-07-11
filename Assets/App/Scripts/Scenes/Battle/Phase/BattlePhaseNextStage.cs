//
// BattlePhaseNextStage.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.05
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.Phase
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

		private MapManager _mapManager = null;
		private Chara.CharaManager _charaManager = null;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void Awake() 
		{
			base.Awake();

			_mapManager = Resolve<MapManager>();
			_charaManager = Resolve<Chara.CharaManager>();
		}

		public override void Init() 
		{
			BuildNextMap();
		}

		public override void Proc() 
		{
		}

		public override void Term() 
		{ 
		}

		#endregion


		#region private 関数

		private void BuildNextMap()
		{
			var buildNextMapObservable = _mapManager.BuildNextMap();
			var createMapViewObservable = _mapManager.CreateMapView();

			buildNextMapObservable
				.Concat(createMapViewObservable)
				.Subscribe(onNext: _ => 
					{}, 
					onCompleted: () =>
					{
						// 動きを制御
						var process = _processManager.Attach<Process.ProcessNextStageAnim>();
						process.OnFinish =
							() =>
							{
								ApplyNextLevel();
							};
					});
		}

		private void ApplyNextLevel()
		{
			// 次の階層に移動処理する
			Scene.ApplyNextLevel();

			// 次のステージ移動完了
			Change(BattleScene.Phase.PlayerAction);
		}

		#endregion
	}
}
