using System.Globalization;
//
// Phase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.30
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Utility
{
	public class PhaseArgument
	{
	}

	public class Phase<T> : MonoBehaviour where T : Enum
	{
		public PhaseArgument Argument { get; set; }

		private PhaseController<T> _controller;


		public void SetController(PhaseController<T> controller) =>
			_controller = controller;

		public void Change(T type, PhaseArgument argument = null) =>
			_controller.Change(type, argument);


		/// <summary>
		/// フェーズが切り替わった時に一度だけ呼び出される
		/// </summary>
		public virtual void PhaseStart() { }

		/// <summary>
		/// フェーズがアクティブ状態の時に呼び出され続ける
		/// </summary>
		public virtual void PhaseUpdate() { }

		/// <summary>
		/// フェーズが終了する時一度だけ呼び出される
		/// </summary>
		public virtual void PhaseStop() { }
	}
}
