//
// MoveInputProvider.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.02.28
//

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

namespace Ling.Common.Input
{
	/// <summary>
	/// IMoveActionsを継承した移動用InputProvider
	/// </summary>
	public class MoveInputProvider : MonoBehaviour, 
		InputControls.IMoveActions, 
		IInputProvider<InputControls.IMoveActions>
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private InputControls _controls;

		#endregion


		#region プロパティ

		public InputControls Controls => _controls;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数


        public void OnLeft(InputAction.CallbackContext context)
		{
		}

		public void OnLeftUp(InputAction.CallbackContext context)
		{
		}

        public void OnUp(InputAction.CallbackContext context)
		{
		}

        public void OnRightUp(InputAction.CallbackContext context)
		{}

        public void OnRight(InputAction.CallbackContext context)
		{}

        public void OnRightDown(InputAction.CallbackContext context)
		{}

        public void OnDown(InputAction.CallbackContext context)
		{}

        public void OnLeftDown(InputAction.CallbackContext context)
		{}
		
		public void OnDirSwitch(InputAction.CallbackContext context)
		{}

		#endregion


		#region private 関数

		private void OnEnable()
		{
			_controls.Enable();
		}

		private void OnDisable()
		{
			_controls.Disable();
		}

		private void Awake()
		{
			_controls = new InputControls();
			_controls.Move.SetCallbacks(this);

			_controls.Enable();
		}

		private void OnDestroy()
		{
			_controls.Dispose();
		}

		#endregion
	}
}
