//
// Drawer.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.05
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Ling.Utility.Editor
{
	/// <summary>
	/// <see cref="UnityEditor.PropertyDrawer"/>関連の便利関数定義
	/// </summary>
	public static class Drawer
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 指定した区画の幅を<paramref name="num"/>で分割する
		/// </summary>
		/// <param name="rect">分割対象</param>
		/// <param name="num">分割する数</param>
		/// <param name="space">スペースを作る場合指定</param>
		/// <returns></returns>
		public static List<Rect> SplitHorizontalRects(in Rect rect, int num, float space = 0f)
		{
			if (num <= 0) return new List<Rect> { rect };

			var result = new List<Rect>();
			var x = rect.x;
			var w = (rect.width - (space * num - 1)) / num;

			for (int i = 0; i < num; ++i)
			{
				var r = rect;
				r.x = x;
				r.width = w;

				result.Add(r);

				x += w + space;
			}

			return result;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
