//
// HotColdTest.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.07
//
using System;
using NUnit.Framework;
using UniRx;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;
using UnityEngine.TestTools;
using Cysharp.Threading.Tasks;
using System.Collections;


namespace Ling.Tests.EditMode.Editor.Plugin.UniRxTest
{
	/// <summary>
	/// https://qiita.com/yutorisan/items/844eaeab392abf03ce80
	/// https://qiita.com/yutorisan/items/68bd68bbffe3c4f3ba32
	/// こちらのQiitaの記事を理解するためのテストコード
	/// </summary>
	public class HotColdTest
	{
		#region 定数, class, enum

		public class HotObservableProvider
		{
			private Subject<int> _subject = new Subject<int>();

			public void Fire()
			{
				_subject.OnNext(1);
				_subject.OnNext(2);
				_subject.OnNext(3);
			}
			public void Fire(int num)
			{
				_subject.OnNext(num);
			}

			// HotObervableから取得できるIObservableはSUbジェCTクラスそのものを指す
			public IObservable<int> HotObservable => _subject;
		}

		public static class MyObservable
		{
			public static IObservable<int> MyRange(int start, int end) =>
				new MyRange(start, end);
		}

		public class MyRange : IObservable<int>
		{
			private class Subscription : IDisposable
			{
				public void Dispose() {}
			}

			private int _start, _end;

			public MyRange(int start, int end)
			{
				_start = start;
				_end = end;
			}

			public IDisposable Subscribe(IObserver<int> observer)
			{
				// Subscribeしてくれた人に直接値を送る
				// Subjectを使ってないので直接OnNext。つまり、Subscribeされる度に最初からになる
				for (int i = _start; i <= _end; ++i)
				{
					observer.OnNext(i);
				}

				observer.OnCompleted();

				return new Subscription();
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

		[SetUp]
		public void Setup()
		{
		}

		/// <summary>
		/// Subjectは購読者を複数持つことができるIObservableということのテスト
		/// https://qiita.com/yutorisan/items/844eaeab392abf03ce80
		/// こちらを参考に
		/// </summary>
		[UnityTest]
		public IEnumerator SubjectTest() =>
			UniTask.ToCoroutine(async () =>
			{
				// SubjectがObservableソース
				{
					var provider = new HotObservableProvider();
					provider.HotObservable  // SubjectなのでHot
						.Subscribe(n => Debug.Log($"{n}が通知された"));

					// OnNextのタイミングで値が発行される
					provider.Fire();
				}

				// ファクトリメソッドがObservableソース
				{
					// Subscribeのタイミングで値が発行される
					Observable.Range(1, 10)
						.Subscribe(n => Debug.Log($"Range: {n}"));
				}

				// ColdなObservableソースの例
				{
					var observableTimer = Observable.Interval(TimeSpan.FromSeconds(0.1));

					observableTimer.Subscribe(n => Debug.Log($"First {n}..."));

					// 1秒待機してColdなObservableソースを２回購読
					await UniTask.Delay(500);

					// ColdなObservableなので最初からカウントが始まる
					observableTimer.Subscribe(n => Debug.Log($"Second {n}..."));

					await UniTask.Delay(500);
				}

				// HotなObservableソースの例
				{

					var provider = new HotObservableProvider();

					provider.HotObservable  // SubjectなのでHot
						.Subscribe(n => Debug.Log($"{n}が通知された"));

					// OnNextのタイミングで値が発行される
					provider.Fire();

					provider.HotObservable  // SubjectなのでHot
						.Subscribe(n => Debug.Log($"{n}が通知された"));
				}
			});

		[UnityTest]
		public IEnumerator IConnectableObservableTest() =>
			UniTask.ToCoroutine(async () =>
			{
				// IConnectableObservableのPublush, ConnectからHot変換する
				// OnNextにつきsubjectのDoは一回のみ。Subscribeの通知は２回くる
				{
					// Observable ソースを作る
					var subject = new Subject<int>();

					// Subjectの通知にオペレータを追加する
					// ColdのままだとオペレータがSubscribeの度に作られる
					var sourceColdObservable = subject.Do(n => Debug.Log($"{n}が通知"))
						.Where(n => n > 0)
						.Select(n => TimeSpan.FromSeconds(n));

					// ColdなIObservable「sourceColdObervable」をHot変換する
					var connectableObservable = sourceColdObservable.Publish();

					// Publish内部のSubjectにsourceColdObservableをSubscribeさせる
					connectableObservable.Connect();

					// Publish内部のSubject(つまりHotなIObservable)をSubjectする
					connectableObservable.Subscribe(time => Debug.Log($"購読者1：{time}"));
					connectableObservable.Subscribe(time => Debug.Log($"購読者2：{time}"));

					subject.OnNext(30);
					subject.OnNext(4);
				}

				Debug.Log("--------------");

				// Hot変換しない場合の挙動
				// OnNextにつきSubscribe分、SubjectのDoが呼び出される。
				{
					var subject = new Subject<int>();

					var sourceColdObservable = subject.Do(n => Debug.Log($"{n}が通知"))
						.Where(n => n > 0)
						.Select(n => TimeSpan.FromSeconds(n));

					sourceColdObservable.Subscribe(time => Debug.Log($"購読者1: {time}"));
					sourceColdObservable.Subscribe(time => Debug.Log($"購読者2: {time}"));

					subject.OnNext(10);
					subject.OnNext(50);
				}

				Debug.Log("--------------");

				// ファクトリメソッドから生成されたObservableシーケンスを使い回す
				// Subscribeした時点から継続したObservableシーケンスの流れを購読する
				{
					var observableTimer = Observable.Interval(TimeSpan.FromSeconds(0.1));

					// Hot変換の準備
					var connectableObservable = observableTimer.Publish();

					// Publish内でObservableTimerをSubscribe
					// → Observableシーケンスが生成されて、タイマーが開始
					var disposable = connectableObservable.Connect();

					connectableObservable.Subscribe(t => Debug.Log($"購読者1: {t}"));

					// ○秒後にもう一度Publush内のSubjectをSubscribe（Hot変換されている
					await UniTask.Delay(500);

					connectableObservable.Subscribe(t => Debug.Log($"購読者2: {t}"));

					await UniTask.Delay(500);

					disposable.Dispose();
				}

				Debug.Log("--------------");

				// ファクトリメソッドから生成されたObservableシーケンスそのまま
				// Subscribeされる度に作り直されるので1からカウント
				{
					var observableTimer = Observable.Interval(TimeSpan.FromSeconds(0.1));

					var disposable1 = observableTimer.Subscribe(t => Debug.Log($"購読者1: {t}"));

					// ○秒後にもう一度Publush内のSubjectをSubscribe（Hot変換されている
					await UniTask.Delay(500);

					var disposable2 = observableTimer.Subscribe(t => Debug.Log($"購読者2: {t}"));

					await UniTask.Delay(500);

					disposable1.Dispose();
					disposable2.Dispose();
				}

				Debug.Log("--------------");

				// ConnectとSubscribeのDisposeの違いを理解する
				// ConnectのIDsposableは 元のIObservableをSubscribeして出てきたもの。ソースとなるColdなIObservableへの購読が破棄される
				// SubscribeのIDisposableは、そのままSubject自体のSubscribeが終了する
				//
				// ConnectをDisposeした後に再びConnect（再接続）する場合、ソースとなるColdなIObservableのObservableソースがColdかHotなのかで挙動が変わってくる
				// Hotであればつないでれば通知が来るし、つないでなければ通知来ないだけ
				// Coldであればつなぐ度に新しいObservableソースが生成されることになる
				{
					var observableInterval = Observable.Interval(TimeSpan.FromSeconds(0.1));
					var connectableObservable = observableInterval.Publish();

					connectableObservable.Subscribe(n => Debug.Log($"購読者1:{n}"));
					connectableObservable.Subscribe(n => Debug.Log($"購読者2:{n}"));

					// Observable.Intervalに接続する
					var connectionDisposable = connectableObservable.Connect();

					// 切断して再接続
					await UniTask.Delay(500);

					Debug.Log("再接続");

					connectionDisposable.Dispose();

					// 再接続しても新しいObservableシーケンスとなるため０からカウント
					connectionDisposable = connectableObservable.Connect();

					await UniTask.Delay(500);

					connectionDisposable.Dispose();
				}

				Debug.Log("--------------");

				{
					// Hot化
					var connectableInterval = Observable.Interval(TimeSpan.FromSeconds(0.1))
						.Publish();

					// この時点でSubscribe用のPublishをしても結果は変わらない（Coldのままなので）

					// Observable.Interval自体をHot化（ObservableソースがHot化する）
					connectableInterval.Connect();

					// 再度Hot化のObservableソースを作成する
					var connectableObservable = connectableInterval.Publish();

					connectableObservable.Subscribe(n => Debug.Log($"購読者1:{n}"));
					connectableObservable.Subscribe(n => Debug.Log($"購読者2:{n}"));

					// Observable.IntervalをHot化したものに接続する
					var connectionDisposable = connectableObservable.Connect();

					// 切断して再接続
					await UniTask.Delay(500);

					Debug.Log("再接続");

					connectionDisposable.Dispose();

					// 再接続しても新しいObservableシーケンスとなるため０からカウント
					connectionDisposable = connectableObservable.Connect();

					await UniTask.Delay(500);

					connectionDisposable.Dispose();
				}
			});

		#endregion


		#region private 関数

		#endregion
	}
}
