//
// UniRxMessageTest.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.20
//

using NUnit.Framework;
using UniRx;
using System;

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



		#endregion


		#region private 関数

		#endregion
	}
}
