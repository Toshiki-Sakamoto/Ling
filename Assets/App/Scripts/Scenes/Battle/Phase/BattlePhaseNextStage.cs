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
	public class BattlePhaseNextStage : Utility.PhaseScene<BattleScene.Phase, BattleScene>.Base
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private MapManager _mapManager = null;

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
		}

		public override void Init() 
		{
			// 次の階層を作成
			var currentMapIndex = _mapManager.CurrentMapIndex;

			var buildNextMapObservable = _mapManager.BuildNextMap();
			buildNextMapObservable.Subscribe(_a =>
				{
					// マップを移動
					var createAndMoveObservable = _mapManager.CreateAndMoveNextMap();
					createAndMoveObservable.Subscribe(_b => 
					{
						Change(BattleScene.Phase.PlayerAction);
					});  
				});
		}

		public override void Proc() 
		{
		}

		public override void Term() 
		{ 
		}

		#endregion


		#region private 関数

		#endregion
	}
}
