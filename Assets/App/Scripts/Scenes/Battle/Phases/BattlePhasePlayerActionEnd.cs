//
// BattlePhasePlayerActionEnd.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.09
//

using Ling.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// 
	/// </summary>
	public class BattlePhasePlayerActionEnd : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Chara.CharaManager _charaManager;
		[Inject] private MapManager _mapManager;

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
			// 予約済みフェーズに移動するか
			if (Scene.MoveToResercationPhase())
			{
				return;
			}

			Change(Phase.PlayerAction);
		}

		public override void PhaseUpdate()
		{
		}

		public override void PhaseStop()
		{
		}

		#endregion


		#region private 関数

		#endregion
	}
}
