//
// RectData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.31
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Map.Builder.Split
{
	/// <summary>
	/// 区画を操作する
	/// </summary>
	public class MapRectData
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数


		#endregion


		#region private 変数

		private RectData[] _data = null;        // 区画データ

		#endregion


		#region プロパティ

		/// <summary>
		/// 現在の最新の区画データを返す
		/// </summary>
		public RectData LatestData => _data[RectCount - 1];

		public RectData this[int index] => _data[index];

		/// <summary>
		/// 区画の数
		/// </summary>
		public int RectCount { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		public MapRectData()
		{
			// 最大の区画分しか作らない(使い回しを考える)
			_data = new RectData[SplitConst.MaxRectNum];

			for (int i = 0; i < SplitConst.MaxRectNum; ++i)
			{
				_data[i] = new RectData();
			}
		}

		#endregion


		#region public, protected 関数



		/// <summary>
		/// 区画を作成する
		/// 指定した引数がそのまま区画の大きさとなる
		/// </summary>
		public RectData CreateRect(int left, int top, int right, int bottom)
		{
			var rect = _data[RectCount];
			rect.Initialize();

			rect.rect = new RectInt();
			rect.rect.xMin = left;
			rect.rect.yMin = top;
			rect.rect.xMax = right;
			rect.rect.yMax = bottom;

			++RectCount;

			return rect;
		}

		/// <summary>
		/// 保持しているデータからランダムに一つ取得する
		/// </summary>
		/// <returns></returns>
		public RectData GetRandomData() =>
			_data[Utility.Random.Range(RectCount - 1)];

		/// <summary>
		/// Cell座標から区画データを返す
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public RectData FindDataByCellPos(Vector2Int pos) =>
			System.Array.Find(_data, data_ => data_.InRectRange(pos));

		/// <summary>
		/// 隣接している区画同士のデータをつなげる
		/// </summary>
		public void ConnectNeighbor(RectData targetData)
		{
			var targetRect = targetData.rect;

			foreach (var data in _data)
			{
				if (data == targetData) continue;

				var rect = data.rect;

				// 上
				if (targetRect.yMin == rect.yMax)
				{
					targetData.nearbyTop.Add(data);
					data.nearbyBottom.Add(data);
					continue;
				}

				// 下
				if (targetRect.yMax == rect.yMin)
				{
					targetData.nearbyBottom.Add(data);
					data.nearbyTop.Add(data);
					continue;
				}

				// 左
				if (targetRect.xMin == rect.xMax)
				{
					targetData.nearbyLeft.Add(data);
					data.nearbyRight.Add(data);
					continue;
				}

				// 右
				if (targetRect.xMax == rect.yMin)
				{
					targetData.nearbyRight.Add(data);
					data.nearbyLeft.Add(data);
					continue;
				}
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
