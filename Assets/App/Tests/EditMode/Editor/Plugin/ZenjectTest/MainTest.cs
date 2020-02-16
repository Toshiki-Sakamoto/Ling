//
// MainTest.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.02.16
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Zenject;

using NUnit.Framework;

using Assert = UnityEngine.Assertions.Assert;


namespace Ling.Tests.Plugin.ZenjectTest
{
	/// <summary>
	/// Zenjectを使ってみる
	/// </summary>
	public class MainTest
	{
		#region 定数, class, enum

		/// <summary>
		/// IInitializableのテスト
		/// 初期化時に Initialize() が呼ばれることになる
		/// </summary>
		public class ExampleInitializable : IInitializable
		{
			public bool IsValid { get; private set; }

			public void Initialize() { Debug.Log("SSS"); IsValid = true; }
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void ResolveTest()
		{
			var container = new DiContainer();
			container.BindInstance("hoge");

			Assert.AreEqual("hoge", container.Resolve<string>());
		}

		[Test]
		public void ResolveAllTest()
		{
			var container = new DiContainer();
			container.BindInstance("hoge");
			container.BindInstance("fuga");

			var all = container.ResolveAll<string>();

			Assert.AreEqual(2, all.Count, "配列の数は一致している");
			Assert.AreEqual("hoge", all[0], "配列の一番目の解決が一致");
			Assert.AreEqual("fuga", all[1], "配列の二番目の解決が一致");
		}

		[Test]
		public void IInitializable()
		{
			var container = new DiContainer();
			container.BindInterfacesAndSelfTo<ExampleInitializable>().AsSingle();

			var instance = container.Resolve<ExampleInitializable>();

			Assert.IsNotNull(instance);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
