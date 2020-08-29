// 
// TouchPointEventTrigger.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.27
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Ling.Utility
{
	/// <summary>
	/// イベントの実態
	/// </summary>
	public class EventTouchPoint
	{
		public EventTriggerType type;
		public string strParam;
		public int intParam;
		public GameObject gameObject;
	}

	/// <summary>
	/// タッチ時にイベント発行
	/// </summary>
	public class TouchPointEventTrigger : EventTrigger 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		public static EventTouchPoint cacheInstance;	// キャッシュして使い回す

		#endregion


		#region private 変数

		[SerializeField] private bool _isCreateNewEvent;	// イベントが発行されるたびに新しいインスタンスを作成する
		[SerializeField] private string _stringParam;	// 特殊な文字列を贈りたいとき
		[SerializeField] private int _intParam;
		[SerializeField] private GameObject _gameObject = null;	// 設定すれば送られる

		#endregion


		#region プロパティ

		public int IntParam { set { _intParam = value; } }
		public string StringParam { set { _stringParam = value; } }

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			if (cacheInstance == null)
			{
				cacheInstance = new EventTouchPoint();
			}
		}

		public override void OnPointerClick(PointerEventData data) =>
			EventTrigger(EventTriggerType.PointerClick);


		private void EventTrigger(EventTriggerType type)
		{
			var ev = cacheInstance;
			if (_isCreateNewEvent) 
			{
				ev = new EventTouchPoint();
			}

			ev.type = type;
			ev.strParam = _stringParam;
			ev.intParam = _intParam;
			ev.gameObject = _gameObject;

			EventManager.SafeTrigger(ev);
		}

		#endregion
	}
}