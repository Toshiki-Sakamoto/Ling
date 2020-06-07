//
// SimpleTest.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.10
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;
using UnityEngine.TestTools;

using Cysharp.Text;
using UnityEngine.Networking;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Ling.Tests.PlayMode.Plugin.UniRxTest
{
	/// <summary>
	/// UniTaskをいじってみる
	/// </summary>
	public class UniTaskTest
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Subject<int> _timerSubject = new Subject<int>();

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
		/// UniTaskのDelayが無視されるか
		/// </summary>
		/// <returns></returns>
		[UnityTest]
		public IEnumerator DelayIgnore() => UniTask.ToCoroutine(async () =>
		{
			var time = Time.realtimeSinceStartup;

			Time.timeScale = 0.01f;

			try
			{
				await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: true);

				var elapsed = Time.realtimeSinceStartup - time;

				// MidpointRounding は数値の丸め処理を行うメソッドで、処理する方法を決める
				// ToEvanは数値が２つの数値の中間に位置するときに最も近い偶数方向に丸められる
				Assert.AreEqual(1, (int)Math.Round(TimeSpan.FromSeconds(elapsed).TotalSeconds, MidpointRounding.ToEven));
			}
			finally
			{
				Time.timeScale = 1.0f;
			}
		});

		/// <summary>
		/// UnityWebRequestをUniTask使用して待機
		/// </summary>
		/// <returns></returns>
		[UnityTest]
		public IEnumerator Request() => UniTask.ToCoroutine(async () =>
		{
			var request = UnityWebRequest.Get("http://google.co.jp/");

			// 通信が終わるまで
			await request.SendWebRequest();

			if (request.isHttpError || request.isNetworkError)
			{
				// 通信エラー
				return;
			}

			Debug.Log($"【UniTask Request Test Response】{request.downloadHandler.text}");

			Assert.IsTrue(!string.IsNullOrEmpty(request.downloadHandler.text), "通信結果にテキストが入っている");
		});

		/// <summary>
		/// Intervalテスト
		/// </summary>
		/// <returns></returns>
		[UnityTest]
		public IEnumerator Interval() => UniTask.ToCoroutine(async () =>
		{
			Observable.Interval(TimeSpan.FromSeconds(0.2f))
				.DoOnCompleted(() => Debug.Log("Interval完了！")).Subscribe();

			await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
		});

		/// <summary>
		/// Subjectテスト
		/// </summary>
		/// <returns></returns>
		[UnityTest]
		public IEnumerator Subject()
		{
			// 購読者
			// 通知の受取時の実行関数を登録
			_timerSubject.Subscribe(time_ => Debug.Log($"【Subscribe】{time_.ToString()}"));

			var time = 10;
			while (time > 0)
			{
				// Subscribeで登録された関数に通知を行う
				_timerSubject.OnNext(time--);

				yield return new WaitForSeconds(0.01f);
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
