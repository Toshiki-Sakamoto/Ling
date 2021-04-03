// 
// InputManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.03.21
// 

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

namespace Ling.Common.Input
{
	/// <summary>
	/// InputProvider継承用インターフェイス
	/// </summary>
	public interface IInputProvider<TInputActions>
		where TInputActions : class
	{
		InputControls Controls { get; }
	}

	public interface IInputManager
	{
		void Bind<TInputActions>(IInputProvider<TInputActions> inputProvider)
			where TInputActions : class;

		IInputProvider<TInputActions> Resolve<TInputActions>()
			where TInputActions : class;
	}
	
	/// <summary>
	/// InputProvider管理者
	/// </summary>
	public class InputManager : MonoBehaviour, IInputManager
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数
		
		private Dictionary<Type, System.Object> _providersDict = new Dictionary<Type, System.Object>();

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		
		public void Bind<TInputActions>(IInputProvider<TInputActions> inputProvider)
			where TInputActions : class
		{
			var inputType = typeof(TInputActions);
			if (_providersDict.TryGetValue(inputType, out var _))
			{
				Utility.Log.Error($"すでにBindされている型です{inputType.ToString()}");
				return;
			}

			_providersDict.Add(inputType, inputProvider);
		}

		public IInputProvider<TInputActions> Resolve<TInputActions>()
			where TInputActions : class
		{
			var inputType = typeof(TInputActions);
			if (!_providersDict.TryGetValue(inputType, out var result))
			{
				Utility.Log.Error($"Bindされていません{inputType.ToString()}");
				return null;
			}

			return (IInputProvider<TInputActions>)result;
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		void Awake()
		{
			
		}

		#endregion
	}
}