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

		[UnityTest]
		public IEnumerator OperatorTest()
		{
			var subject = new Subject<int>();

			// そのまま
			subject.Subscribe(x => Debug.Log("raw:" + x));

			subject
				.Where(x => x > 0);
				.Subscribe(x => Debug.Log("filter:" + x));

			subject.OnNext(1);
			subject.OnNext(-1);
			subject.OnNext(3);
			subject.OnNext(0);

			subject.OnCompleted();
			subject.Dispose();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
