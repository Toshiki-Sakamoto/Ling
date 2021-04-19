//
// BattlePhaseFloorSetup.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.02
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
	public class BattlePhaseFloorSetup : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Map.Builder.IManager _builderManager = null;
		[Inject] private Map.Builder.BuilderFactory _builderFactory = null;
		[Inject] private Map.MapManager _mapManager = null;

		private bool _isLoadFinish;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseInit()
		{
		}

		public override void PhaseUpdate()
		{
			Change(Phase.PlayerAction);
		}

		public override void PhaseStop()
		{
			_isLoadFinish = false;
		}

		#endregion


		#region private 関数

		private void CreateMiniMap()
		{

		}

		#endregion
	}
}
