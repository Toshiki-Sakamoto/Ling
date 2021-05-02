//
// MessagrConverter.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.28
//

using Ling.Chara;
using UnityEngine;
using Zenject;

namespace Ling.Scenes.Battle.Message
{
	/// <summary>
	/// 各種イベントをメッセージに変換して流す
	/// </summary>
	[RequireComponent(typeof(MessageControl))]
	public class MessageConverter : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Utility.IEventManager _eventManager;

		private MessageControl _control;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		private void ShowMessage(string text) =>
			_control.ShowMessage(text);

		private void Awake()
		{
			_control = GetComponent<MessageControl>();
			
			// Chara
			_eventManager.Add<EventDead>(this, ev => 
				{
					// キャラが死亡した
					ShowMessage($"{ev.chara.Name} は 倒れた");
				});

			_eventManager.Add<EventDamage>(this, ev => 
				{
					// キャラにダメージ
					ShowMessage($"{ev.chara.Name} は {ev.value}ダメージを受けた");
				});

			_eventManager.Add<Chara.EventAddedExp>(this, ev => 
				{
					// 経験値ゲット
					ShowMessage($"{ev.Chara.Name} は {ev.Exp}経験値獲得");
				});

			_eventManager.Add<Chara.EventLevelUp>(this, ev => 
				{
					// レベルアップ
					ShowMessage($"<color=#ffa500ff>{ev.Chara.Name} は レベルが{ev.Lv}になった</color>");
				});
		}

		private  void OnDestroy()
		{
			_eventManager.RemoveAll(this);
		}

		#endregion
	}
}
