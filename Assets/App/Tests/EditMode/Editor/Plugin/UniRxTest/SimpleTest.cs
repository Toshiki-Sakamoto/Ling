//
// SimpleTest.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.10
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;

using NUnit.Framework;

using Assert = UnityEngine.Assertions.Assert;


namespace Ling.Tests.Plugin.UniRxTest
{
	/// <summary>
	/// UniRxを使ってみる
	/// 単純なテスト
	/// </summary>
	public class SimpleTest
    {
		#region 定数, class, enum

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
		public void Subject()
		{
			Subject<string> subject = new Subject<string>();

			subject.Subscribe(text_ => Debug.Log($"【Subject】{text_}"));
			subject.DoOnCompleted(() => Debug.Log("【Subject Complete】"));

			subject.OnNext("テスト成功");
			subject.OnCompleted();
		}


		#endregion


		#region private 関数

		#endregion
	}
}
