//
// UniRxSelectManyTest.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.22
//

using Assert = UnityEngine.Assertions.Assert;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;
using UniRx;
using System;


namespace Ling.Tests.PlayMode.Plugin.UniRx
{
	/// <summary>
	/// UniRx SelectManyテスト
	/// </summary>
	public class UniRxSelectManyTest
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

		[UnityTest]
		public IEnumerator SelectManyTest()
		{
			int count = 0;
			bool finished = false;

			var subject = new Subject<int>();
			subject
				.SelectMany(num => Observable.IntervalFrame(1).Take(2).Select(_ => num))
				.Subscribe(num => count += num, () => finished = true);

			subject.OnNext(1);
			subject.OnCompleted();

			yield return new WaitUntil(() => finished);

			Assert.AreEqual(2, count, "SelectManyでIObservable<T>を返し、合成された");
		}

		#endregion


		#region private 関数

		#endregion
	}
}
