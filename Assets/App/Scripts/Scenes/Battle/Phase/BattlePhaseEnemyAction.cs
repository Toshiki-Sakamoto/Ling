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

namespace Ling.Scenes.Battle.Phase
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

		private Chara.CharaManager _charaManager;
		private Map.MapManager _mapManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void Init()
		{
			_charaManager = Resolve<Chara.CharaManager>();
			_mapManager = Resolve<Map.MapManager>();

			// 敵全員を生成順に
			// 「攻撃するか」
			// 「移動するか」
			// 「特技を使うか」
			// のそれぞれに分類する
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
