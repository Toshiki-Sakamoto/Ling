//
// Test2.1.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.22
//

using NUnit.Framework;
using UniRx;
using System;
using UnityEngine.TestTools;
using UnityEngine;
using System.Collections;

using Assert = UnityEngine.Assertions.Assert;


namespace  Ling.Tests.PlayMode.Plugin.UniRx.UniRxUniTaskBook
{
	/// <summary>
	/// 
	/// </summary>
	public class Sample2_1
    {
		#region 定数, class, enum

		public class PrintLogObserver<T> : IObserver<T>
		{
			public void OnCompleted()
			{
				UnityEngine.Debug.Log("OnCompleted");
			}

			public void OnError(Exception error)
			{
				UnityEngine.Debug.LogError(error);
			}

			public void OnNext(T value)
			{
				UnityEngine.Debug.Log(value);
			}
		}

		#endregion


		#region public, protected 変数

		public IObservable<int> CountDownObservable => _subject;

		#endregion


		#region private 変数

		private Subject<int> _subject;
		
		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		[UnityTest]
		public IEnumerator CountDownEeventProviderEvent()
		{
			var printLogObserver = new PrintLogObserver<int>();
			_subject = new Subject<int>();

			CountDownObservable
				.Subscribe(printLogObserver);

			int count = 10;
			while (count > 0)
			{
				_subject.OnNext(count--);

				yield return new WaitForEndOfFrame();
			}

			_subject.OnNext(0);
			_subject.OnCompleted();
		}

		[Test]
		public void OperatorTest()
		{
			var subject = new Subject<int>();

			// そのまま
			subject.Subscribe(x => Debug.Log("raw:" + x));

			subject
				.Where(x => x > 0)
				.Subscribe(x => Debug.Log("filter:" + x));

			subject.OnNext(1);
			subject.OnNext(-1);
			subject.OnNext(3);
			subject.OnNext(0);

			subject.OnCompleted();
			subject.Dispose();
		}

		[Test]
		public void ErrorTest()
		{
			int counter = 0;
			bool error = false;
			var subject = new Subject<string>();

			subject
				.Select(str => int.Parse(str))
				.Subscribe(
					x => { Debug.Log(x); counter++; },
					ex => { Debug.LogWarning("例外が発生しました:" + ex.Message); error = true; },
					() => Debug.Log("OnCOmpleted"));

			subject.OnNext("1");
			subject.OnNext("2");

			// int.Parseに失敗して例外が発生する
			subject.OnNext("Three");

			Assert.IsTrue(error, "エラーが発行されたのでカウンタが1");

			// OnErrorメッセージ発生により購読は終了済み
			subject.OnNext("4");

			Assert.AreNotEqual(counter, 3, "終了しているのでカウントされない");

			// ただしSubject自体が例外出したわけじゃないのでSubjectは正常稼働中
			// そのため再購読すれば利用ができる
			counter = 0;

			subject.Subscribe(
				x => { Debug.Log(x); counter = 1; },
				() => Debug.Log("Completed"));

			subject.OnNext("Hellor");
			subject.OnCompleted();
			subject.Dispose();

			Assert.AreEqual(1, counter, "再購読したらカウントが 1 になった");
		}


		/// <summary>
		/// Subscribeを呼び出すタイミングと挙動についてのテスト
		/// </summary>
		[Test]
		public void SubscriveTimingTest()
		{
			string result = "";
			var subject = new Subject<string>();

			// OnNextの内容をスペース区切りで連結し、最後の一つだけを出力するObservable
			var appendStringObservable = subject
				.Scan((prev, current) => prev + " " + current)
				.Last();

			appendStringObservable.Subscribe(x => result = x);

			subject.OnNext("I");
			subject.OnNext("have");
			subject.OnNext("a");
			subject.OnNext("pen.");
			subject.OnCompleted();
			subject.Dispose();

			Assert.AreEqual("I have a pen.", result, "文字列が連結されている");
		}

		#endregion


		#region private 関数

		#endregion
	}
}
