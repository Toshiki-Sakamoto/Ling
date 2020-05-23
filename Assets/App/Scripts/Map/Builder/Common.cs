//
// Common.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.23
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Map.Builder
{
	/// <summary>
	/// 
	/// </summary>
	public static class Common
	{
		#region 定数, class, enum

		private static readonly int[,] Dir = new int[4, 2] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
		private static readonly int[,] DirWithDiagonal = new int[8, 2] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 }, { -1, -1 }, { 1, -1 }, { -1, 1 }, { 1, 1 } };

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

		/// <summary>
		/// 各方角（最大８方向）指定デリゲートを呼び出す
		/// </summary>
		public static void CallDirection(int x, int y, System.Action<int, int> action, bool useDiagonal = false)
		{
			var dirArray = GetDirArray(useDiagonal);
			for (int i = 0, size = dirArray.GetLength(0); i < size; ++i)
			{
				action.Invoke(x + dirArray[i, 0], y + dirArray[i, 1]);
			}
		}

		public static bool CallDirection(int x, int y, System.Func<int, int, bool> func, bool useDiagonal = false)
		{
			var dirArray = GetDirArray(useDiagonal);
			for (int i = 0, size = dirArray.GetLength(0); i < size; ++i)
			{
				if (func.Invoke(x + dirArray[i, 0], y + dirArray[i, 1]))
				{
					return true;
				}
			}

			return false;
		}


		#endregion


		#region private 関数

		private static int[,] GetDirArray(bool useDiagonal)
		{
			if (useDiagonal) return DirWithDiagonal;
			return Dir;
		}

		#endregion
	}
}
