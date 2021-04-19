//
// ParamFunc.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.21
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Utility
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class ParamFuncBase
	{
		public abstract void Call();
	};

	/// <summary>
	/// 引数なし
	/// </summary>
	public class ParamFunc : ParamFuncBase
	{
		private System.Action _act;

		public static ParamFunc Create(System.Action act)
		{
			var instance = new ParamFunc();

			instance._act = act;

			return instance;
		}

		public override void Call()
		{
			_act();
		}
	};

	/// <summary>
	/// 引数一つ
	/// </summary>
	public class ParamFunc<Param1> : ParamFuncBase
	{
		private System.Action<Param1> _act;
		private Param1 _param1;


		public static ParamFunc<Param1> Create(Param1 param1,
											   System.Action<Param1> act)
		{
			var instance = new ParamFunc<Param1>();

			instance._act = act;
			instance._param1 = param1;

			return instance;
		}

		public override void Call()
		{
			_act(_param1);
		}
	};

	/// <summary>
	/// 引数２つ
	/// </summary>
	public class ParamFunc<Param1, Param2> : ParamFuncBase
	{
		private System.Action<Param1, Param2> _act;
		private Param1 _param1;
		private Param2 _param2;


		public static ParamFunc<Param1, Param2> Create(Param1 param1,
													   Param2 param2,
													   System.Action<Param1, Param2> act)
		{
			var instance = new ParamFunc<Param1, Param2>();

			instance._act = act;
			instance._param1 = param1;
			instance._param2 = param2;

			return instance;
		}

		public override void Call()
		{
			_act(_param1, _param2);
		}
	};

	/// <summary>
	/// 引数３つ
	/// </summary>
	public class ParamFunc<Param1, Param2, Param3> : ParamFuncBase
	{
		private System.Action<Param1, Param2, Param3> _act;
		private Param1 _param1;
		private Param2 _param2;
		private Param3 _param3;


		public static ParamFunc<Param1, Param2, Param3> Create(Param1 param1,
														Param2 param2,
														Param3 param3,
														System.Action<Param1, Param2, Param3> act)
		{
			var instance = new ParamFunc<Param1, Param2, Param3>();

			instance._act = act;
			instance._param1 = param1;
			instance._param2 = param2;
			instance._param3 = param3;

			return instance;
		}

		public override void Call()
		{
			_act(_param1, _param2, _param3);
		}
	};
}
