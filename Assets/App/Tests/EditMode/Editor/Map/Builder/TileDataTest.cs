//
// TileDataTest.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.30
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using NUnit.Framework;

using Assert = UnityEngine.Assertions.Assert;

using Ling.Map.Builder;


namespace Ling.Tests.Map.Builder
{
	/// <summary>
	/// TileData のテスト
	/// </summary>
	public class TileDataTest
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private TileData _tileData;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		[SetUp]
		public void Setup()
		{
			_tileData = new TileData();
			_tileData.Initialize();
		}

		[Test]
		public void TileData_AddFlag()
		{
			_tileData.Initialize();

			// 何もしてないときは 0(None) のはず
			Assert.AreEqual(_tileData.Flag, Const.TileFlag.None, "何もしてないときはNone");

			_tileData.AddFlag(Const.TileFlag.Wall);

			// AddFlagが正常に動いているか		
			Assert.IsTrue((_tileData.Flag | Const.TileFlag.Wall) != 0, "AddFlagでWallフラグが追加されているか");
		}

		[Test]
		public void TileData_RemoveFlag()
		{
			_tileData.Initialize();

			// 何もしてないときは 0(None)のはず
			Assert.AreEqual(_tileData.Flag, Const.TileFlag.None, "何もしてないときはNone");

			_tileData.AddFlag(Const.TileFlag.Wall);
			_tileData.RemoveFlag(Const.TileFlag.Wall);

			// Removeしたので 0のはず
			Assert.AreEqual(_tileData.Flag, Const.TileFlag.None, "RemoveしたのでNone");

			_tileData.AddFlag(Const.TileFlag.Wall);
			_tileData.AddFlag(Const.TileFlag.StepUp);

			_tileData.RemoveFlag(Const.TileFlag.Wall);

			Assert.IsTrue(_tileData.HasFlag(Const.TileFlag.StepUp), "WallをRemoveしたがStepUpはある");
			Assert.IsFalse(_tileData.HasFlag(Const.TileFlag.Wall), "WallをRemoveしたので無い");
		}

		/// <summary>
		/// HasFlagのチェック
		/// </summary>
		[Test]
		public void TileData_HasFlag()
		{
			_tileData.Initialize();

			// HasFlagのチェック
			_tileData.AddFlag(Const.TileFlag.Wall);

			Assert.IsTrue(_tileData.HasFlag(Const.TileFlag.Wall), "Wall指定でtrueになる");
			Assert.IsFalse(_tileData.HasFlag(Const.TileFlag.StepUp), "StepUpはAddしてないのでFalse");
			Assert.IsFalse(_tileData.HasFlag(Const.TileFlag.StepDown), "StepDownはAddしてないのでFalse");

			// StepUpをAddしたらWallもStepUpもTrueになる
			_tileData.AddFlag(Const.TileFlag.StepUp);

			Assert.IsTrue(_tileData.HasFlag(Const.TileFlag.Wall), "Wallはtrueのまま");
			Assert.IsTrue(_tileData.HasFlag(Const.TileFlag.StepUp), "StepUpがAddされたのでTrue");
			Assert.IsFalse(_tileData.HasFlag(Const.TileFlag.StepDown), "StepDownはAddされてないのでFlase");

			// Initializeしたらフラグも初期化される
			_tileData.Initialize();

			Assert.AreEqual(_tileData.Flag, Const.TileFlag.None, "InitializeしたらNoneになる");
			Assert.IsFalse(_tileData.HasFlag(Const.TileFlag.Wall), "InitializeしたらWallもFalseになる");
		}

		#endregion


		#region private 関数

		#endregion
	}
}
