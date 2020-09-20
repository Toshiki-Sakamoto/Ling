//
// UniRxStreamTest.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.20
//

using System;
using System.Collections;
using Assert = UnityEngine.Assertions.Assert;
using NUnit.Framework;
using UniRx;
using UnityEngine.TestTools;

namespace Ling.Tests.PlayMode.Plugin.UniRx
{
	/// <summary>
	/// SteremSourceテスト
	/// </summary>
	public class UniRxStreamTest
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

		[Test]
		public void SubjectsTest()
		{
			{
				int count = 0;

				// 最後に発行された値をキャッシュし、後からSubscribeされたときにその値を発行
				var behaviourSubject = new BehaviorSubject<int>(1);
				behaviourSubject.Subscribe(num => count += num);

				Assert.AreEqual(1, count, "初期値が発行されて1");
			}

			{
				int count = 0;

				// 最後に発行された値をキャッシュし、後からSubscribeされたときにその値を発行
				var behaviourSubject = new BehaviorSubject<int>(1);
				behaviourSubject.OnNext(2);
				behaviourSubject.Subscribe(num => count += num);

				Assert.AreEqual(2, count, "最後に発行された値がキャッシュされ、後のSubscribeで呼び出される");
			}

			{
				int count = 0;

				// 過去全ての発行された値をキャッシュし、あとからSubscribeされたときにその値をすべてまとめて発行する
				var replaySubject = new ReplaySubject<int>();
				replaySubject.OnNext(1);
				replaySubject.OnNext(1);
				replaySubject.Subscribe(num => count += num);

				Assert.AreEqual(2, count, "過去に発行された値がすべて呼び出されて2");
			}

			{
				int count = 0;

				// OnNextを直ちに発行せずに内部でキャッシュしてOnCompletedが実行されたタイミングで、最後のOnNextを一つだけ発行する
				var asyncSubject = new AsyncSubject<int>();
				asyncSubject.OnNext(1);
				asyncSubject.OnNext(2);
				asyncSubject.OnNext(3);

				asyncSubject.Subscribe(num => count += num);
				Assert.AreEqual(0, count, "まだOnNextは呼びだれていないので0");

				asyncSubject.OnCompleted();
				Assert.AreEqual(3, count, "OnCompletedが呼び出され、最後のOnNextが一つだけ発行されて3");
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
