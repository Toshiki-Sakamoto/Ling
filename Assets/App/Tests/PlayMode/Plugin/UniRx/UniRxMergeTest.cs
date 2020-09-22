//
// UniRxMergeTest.cs
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
	/// UniRxのMerge挙動テスト
	/// </summary>
	public class UniRxMergeTest
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
		public IEnumerator MergeTest()
		{
			// ２つのIObservableを合成する
			int count = 0;
			bool finished = false;

			var o1 = Observable.IntervalFrame(1).Take(2).Select(_ => 1);
			var o2 = Observable.IntervalFrame(1).Take(1).Select(_ => 1);

			o1.Merge(o2).Subscribe(num => count += num, () => finished = true);

			yield return new WaitUntil(() => finished);

			Assert.AreEqual(3, count, "Mergeによって２つのIObservableが合成された");
		}

		[UnityTest]
		public IEnumerator MergeArrayTest()
		{
			// 配列を受け取る
			int count = 0;
			bool finished = false;

			var o1 = Observable.IntervalFrame(1).Take(2).Select(_ => 1);
			var o2 = Observable.IntervalFrame(1).Take(1).Select(_ => 1);

			Observable.Merge(o1, o2).Subscribe(num => count += num, () => finished = true);

			yield return new WaitUntil(() => finished);

			Assert.AreEqual(3, count, "Mergeによって２つのIObservableが合成された");
		}

		[UnityTest]
		public IEnumerator MergeResultObservableTest()
		{
			int count = 0;
			bool finished = false;

			var subject = new Subject<int>();
			subject
				// IObservable<int>からIObservable<IObservable<int>>に変換
				.Select(i => Observable.IntervalFrame(1).Take(2).Select(_ => 1))
				// IObservable<IObservable<T>>からIObservabel<T>へマージ
				.Merge()
				.Subscribe(num => count += num, () => finished = true);

			subject.OnNext(1);
			subject.OnCompleted();
			
			yield return new WaitUntil(() => finished);

			Assert.AreEqual(2, count, "MergeによってIObservabvle<IObservabvle<T>>からIObservavble<T>にマージされた");
		}

		#endregion


		#region private 関数

		#endregion
	}
}
