﻿//
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

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// 
	/// </summary>
	public class BattlePhasePlayerActionProcess : BattlePhaseBase
    {
		#region 定数, class, enum

		public class Argument : Utility.PhaseArgBase
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

		public override void Init()
		{
			var arg = Arg as Argument;

			arg.process
				.SetNext<Utility.Process.ProcessCallFunc>()
				.Setup(() => 
				{
					_isFinish = true;
				});
		}

		public override void Proc()
		{
			if (!_isFinish) return;

			//
			Change(BattleScene.Phase.PlayerAction);
		}

		public override void Term()
		{
			_isFinish = false;
		}

		#endregion


		#region private 関数

		#endregion
	}
}