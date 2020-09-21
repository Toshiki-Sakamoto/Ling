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

		#endregion


		#region private 関数

		#endregion
	}
}
