//
// UniRxUpdateTest.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.20
//

using System.Collections;
using Assert = UnityEngine.Assertions.Assert;
using NUnit.Framework;
using UniRx;
using UnityEngine.TestTools;
using System.Net;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Ling.Tests.PlayMode.Plugin.UniRx
{
	/// <summary>
	/// Updateをストリームに変換するテスト
	/// </summary>
	public class UniRxUpdateTest
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
		public IEnumerator UpdateAsObservableTest()
		{
			SceneManager.LoadScene("UniRxAddToTestScene");

			yield return null;

			var instance = GameObject.FindObjectOfType<UpdateSample>();
			Assert.IsNotNull(instance, "Classが見つからない");

			yield return new WaitUntil(() => instance.Updated);

			Assert.IsTrue(instance.Updated, "UpdateAsObservableが呼び出されてtrue");
		}

		#endregion


		#region private 関数

		#endregion
	}
}
