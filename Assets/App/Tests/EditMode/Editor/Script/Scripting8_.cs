//
// Scripting8_.cs
// ProductName Ling
//
// Created by  on 2021.07.23
//

using NUnit.Framework;
using System.Collections.Generic;
using Assert = UnityEngine.Assertions.Assert;


namespace Ling.Tests.Script
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

		[Test]
		public void Null許容参照型()
		{
			//string str = null;  // 通常はOK

#nullable enable

			//string str = null;	// null非許容参照型なのでnull入れると警告が出る
			//string? str = null;		// これはnull許容参照型なので問題ない

#nullable disable
		}

		[Test]
		public void 静的ローカル関数()
		{
			int v = 0;

			static int Add(int x1, int x2)
			{
				// v += x1; 静的ローカル関数ではキャプチャができない
				return x1 + x2;
			}

			Add(v, 1);
		}

#endregion


#region private 関数

#endregion
	}
}
