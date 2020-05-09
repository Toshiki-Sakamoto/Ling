//
// Function.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.04
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Utility
{
	/// <summary>
	/// 
	/// </summary>
	public interface IFunction
    {
		void Execute();
	}

	public class Function : IFunction
	{
		private System.Action _function;


		public Function(System.Action function)
		{
			_function = function;
		}

		public void Execute() => _function?.Invoke();
	}


	public class FunctionParam1<TParam1> : IFunction
	{
		private TParam1 _param1;
		private System.Action<TParam1> _function;


		public FunctionParam1(TParam1 param1, System.Action<TParam1> function)
		{
			_param1 = param1;
			_function = function;
		}

		public void Execute() => _function?.Invoke(_param1);
	}

	public class FunctionParam2<TParam1, TParam2> : IFunction
	{
		private TParam1 _param1;
		private TParam2 _param2;
		private System.Action<TParam1, TParam2> _function;


		public FunctionParam2(TParam1 param1, TParam2 param2, System.Action<TParam1, TParam2> function)
		{
			_param1 = param1;
			_param2 = param2;
			_function = function;
		}

		public void Execute() => _function?.Invoke(_param1, _param2);
	}

	public class FunctionParam3<TParam1, TParam2, TParam3> : IFunction
	{
		private TParam1 _param1;
		private TParam2 _param2;
		private TParam3 _param3;
		private System.Action<TParam1, TParam2, TParam3> _function;


		public FunctionParam3(TParam1 param1, TParam2 param2, TParam3 param3, System.Action<TParam1, TParam2, TParam3> function)
		{
			_param1 = param1;
			_param2 = param2;
			_param3 = param3;
			_function = function;
		}

		public void Execute() => _function?.Invoke(_param1, _param2, _param3);
	}
}
