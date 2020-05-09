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

		private CharaManager _charaManager;
		private Player _player;
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
			_player = _charaManager.Player;

			// 階段の上にいるか
			var tileDataMap = _mapManager.CurrentTileDataMap;
			var tileFlag = tileDataMap.GetTileFlag(_player.CellPos);

			// 下り階段
			if (tileFlag.HasStepDown())
			{
				// 選択肢を出す
				var eventMessageSelect = EventHolder.MessageTextSelect;

				eventMessageSelect.text = "階段を降りる?";
				eventMessageSelect.selectTexts = new string[] { "はい", "いいえ" };
				eventMessageSelect.onSelected = selected =>
				{
					Change(BattleScene.Phase.PlayerAction);
				};

				_eventManager.Trigger(eventMessageSelect);
			}
			else
			{
				Change(BattleScene.Phase.PlayerAction);
			}
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
