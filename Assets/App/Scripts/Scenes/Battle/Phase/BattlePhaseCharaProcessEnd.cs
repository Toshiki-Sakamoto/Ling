//
// BattlePhaseCharaProcessEnd.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.23
//

using Ling.Const;
using Ling.Map.TileDataMapExtensions;

namespace Ling.Scenes.Battle.Phase
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


		private Chara.CharaManager _charaManager;
		private Map.MapManager _mapManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		protected override void AwakeInternal()
		{
			_charaManager = Resolve<Chara.CharaManager>();
			_mapManager = Resolve<Map.MapManager>();
		}

		public override void Init()
		{
			// 今の所何もすることないのでプレイヤー行動開始時に戻す
			// 足元確認とか次に入れるほうが良さそう

			var player = _charaManager.Player;

			var tileDataMap = _mapManager.CurrentTileDataMap;
			var tileFlag = tileDataMap.GetTileFlag(player.Model.CellPosition.Value);

			// 下り階段
			if (tileFlag.HasStepDown())
			{
				ConfirmNextStage();
			}
			else
			{
				Change(BattleScene.Phase.PlayerAction);
			}
		}


		#endregion


		#region private 関数

		private void ConfirmNextStage()
		{
			// 選択肢を出す
			var eventMessageSelect = _gameManager.EventHolder.MessageTextSelect;

			eventMessageSelect.text = "階段を降りる?";
			eventMessageSelect.selectTexts = new string[] { "はい", "いいえ" };
			eventMessageSelect.onSelected = selected =>
				{
					// 次のステージに行く
					if (selected == 0)
					{
						//_eventManager.Trigger(new EventChangePhase { phase = BattleScene.Phase.NextStage });
						Change(BattleScene.Phase.NextStage);
					}
					else
					{
						Change(BattleScene.Phase.PlayerAction);
					}
				};

			_eventManager.Trigger(eventMessageSelect);
		}

		#endregion
	}
}
