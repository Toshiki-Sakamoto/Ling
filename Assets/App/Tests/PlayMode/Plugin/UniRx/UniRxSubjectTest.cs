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

		[Test]
		public void SubjectWhereTest()
		{
			var subject = new Subject<int>();

			int count = 0;
			subject
				.Where(num => num == 1)
				.Subscribe(num => count += num);

			subject.OnNext(1);
			subject.OnNext(2);
			subject.OnNext(3);
			subject.OnNext(4);

			Assert.AreEqual(1, count, "Whereで１以外はフィルタリングされたので1");
		}

		[Test]
		public void SubjectMyWhereTest()
		{
			var subject = new Subject<int>();

			int count = 0;

			// 自分で作ったFilterを挟んでSubscribeしてみる
			subject
				.FilterOperator(num => num == 1)
				.Subscribe(num => count += num);

			subject.OnNext(1);
			subject.OnNext(2);
			subject.OnNext(3);

			Assert.AreEqual(1, count, "自作したフィルタで1以外は弾く");
		}

		#endregion


		#region private 関数

		#endregion
	}

	
	/// <summary>
	/// フィルタリングオペレータ
	/// </summary>
	public class MyFilter<T> : IObservable<T>
	{
		/// <summary>
		/// 上流となるObservable
		/// </summary>
		private IObservable<T> _source;

		/// <summary>
		/// 判定式
		/// </summary>
		private Func<T, bool> _conditionalFunc;


		public MyFilter(IObservable<T> source, Func<T, bool> conditionalFunc)
		{
			_source = source;
			_conditionalFunc = conditionalFunc;
		}

		public IDisposable Subscribe(IObserver<T> observer)
		{
			// Subscribeされたら、MyFilterOperator本体を作って返却
			return new MyFilterInternal(this, observer).Run();
		}

		/// <summary>
		/// ObserverとしてMyFilterInternalが実際に機能する
		/// </summary>
		private class MyFilterInternal : IObserver<T>
		{
			private MyFilter<T> _parent;
			private IObserver<T> _observer;
			private object lockObject = new object();


			public MyFilterInternal(MyFilter<T> parent, IObserver<T> observer)
			{
				_observer = observer;
				_parent = parent;
			}

			public IDisposable Run()
			{
				return _parent._source.Subscribe(this);
			}

			public void OnNext(T value)
			{
				lock (lockObject)
				{
					if (_observer == null) return;

					try
					{
						// 条件を満たす場合のみOnNextを通過
						if (_parent._conditionalFunc(value))
						{
							_observer.OnNext(value);
						}
					}
					catch (Exception e)
					{
						// 途中でエラーが発生したらエラーを送信
						_observer.OnError(e);
						_observer = null;
					}
				}
			}

			public void OnError(Exception error)
			{
				lock (lockObject)
				{
					// エラーを伝播して停止
					_observer.OnError(error);
					_observer = null;
				}
			}

			public void OnCompleted()
			{
				lock (lockObject)
				{
					// 停止
					_observer.OnCompleted();
					_observer = null;
				}
			}
		}
	}

	/// <summary>
	/// オペレータ使うたびにインスタンス化が必要になり使い勝手が悪いので
	/// オペレータチェーンでこのFilterを挟み込めるようにする
	/// </summary>
	public static class ObservableOperators
	{
		public static IObservable<T> FilterOperator<T>(this IObservable<T> source, Func<T, bool> conditionalFunc)
		{
			return new MyFilter<T>(source, conditionalFunc);	
		}
	}
}
