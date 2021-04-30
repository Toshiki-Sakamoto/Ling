//
// BattlePhasePlayerActionProcess.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.03
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
	public class BattlePhasePlayerActionProcess : BattlePhaseBase
	{
		#region 定数, class, enum

		public class Arg : Utility.PhaseArgument
		{
			public Utility.ProcessBase process;   // 行動プロセス
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private bool _isFinish;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseStart()
		{
			var arg = Argument as Arg;

			arg.process
				.SetNextLast<Utility.Process.ProcessCallFunc>()
				.Setup(() =>
				{
					_isFinish = true;
				});
		}

		public override void PhaseUpdate()
		{
			if (!_isFinish) return;

			Change(Phase.PlayerActionEnd);
		}

		public override void PhaseStop()
		{
			_isFinish = false;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
