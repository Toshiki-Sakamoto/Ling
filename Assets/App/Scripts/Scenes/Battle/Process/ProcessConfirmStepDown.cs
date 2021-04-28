//
// ProcessConfirmStepDown.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.09
//

using Zenject;

namespace Ling.Scenes.Battle.Process
{
	/// <summary>
	/// 
	/// </summary>
	public class ProcessConfirmStepDown : Common.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private BattleManager _battleManager = null;
		[Inject] private Utility.IEventManager _eventManager = null;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		public void Start()
		{
			// 選択肢を出す
			var eventMessageSelect = _battleManager.EventHolder.MessageTextSelect;

			eventMessageSelect.text = "階段を降りる?";
			eventMessageSelect.selectTexts = new string[] { "はい", "いいえ" };
			eventMessageSelect.onSelected = selected =>
				{
					// 次のステージに行く
					if (selected == 0)
					{
						_eventManager.Trigger(new EventChangePhase { phase = Phase.NextStage });
					}

					ProcessFinish();
				};

			_eventManager.Trigger(eventMessageSelect);
		}

		#endregion
	}
}
