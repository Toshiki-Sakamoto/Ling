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
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.Phase
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

		private Map.Builder.IManager _builderManager = null;
		private Map.Builder.BuilderFactory _builderFactory = null;
		private MapManager _mapManager = null;

		private bool _isLoadFinish;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void Awake()
		{
			base.Awake();

			_builderManager = Resolve<Map.Builder.IManager>();
			_builderFactory = Resolve<Map.Builder.BuilderFactory>();
			_mapManager = Resolve<MapManager>();
		}

		public override void Proc() 
		{
			Change(BattleScene.Phase.CharaSetup);
		}

		public override void Term() 
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
