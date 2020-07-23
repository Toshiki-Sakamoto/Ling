// 
// InjectTestClass.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.02.16
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Ling.Tests.PlayMode.Plugin.ZenjectTest
{
	/// <summary>
	/// Injectを試すクラス
	/// </summary>
	public class InjectTestClass : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[Inject] private IExample _fieldInjection;	// フィールドへの注入
		private IExample _methodInjection;          // メソッドへの注入	

		#endregion


		#region プロパティ

		public IExample FieldInjection => _fieldInjection;
		public IExample MethodInjection => _methodInjection;


		/// <summary>
		/// プロパティでの注入
		/// </summary>
		[Inject] public IExample PropertyInjection { get; private set; }

		/// <summary>
		/// IDで違うインスタンスが生成されるかテスト
		/// </summary>
		[Inject(Id = ExampleIdTest.ID.First)] public IExampleIdTest IdInject_1 { get; private set; }
		[Inject(Id = ExampleIdTest.ID.Second)] public IExampleIdTest IdInject_2 { get; private set; }

		#endregion


		#region public, protected 関数

		/// <summary>
		/// MonoBehaviourはコンストラクタを持てないので[Inject]メソッドを使用して
		/// 依存関係を注入するのが推奨
		/// </summary>
		/// <param name="example"></param>
		[Inject] 
		public void InitializableManager(IExample example)
		{
			_methodInjection = example;
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}