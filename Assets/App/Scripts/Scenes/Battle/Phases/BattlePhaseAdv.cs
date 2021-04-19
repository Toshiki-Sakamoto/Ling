//
// BattlePhaseAdv.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.04
//

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
	public class BattlePhaseAdv : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Adv.Engine.Manager _advManager = null;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseInit()
		{
		}

		public override void PhaseStart()
		{
			_advManager.Load("test.txt");
			_advManager.AdvStart();

			_advManager.OnFinish = () =>
				{
					Change(Phase.PlayerAction);
				};
		}

		#endregion


		#region private 関数

		#endregion
	}
}
