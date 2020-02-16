//
// ZenjectTest.cs
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
using UnityEngine.SceneManagement;
using NUnit.Framework;
using UnityEngine.TestTools;

using Assert = UnityEngine.Assertions.Assert;

namespace Ling.Tests.PlayMode.Plugin.ZenjectTest
{
	/// <summary>
	/// Injectがうまくいくかテスト
	/// </summary>
	public interface IExample { }
	public class Example : IExample { }

	/// <summary>
	/// IDで違うインスタンスが取れるかテストするクラス
	/// </summary>
	public interface IExampleIdTest {}
	public class ExampleIdTest : IExampleIdTest
	{
		public enum ID { First, Second }
	}

	/// <summary>
	/// ZenjectのPlayModeテストしてみる
	/// </summary>
	public class MainTest
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private InjectTestClass _injectTextClass;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		[SetUp]
		public void Setup()
		{
			// あるシーンのテストは1フレーム待たないとFindできなかった。
			SceneManager.LoadScene("ZenjectTestScene");
		}

		[UnityTest]
		public IEnumerator MonoInstallerInjectTest()
		{
			// InjectTestClass をまずは探す
			_injectTextClass = GameObject.FindObjectOfType<InjectTestClass>();

			Assert.IsNotNull(_injectTextClass, "InjectTestClassが見つからない");
		
			Assert.IsNotNull(_injectTextClass.FieldInjection, "フィールドに対して依存性の注入がされた");
			Assert.IsNotNull(_injectTextClass.PropertyInjection, "プロパティに対して依存性の注入がされた");
			Assert.IsNotNull(_injectTextClass.MethodInjection, "メソッドに対して依存性の注入がされた");

			yield return null;

		}

		#endregion


		#region private 関数

		#endregion
	}
}
