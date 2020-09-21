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

		#endregion


		#region private 関数

		#endregion
	}
}
