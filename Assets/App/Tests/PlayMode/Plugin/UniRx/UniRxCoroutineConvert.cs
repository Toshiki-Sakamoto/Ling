//
// UniRxCoroutineConvert.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.21
//

using NUnit.Framework;

using Assert = UnityEngine.Assertions.Assert;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;
using UniRx;
using System;


namespace Ling.Tests.PlayMode.Plugin.UniRx
{
	/// <summary>
	/// コルーチン変換
	/// </summary>
	public class UniRxCoroutineConvert
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

		[UnityTest]
		public IEnumerator ConvertFromCoroutineTest()
		{
			SceneManager.LoadScene("UniRxAddToTestScene");

			yield return null;

			var convertFromCoroutine = GameObject.FindObjectOfType<ConvertFromCortoutine>();
			Assert.IsNotNull(convertFromCoroutine, "Classが見つからない");

			yield return new WaitUntil(() => convertFromCoroutine.Finished);

			Assert.IsTrue(convertFromCoroutine.TriggerdOnNext, "OnNextが呼び出されている");
			Assert.IsTrue(convertFromCoroutine.TriggerdOnCompleted, "OnCompletedが呼び出されている");
		}

		[UnityTest]
		public IEnumerator ConvertFromCoroutineValueTest()
		{
			SceneManager.LoadScene("UniRxAddToTestScene");

			yield return null;

			var convertFromCoroutineValue = GameObject.FindObjectOfType<ConvertFromCoroutineValue>();
			Assert.IsNotNull(convertFromCoroutineValue, "Classが見つからない");

			yield return new WaitUntil(() => convertFromCoroutineValue.Finished);

			Assert.AreEqual(3, convertFromCoroutineValue.Count, "yield returnの値を正常に受け取れている");
		}

		[UnityTest]
		public IEnumerator ConvertFromCoroutineToObserverTest()
		{
			SceneManager.LoadScene("UniRxAddToTestScene");

			yield return null;

			var convertFromCoroutineToObserver = GameObject.FindObjectOfType<ConvertFromCoroutineToObserver>();
			Assert.IsNotNull(convertFromCoroutineToObserver, "Classが見つからない");

			yield return new WaitUntil(() => convertFromCoroutineToObserver.Finished);

			Assert.AreEqual(3, convertFromCoroutineToObserver.Count, "引数で渡してIObserverのOnNextで値を正常に受け取れている");
		}

		[UnityTest]
		public IEnumerator ConvertToCortouine()
		{
			SceneManager.LoadScene("UniRxAddToTestScene");

			yield return null;

			var convertToCortouine = GameObject.FindObjectOfType<ConvertToCortouine>();
			Assert.IsNotNull(convertToCortouine, "Classが見つからない");

			yield return new WaitUntil(() => convertToCortouine.Finished);

			Assert.IsTrue(convertToCortouine.Finished, "ToYieldInstructionの処理がうまく行った");
		}

		[UnityTest]
		public IEnumerator SelectManyTest()
		{
			bool execudedCoroutineB = false;

			IEnumerator CoroutineA()
			{
				yield return new WaitForSeconds(0.1f);
			}

			IEnumerator CoroutineB()
			{
				yield return new WaitForSeconds(0.1f);
				execudedCoroutineB = true;
			}

			bool isFinished = false; 
			Observable.FromCoroutine(CoroutineA)
				.SelectMany(CoroutineB)	// SelectManyで合成可能
				.Subscribe(_ => isFinished = true);


			yield return new WaitUntil(() => isFinished);

			Assert.IsTrue(execudedCoroutineB, "SelectManyで処理を合成した");
		}

		[UnityTest]
		public IEnumerator WhenAllTest()
		{
			int count = 0;
			bool isFinished = false;
			IEnumerator CoroutineA(IObserver<int> observer)
			{
				yield return new WaitForSeconds(0.1f);
				observer.OnNext(1);
				observer.OnCompleted();
			}

			IEnumerator CoroutineB(IObserver<int> observer)
			{
				yield return new WaitForSeconds(0.1f);
				observer.OnNext(1);
				observer.OnCompleted();
			}

			// コルーチンAとBを同時に起動して終了を待つ
			Observable.WhenAll(
				Observable.FromCoroutine<int>(o => CoroutineA(o)),
				Observable.FromCoroutine<int>(o => CoroutineB(o))
			).Subscribe(xi => 
			{
				foreach (var x in xi)
				{
					count += x;
				}
			}, 
			() => isFinished = true);


			yield return new WaitUntil(() => isFinished);

			Assert.AreEqual(2, count, "WhenAllで２つのコルーチンの終了処理をした。内部でOnNextが呼び出されたので2");
		}

		#endregion


		#region private 関数

		#endregion
	}
}
