//
// BattlePhase.Start.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.30
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// 
	/// </summary>
	public class BattlePhaseStart : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数


		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseStart()
		{
		}

		public override void PhaseUpdate()
		{
			Change(Phase.Load);
		}

		public override void PhaseStop()
		{
		}

		#endregion


		#region private 関数

		#endregion
	}
}
