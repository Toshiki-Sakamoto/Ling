//
// ObserverTest.cs
// ProductName Ling
//
// Created by  on 2021.07.23
//

using NUnit.Framework;
using System;

using Assert = UnityEngine.Assertions.Assert;


namespace Ling.Tests.Plugin.UniRxTest
{
	/// <summary>
	/// 
	/// </summary>
	public class ObserverTest
	{
		#region 定数, class, enum

		public sealed class ObserverSample
		{
			public void Trigger(IObserver<int> observer)
			{
				observer.OnNext(1);
				observer.OnCompleted();
			}
		}

		/// <summary>
        /// イベントを受け取る方
        /// </summary>
		public sealed class TestObserver : IObserver<int>
		{
			public bool IsCompleted { get; private set; }
			public int Value { get; private set; }

			void IObserver<int>.OnCompleted()
			{
				IsCompleted = true;
			}

			void IObserver<int>.OnError(Exception error)
			{
			}

			void IObserver<int>.OnNext(int value)
			{
				Value = value;
			}
		}

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
		public void OnNextとOnCompleteが呼ばれるか()
		{
			var sample = new ObserverSample();
			var observer = new TestObserver();

			sample.Trigger(observer);

			Assert.IsTrue(observer.IsCompleted);
			Assert.AreEqual(1, observer.Value);
		}


		#endregion


		#region private 関数

		#endregion
	}
}
