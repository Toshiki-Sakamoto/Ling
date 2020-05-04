//
// BattlePhaseBase.cs
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
	public class BattlePhaseBase : Utility.PhaseScene<BattleScene.Phase, BattleScene>.Base
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		protected GameManager _gameManager;
		protected Utility.IEventManager _eventManager;
		protected Utility.ProcessManager _processManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		public override void Awake()
		{
			_gameManager = GameManager.Instance;

			_eventManager = _gameManager.Resolve<Utility.IEventManager>();
			_processManager = _gameManager.Resolve<Utility.ProcessManager>();
		}

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
