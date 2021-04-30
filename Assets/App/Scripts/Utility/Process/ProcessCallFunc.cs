//
// ProcessCallFunc.cs
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
using Utility;
using Zenject;

namespace Utility.Process
{
	/// <summary>
	/// 指定されたメソッドを呼び出す
	/// </summary>
	public class ProcessCallFunc : ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Utility.IFunction _function;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public ProcessCallFunc Setup(System.Action action)
		{
			_function = new Function(action);
			return this;
		}

		public ProcessCallFunc Setup<TParam1>(TParam1 param1, System.Action<TParam1> action)
		{
			_function = new FunctionParam1<TParam1>(param1, action);
			return this;
		}

		public ProcessCallFunc Setup<TParam1, TParam2>(TParam1 param1, TParam2 param2, System.Action<TParam1, TParam2> action)
		{
			_function = new FunctionParam2<TParam1, TParam2>(param1, param2, action);
			return this;
		}

		public ProcessCallFunc Setup<TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3, System.Action<TParam1, TParam2, TParam3> action)
		{
			_function = new FunctionParam3<TParam1, TParam2, TParam3>(param1, param2, param3, action);
			return this;
		}


		public void Start()
		{
			_function.Execute();

			ProcessFinish();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
