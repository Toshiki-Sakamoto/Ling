//
// ListExtensions.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.24
//

using ModestTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling
{
	/// <summary>
	/// Listの拡張メソッド
	/// </summary>
    public static class ListExtensions
    {
		public delegate void RefAction<T>(ref T item);


		/// <summary>
		/// Nullか空の場合true
		/// </summary>
		public static bool IsNullOrEmpty<T>(this IList<T> list) =>
			list == null || list.Count <= 0;

		/// <summary>
		/// refを使用した構造体への作用の反映
		/// </summary>
		public static void ForEach<T>(this IList<T> list, RefAction<T> action)
		{ 
			if (action == null) { throw new ArgumentNullException(nameof(action)); }

			for (int i = 0; i < list.Count; ++i)
			{
				var item = list[i];
				action?.Invoke(ref item);

				list[i] = item;
			}
		}

		/// <summary>
		/// ラムダ式の戻り値を使用して作用を反映させる
		/// </summary>
		public static void ForEach<T>(this IList<T> list, Func<T, T> func)
		{
			if (func == null) { throw new ArgumentNullException(nameof(func)); }

			for (int i = 0; i < list.Count; ++i)
			{
				list[i] = func(list[i]);
			}
		}

		/// <summary>
		/// リストからランダムな値を取得する
		/// </summary>
		public static T GetRandom<T>(this IList<T> list)
		{
			if (list.IsNullOrEmpty()) return default(T);

			return list[Utility.Random.Range(list.Count())];
		}
    }
}
