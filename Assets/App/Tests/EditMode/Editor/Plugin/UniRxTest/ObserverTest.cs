//
// ObserverTest.cs
// ProductName Ling
//
// Created by  on 2021.07.23
//

using NUnit.Framework;
using System;
using System.Collections.Generic;
using UniRx;

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

		/// <summary>
		/// SubjectはIObserver(受信）とIObservable（送信）の両方を受け持つ
		/// </summary>
		public sealed class MySubject<T> : ISubject<T>, IDisposable
		{
			/// <summary>
			/// 購読状態を管理する
			/// Disposeを呼ぶとSubjetからObserverを削除する
			/// </summary>
			private sealed class Subscription : IDisposable
			{
				private readonly IObserver<T> _observer;
				private MySubject<T> _parent;
				private readonly object _gate = new object();

				public Subscription(IObserver<T> observer, MySubject<T> parent)
				{
					_observer = observer;
					_parent = parent;
				}

				public void Dispose()
				{
					lock (_gate)
					{
						if (_parent == null) return;
						lock (_parent._gate)
						{
							_parent._observers?.Remove(_observer);
							_parent = null;
						}
					}
				}
			}

			private readonly object _gate = new object();   // 排他処理用
			private List<IObserver<T>> _observers;          // 登録されたObserver一覧
			private bool _isDisposed;   // 破棄されたか
			private bool _isStopped;    // 停止状態か
			private Exception _error;

			public void OnNext(T value)
			{
				lock (_gate)
				{
					ThrowIfDisposed();

					if (_isStopped) return;
					if (_observers != null)
					{
						foreach (var observer in _observers)
						{
							// 各種Observerにイベントを伝える
							observer.OnNext(value);
						}
					}
				}
			}

			public void OnError(Exception error)
			{
				lock (_gate)
				{
					ThrowIfDisposed();
					if (_isStopped) return;

					_error = error;

					if (_observers != null)
					{
						foreach (var observer in _observers)
						{
							observer.OnError(error);
						}

						_observers.Clear();
						_observers = null;
					}

					// 停止
					_isDisposed = true;
				}
			}

			public void OnCompleted()
			{
				lock (_gate)
				{
					ThrowIfDisposed();

					if (_isStopped) return;
					if (_observers != null)
					{
						foreach (var observer in _observers)
						{
							observer.OnCompleted();
						}

						_observers.Clear(); // Completeしたら中身を消す
						_observers = null;
					}

					_isStopped = true;
				}
			}


			// 登録する部分
			public IDisposable Subscribe(IObserver<T> observer)
			{
				if (observer == null) throw new ArgumentException();

				lock (_gate)
				{
					ThrowIfDisposed();

					if (!_isStopped)
					{
						// Observerのリストに登録
						if (_observers == null) _observers = new List<IObserver<T>>();
						_observers.Add(observer);

						return new Subscription(observer, this);
					}
					else
					{
						// 終了してるのを伝える
						if (_error != null)
						{
							observer.OnError(_error);
						}
						else
						{
							observer.OnCompleted();
						}

						return Disposable.Empty;
					}
				}
			}

			private void ThrowIfDisposed()
			{
				lock (_gate)
				{
					if (_isDisposed) throw new ObjectDisposedException(nameof(GetType));
				}
			}

			public void Dispose()
			{
				lock (_gate)
				{
					if (_isDisposed) return;

					_isDisposed = true;
					_observers = null;
				}
			}
		}

		public class SampleObserver : IObserver<int>
		{
			public int Value { get; private set; }

			public void OnNext(int value)
			{
				Value = value;
			}

			public void OnError(Exception e) { }

			public void OnCompleted() { }
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

		[Test]
		public void IObservableテストコード()
		{
			// IObservableはIObserverを登録できる。という振る舞いをするインターフェース
			// メッセージ送信側で使う

			// SubjectはObserverを登録/かんりしイベントの発行を実行できる
		}

		[Test]
		public void 自作Subjectテスト()
        {
			var mySubject = new MySubject<int>();
			var sampleObserver = new SampleObserver();

			var disposable = mySubject.Subscribe(sampleObserver);

			mySubject.OnNext(1);

			Assert.AreEqual(1, sampleObserver.Value, "受け取れてること");

			// 破棄
			disposable.Dispose();

			mySubject.OnNext(2);

			Assert.AreEqual(1, sampleObserver.Value, "破棄しているので更新されない");

			mySubject.OnCompleted();
			mySubject.Dispose();
        }

		[Test]
		public void SubjectにIObserverの代わりにラムダを登録する()
		{
			var value = default(string);

			var subject = new Subject<string>();
			subject.Subscribe(
				onNext: x => value = x,
				onError: error => value = "Error",
				onCompleted: () => value = "Complete");

			subject.OnNext("Next");
			Assert.AreEqual("Next", value);

			subject.OnCompleted();
			Assert.AreEqual("Complete", value);
		}

		[Test]
		public void ReactivePropertyテスト()
		{
			int value = 0;

			// Subjectの機能が同梱された変数として振る舞う
			var rp = new ReactiveProperty<int>(0);
			rp.Subscribe(x => value = x);

			rp.Value = 1;

			Assert.AreEqual(1, value);
		}

		[Test]
		public void Operatorテスト()
		{
			// SubjectとObserverの中間に挟み込むことが出来るオブジェクト
			// SubjectからObserverに伝達されるメッセージの加工や発行タイミングの調整が可能


		}


		#endregion


		#region private 関数

		#endregion
	}
}
