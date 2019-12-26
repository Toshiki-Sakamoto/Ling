//
// ListExtensionsTest.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.26
//

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Ling.Utility.Extensions;

using Assert = UnityEngine.Assertions.Assert;


namespace Ling.Tests.Utility
{
	/// <summary>
	/// Listの拡張メソッドのテスト
	/// </summary>
    public class ListExtensionsTest
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private List<int> _listOrdered;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		[SetUp]
		public void Setup()
		{
			_listOrdered = new List<int> { 0, 1, 2, 3, 4 };
		}

		/// <summary>
		/// 参照を引数とするForEachが正常に動作するか
		/// </summary>
		[Test]
		public void ListExtensions_RefForEach()
		{
			// 参照で配列内の構造体が書き換えられているか
			_listOrdered.ForEach((ref int _num) => { _num = 10; });
			_listOrdered.ForEach(_num => Assert.IsTrue(_num == 10, "参照により書き変わっている"));
		}

		/// <summary>
		/// ラムダを使用したForEach
		/// </summary>
		[Test]
		public void ListExtensions_DelegateForEach()
		{
			_listOrdered.ForEach(_num => 10);
			_listOrdered.ForEach(_num => Assert.IsTrue(_num == 10, "戻り値により書き変わっている"));
		}

		#endregion


		#region private 関数

		#endregion
	}
}
