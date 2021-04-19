using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Utility.Extensions;

using Assert = UnityEngine.Assertions.Assert;

namespace Ling.Tests.Utility
{
	/// <summary>
	/// Arrayの拡張メソッドのテスト
	/// </summary>
	public class ArrayExtensionsTest
	{
		int[] _arrayOrdered;


		[SetUp]
		public void Setup()
		{
			_arrayOrdered = new int[] { 0, 1, 2, 3, 4 };
		}


		/// <summary>
		/// ForEachが正常に動作するか
		/// </summary>
		[Test]
		public void ArrayExtensions_ForEach()
		{
			int count = 0;

			_arrayOrdered.ForEach(_num => { ++count; });
			Assert.IsTrue(count == _arrayOrdered.Length, "配列の数だけForEachが呼び出されている");

			count = 0;
			_arrayOrdered.ForEach(_num => Assert.AreEqual(_num, count++, "要素が正しく呼び出されている"));
		}

		/// <summary>
		/// 参照を引数とするForEachが正常に動作するか
		/// </summary>
		[Test]
		public void ArrayExtensions_RefForEach()
		{
			// 参照で配列内の構造体が書き換えられているか
			_arrayOrdered.ForEach((ref int _num) => { _num = 10; });
			_arrayOrdered.ForEach(_num => Assert.IsTrue(_num == 10, "参照により書き変わっている"));
		}

		/// <summary>
		/// ラムダを使用したForEach
		/// </summary>
		[Test]
		public void ArrayExtensions_DelegateForEach()
		{
			_arrayOrdered.ForEach(_num => 10);
			_arrayOrdered.ForEach(_num => Assert.IsTrue(_num == 10, "戻り値により書き変わっている"));
		}


		/// <summary>
		/// 各テスト終了後に呼び出される
		/// </summary>
		[TearDown]
		public void TearDown()
		{
			_arrayOrdered = new int[] { 0, 1, 2, 3, 4 };
		}
	}
}
