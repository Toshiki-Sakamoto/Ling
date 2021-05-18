//
// BattlePhaseCharaProcessEnd.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.23
//

using Ling.Const;
using Ling.Map.TileDataMapExtensions;
using Zenject;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// キャラのプロセス終了処理
	/// </summary>
	public class BattlePhaseCharaProcessEnd : BattlePhaseBase
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

		public override void PhaseInit()
		{
		}

		public override void PhaseStart()
		{
			// 今の所何もすることないのでプレイヤー行動開始時に戻す
			// 足元確認とか次に入れるほうが良さそう

			var player = _charaManager.Player;

			var tileDataMap = _mapManager.CurrentTileDataMap;
			var tileFlag = tileDataMap.GetTileFlag(player.Model.CellPosition.Value);

			// アイテム確認
			if (tileFlag.HasItem())
			{
				Change(Phase.ItemGet, new BattlePhaseItemGet.Arg { NextPhase = Phase.CharaProcessEnd });
			}
			// 下り階段
			else if (tileFlag.HasStepDown())
			{
				ConfirmNextStage();
			}
			else
			{
				Change(Phase.PlayerAction);
			}
		}


		#endregion


		#region private 関数

		private void ConfirmNextStage()
		{
			// 選択肢を出す
			var eventMessageSelect = _battleManager.EventHolder.MessageTextSelect;

			eventMessageSelect.text = "<color=#ffff00>階段を降りる?</color>";
			eventMessageSelect.selectTexts = new string[] { "はい", "いいえ" };
			eventMessageSelect.onSelected = selected =>
				{
					// 次のステージに行く
					if (selected == 0)
					{
						//_eventManager.Trigger(new EventChangePhase { phase = BattleScene.Phase.NextStage });
						Change(Phase.NextStage);
					}
					else
					{
						Change(Phase.PlayerAction);
					}
				};

			_eventManager.Trigger(eventMessageSelect);
		}

		#endregion
	}
}
