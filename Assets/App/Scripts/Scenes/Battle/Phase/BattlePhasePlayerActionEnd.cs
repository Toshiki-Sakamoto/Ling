//
// BattlePhasePlayerActionEnd.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.09
//

using Ling.Chara;
using Ling.Map;
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
	public class BattlePhasePlayerActionEnd : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Chara.CharaManager _charaManager;
		private MapManager _mapManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void Awake()
		{
			base.Awake();

			_charaManager = Resolve<CharaManager>();
			_mapManager = Resolve<MapManager>();
		}

		public override void Init()
		{
			// 予約済みフェーズに移動するか
			if (Scene.MoveToResercationPhase())
			{
				return;
			}

			Change(BattleScene.Phase.PlayerAction);
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
