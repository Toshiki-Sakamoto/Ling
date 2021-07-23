//
// Scripting8_.cs
// ProductName Ling
//
// Created by  on 2021.07.23
//

using NUnit.Framework;
using System.Collections.Generic;
using Assert = UnityEngine.Assertions.Assert;


namespace Ling.Tests.EditMode.Script
{
	/// <summary>
	/// C#8
	/// </summary>
	public class Scripting8_
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

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Null合体割当演算子()
		{
			// 左オペランドがNullだった場合のみ右の値を左に代入する
			var str = default(string);

			str ??= "Test";

			Assert.AreEqual("Test", str, "文字列が一致する");
		}

		#endregion


		#region private 関数

		#endregion
	}
}
