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

namespace Ling.Scenes.Battle.Phase
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

		Adv.Engine.Manager _advManager = null;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		protected override void AwakeInternal()
		{
			_advManager = Resolve<Adv.Engine.Manager>();
		}

		public override void Init()
		{
			_advManager.Load("test.txt");
			_advManager.AdvStart();

			_advManager.OnFinish = () =>
				{
					Change(BattleScene.Phase.PlayerAction);
				};
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
