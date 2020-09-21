//
// UniRxMessageTest.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.20
//

using NUnit.Framework;
using UniRx;
using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

using Assert = UnityEngine.Assertions.Assert;


namespace Ling.Tests.PlayMode.Plugin.UniRx
{
	/// <summary>
	/// UniRxのメッセージ関するテスト
	/// </summary>
	public class UniRxMessageTest
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
		/// 意味のない通知を送るテスト
		/// </summary>
		[Test]
		public void MessageUnitDefaultTest()
		{
			int count = 0;

			var subject = new Subject<Unit>();
			subject.Subscribe(_ =>  count++);

			// Unit型はそれ自身は特に意味はない。通知を贈りたいときに利用
			// イベントは飛ばしたいけどイベントの中身の値は何でも良いシチュエーションで使われる
			subject.OnNext(Unit.Default);

			Assert.AreEqual(1, count, "通知が届いているので1");
		}

		/// <summary>
		/// ストリームの途中で例外が発生したときにOnErrorが呼ばれるかのテスト
		/// </summary>
		[Test]
		public void MessageOnErrorTest()
		{
			int count = 0;

			var subject = new Subject<string>();
			subject
				.Select(str => int.Parse(str))
				.Subscribe(
					num => count += num,	// OnNext
					ex => count = 0);		// OnError

			subject.OnNext("1");
			subject.OnNext("1");
			Assert.AreEqual(2, count, "OnNextがよばれて2になっている");

			subject.OnNext("Hello");	
			Assert.AreEqual(0, count, "Parseエラーが発生し、countの値が0に戻っているはず");

			subject.OnNext("1");
			Assert.AreEqual(0, count, "ストリームの途中で例外が発生して終了したのでOnNextが呼ばれなくなっている");
		}

		/// <summary>
		/// 途中で例外が発生したら再購読する
		/// </summary>
		[Test]
		public void MessageOnErrorRetryTest()
		{
			int count = 0;

			var subject = new Subject<string>();
			subject
				.Select(str => int.Parse(str))
				.OnErrorRetry((FormatException ex) =>	// 例外の型指定でフィルタリング可能
				{
					// 例外が発生したため再購読
					count += 1;
				})
				.Subscribe(
					num => count += num,	// OnNext
					ex => count += 1	// OnError
				);

			subject.OnNext("1");
			subject.OnNext("1");
			subject.OnNext("Hello");
			subject.OnNext("1");

			Assert.AreEqual(4, count, "OnNextとOnError, OnErrorRetryで5になっている");
		}

		/// <summary>
		/// OnCompletedを検知する
		/// </summary>
		[Test]
		public void MessageOnCompletedTest()
		{
			int count = 0;

			var subject = new Subject<int>();
			subject.Subscribe(
				num => count += num,
				() => count += 1
			);

			subject.OnNext(1);
			subject.OnCompleted();

			Assert.AreEqual(2, count, "OnNextとOnCompletedで2になっている");

			count = 0;
			subject.Subscribe(
				num => count += num, 
				() => count += 1
				);

			Assert.AreEqual(1, count, "OnCompletedが呼び出され済みなのですぐにOnCompletedが呼び出される");
		}

		[Test]
		public void MessageDisaposeTest()
		{
			int count = 0;
			var subject = new Subject<int>();
			var disposable = subject.Subscribe(num => count += num, () => count += 1);
			subject.OnNext(1);

			// 購読終了
			disposable.Dispose();

			subject.OnNext(1);
			subject.OnCompleted();

			Assert.AreEqual(1, count, "Disposeを呼び出したので途中で購読が中止されている");
		}

		[Test]
		public void MessageSpecificDisaposeTest()
		{
			int count = 0;

			var subject = new Subject<int>();
			var disposable1 = subject.Subscribe(num => count += num);
			var disposable2 = subject.Subscribe(num => count += num);

			// 特定のストリームだけ購読終了
			disposable1.Dispose();

			subject.OnNext(1);

			Assert.AreEqual(1, count, "一つストリームの購読を中止しているので1");
		}

		[UnityTest]
		public IEnumerator MessageAddToTest()
		{
			SceneManager.LoadScene("UniRxAddToTestScene");

			yield return null;

			var addToTestObject = GameObject.FindObjectOfType<MessageAddTo_Object>();
			Assert.IsNotNull(addToTestObject, "Classが見つからない");

			// 6フレーム待機して例外でなきゃOK
			int count = 6;
			while (count-- >= 0) 
			{
				yield return null;
			}
		}



		#endregion


		#region private 関数

		#endregion
	}
}
