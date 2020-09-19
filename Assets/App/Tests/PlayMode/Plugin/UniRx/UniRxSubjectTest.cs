//
// UniRxSubjectTest.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.19
//

using UnityEngine;
using System;
using System.Collections;
using Assert = UnityEngine.Assertions.Assert;
using NUnit.Framework;
using UniRx;
using UnityEngine.TestTools;

namespace Ling.Tests.PlayMode.Plugin.UniRx
{
	/// <summary>
	/// UniRxのSubjectテスト
	/// </summary>
	public class UniRxSubjectTest
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

		/// <summary>
		/// SubjectのOnNextがよびだされているかのテスト
		/// </summary>
		[UnityTest]
		public IEnumerator SubjectOnNextTest()
		{
			var subject = new Subject<int>();

			IEnumerator CountCoroutine()
			{
				// 5 からカウントダウン
				var count = 5;
				while (count > 0)
				{
					// イベントを発行
					subject.OnNext(count--);
					
					// 1フレーム待機
					yield return null;
				}
			}

			// イベントの購読側だけを公開
			int totalCount = 0;

			var observer = (IObservable<int>)(subject);
			observer.Subscribe(count => 
				{
					totalCount += count;	
				});

			yield return CountCoroutine();

			Assert.AreEqual(15, totalCount, "1~5までをカウントしていけば答えは15になるはず");
		}

		[Test]
		public void SubjectMultiTest()
		{
			var subject = new Subject<int>();

			int count = 0;

			// 複数登録
			subject.Subscribe(num => count += num);
			subject.Subscribe(num => count += num);
			subject.Subscribe(num => count += num);
			
			// イベントメッセージ発行
			subject.OnNext(1);
			Assert.AreEqual(3, count, "３回購読されたはずなので値は3");

			subject.OnNext(2);			
			Assert.AreEqual(9, count, "追加で2を３回購読されたはずなので値は9");
		}

		[Test]
		public void SubjectSubscribeTest()
		{
			int count = 0;

			{
				// OnNextのみ
				var subject = new Subject<int>();
				subject.Subscribe(num => count = 1);
				subject.OnNext(1);

				Assert.AreEqual(1, count, "OnNextに設定したものが呼び出されて1になっている");
			}

			{
				// OnNext&OnError
				count = 0;

				var subject = new Subject<int>();
				subject.Subscribe(
					num => count += num,
					error => count += 1);

				subject.OnNext(1);
				subject.OnError(new ArgumentNullException("error"));

				Assert.AreEqual(2, count, "OnNext, OnErrorを呼び出したので2になっている");
			}

			{
				// OnNext&OnCompleted
				count = 0;

				var subject = new Subject<int>();
				subject.Subscribe(
					num => count += num,
					() => count += 1);

				subject.OnNext(1);
				subject.OnCompleted();

				Assert.AreEqual(2, count, "OnNext, OnCompletedを呼び出したので2になっている");
			}

			{
				// OnNext & OnError & OnCompleted
				count = 0;

				var subject = new Subject<int>();
				subject.Subscribe(
					num => count += num,
					error => count += 1,
					() => count += 1);

				subject.OnNext(1);
				subject.OnError(new ArgumentNullException("error"));

				// OnErrorの後なのでCnCompleteはもう呼び出されない
				subject.OnCompleted();

				Assert.AreEqual(2, count, "OnNext, OnError, OnCompletedを呼び出したがOnCompleteはOnErrorで止められたので2");
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
