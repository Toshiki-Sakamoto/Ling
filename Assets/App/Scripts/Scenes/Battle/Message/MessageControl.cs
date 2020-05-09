//
// MessageControl.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.08
//

using Ling.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.Message
{
	/// <summary>
	/// 
	/// </summary>
	public class MessageControl : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private MessageView _view = null;
		[SerializeField] private MessageSelect _select = null;

		[Inject] private IEventManager _eventManager = null;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		private void Start()
		{
			_eventManager.Add<EventMessageText>(this,
				ev_ =>
				{
					_view.SetText(ev_.text);
				});

		}

		private void OnDestroy()
		{
			_eventManager.RemoveAll(this);
		}

		#endregion
	}
}
