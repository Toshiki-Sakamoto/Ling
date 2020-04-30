//
// SimpleRoadBuilder.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.30
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ling.Map.Builder.Split.Road
{
	/// <summary>
	/// 
	/// </summary>
	public class SimpleRoadBuilder : ISplitRoadBuilder
    {
		#region 定数, class, enum

		public class Factory : PlaceholderFactory<SimpleRoadBuilder> { }

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

		public IEnumerator<float> Build(TileDataMap tileDataMap, MapRect mapRect)
		{
			for (int i = 0; i < mapRect.RectCount - 1; ++i)
			{
				CreateRoad(tileDataMap, mapRect, i, i + 1);

				yield return 0.5f;
			}
		}

		#endregion


		#region private 関数


		/// <summary>
		/// 部屋と部屋をつなぐ道を作成する
		/// </summary>
		/// <returns></returns>
		private void CreateRoad(TileDataMap tileDataMap, MapRect mapRect, int indexA, int indexB)
		{
			var dataA = mapRect[indexA];
			var dataB = mapRect[indexB];

			var rectA = dataA.rect;
			var rectB = dataB.rect;
			var roomA = dataA.room;
			var roomB = dataB.room;

			// 区画は上下左右のどちらでつながっているかで処理を分ける
			if (rectA.yMax == rectB.yMin || rectA.yMin == rectB.yMax)
			{
				var x1 = Utility.Random.Range(roomA.xMin, roomA.xMax - 1);
				var x2 = Utility.Random.Range(roomB.xMin, roomB.xMax - 1);

				int y;

				if (rectA.yMin > rectB.yMin)
				{
					// B
					// A
					y = rectA.yMin;

					// Aと横道を繋ぐ道を作る
					tileDataMap.FillRect(x1, y + 1, x1 + 1, roomA.yMin, TileFlag.Road);

					// Bと横道を繋ぐ道を作る
					tileDataMap.FillRect(x2, roomB.yMax, x2 + 1, y, TileFlag.Road);
				}
				else
				{
					// A
					// B
					y = rectB.yMin;

					tileDataMap.FillRect(x2, y + 1, x2 + 1, roomB.yMin, TileFlag.Road);
					tileDataMap.FillRect(x1, roomA.yMax, x1 + 1, y, TileFlag.Road);
				}

				var left = Mathf.Min(x1, x2);
				var right = Mathf.Max(x1, x2) + 1;

				tileDataMap.FillRect(left, y, right, y + 1, TileFlag.Road);

				return;
			}

			if (rectA.xMax == rectB.xMin || rectA.xMin == rectB.xMax)
			{
				var y1 = Utility.Random.Range(roomA.yMin, roomA.yMax - 1);
				var y2 = Utility.Random.Range(roomB.yMin, roomB.yMax - 1);

				int x;

				if (rectA.xMin > rectB.xMin)
				{
					// BA
					x = rectA.xMin;

					tileDataMap.FillRect(roomB.xMax, y2, x, y2 + 1, TileFlag.Road);
					tileDataMap.FillRect(x + 1, y1, roomA.xMin, y1 + 1, TileFlag.Road);
				}
				else
				{
					// AB
					x = rectB.xMin;

					tileDataMap.FillRect(roomA.xMax, y1, x, y1 + 1, TileFlag.Road);
					tileDataMap.FillRect(x + 1, y2, roomB.xMin, y2 + 1, TileFlag.Road);
				}

				var top = Mathf.Min(y1, y2);
				var bottom = Mathf.Max(y1, y2) + 1;

				tileDataMap.FillRect(x, top, x + 1, bottom, TileFlag.Road);

				return;
			}
		}


		#endregion
	}
}
