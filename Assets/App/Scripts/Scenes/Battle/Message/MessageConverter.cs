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
					ShowMessage("死亡した");
				});
		}

		private  void OnDestroy()
		{
			_eventManager.RemoveAll(this);
		}

		#endregion
	}
}
