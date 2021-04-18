//
// BattlePhaseEnemyAction.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
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
	/// 敵の行動
	/// </summary>
	public class BattlePhaseEnemyAction : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Chara.CharaManager _charaManager;
		[Inject] private Map.MapManager _mapManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseStart()
		{
			// 敵全員を生成順に
			// 「攻撃するか」
			// 「移動するか」
			// 「特技を使うか」
			// のそれぞれに分類する
		}

		#endregion


		#region private 関数

		#endregion
	}
}
