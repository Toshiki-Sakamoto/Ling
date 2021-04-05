// 
// ButtonEx.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.02.28
// 

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Ling.Common.UI
{
	/// <summary>
	/// UnityのButton拡張
	/// </summary>
	public class ButtonEx : Button
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private UnityEvent _pressedDownEvent = default;
		[SerializeField] private UnityEvent _pressedUpEvent = default;

		private bool _isDown = false;

		#endregion


		#region プロパティ

		public UnityEvent OnPressedDown => _pressedDownEvent;
		public UnityEvent OnPressedUp => _pressedUpEvent;

		#endregion


		#region public, protected 関数

		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);

			_isDown = true;
			OnPressedDown.Invoke();
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);

			if (!_isDown)
			{

			}
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion

		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion



		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}