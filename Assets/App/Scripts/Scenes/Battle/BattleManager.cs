//
// BattleManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
//

using Cysharp.Threading.Tasks;
using Zenject;

namespace Ling.Scenes.Battle
{
	/// <summary>
	/// BattleScene全体を管理する
	/// </summary>
	public class BattleManager : Utility.MonoSingleton<BattleManager>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private BattleModel _model;
		[Inject] private Utility.IEventManager _eventManager;

		#endregion


		#region プロパティ

		public EventHolder EventHolder { get; } = new EventHolder();

		public BattleModel Model => _model;

		/// <summary>
		/// バトルメッセージ送信中の場合true
		/// </summary>
		public bool IsMessageSending { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void ShowMessage(string text)
		{
			var message = EventHolder.MessageText;
			message.text = text;

			_eventManager.Trigger(message);
		}

		public async UniTask WaitMessageSending()
		{
			if (IsMessageSending)
			{
				await UniTask.WaitWhile(() => IsMessageSending);
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
