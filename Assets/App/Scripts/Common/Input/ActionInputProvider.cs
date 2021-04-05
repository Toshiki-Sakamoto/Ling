// 
// ActionInputProvider.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.05
// 

using UnityEngine;
using UnityEngine.InputSystem;

namespace Ling.Common.Input
{
	/// <summary>
	/// アタックやメニューなど行動に関するInputProvider
	/// </summary>
	public class ActionInputProvider : MonoBehaviour, 
		InputControls.IActionActions,
		IInputProvider<InputControls.IActionActions>
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private InputControls _controls;

		#endregion


		#region プロパティ

		public InputControls Controls => _controls;
		
		#endregion


		#region public, protected 関数

		public void OnAttack(InputAction.CallbackContext context)
		{
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

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
			_controls.Action.SetCallbacks(this);

			_controls.Enable();
		}

		private void OnDestroy()
		{
			_controls.Dispose();
		}

		#endregion
	}
}