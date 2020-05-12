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
	public class MapRect
    {
		#region 定数, class, enum

		/// <summary>
		/// 区画情報
		/// </summary>
		public class Data
		{
			public RectInt rect;   // 区画範囲
			public RectInt room;   // 部屋範囲
			public List<RoadData> roads;    // 道情報

			// 隣接
			public List<Data> nearbyAll = new List<Data>(); 
			public List<Data> nearbyTop = new List<Data>();
			public List<Data> nearbyBottom = new List<Data>();
			public List<Data> nearbyLeft = new List<Data>();
			public List<Data> nearbyRight = new List<Data>();

			public void Initialize()
			{
				rect = new RectInt();
				room = new RectInt();
				roads = new List<RoadData>();
				nearbyAll.Clear();
				nearbyTop.Clear();
				nearbyBottom.Clear();
				nearbyLeft.Clear();
				nearbyRight.Clear();
			}

			public bool InRectRange(in Vector2Int pos) =>
				rect.Contains(pos);
		}

		/// <summary>
		/// 道情報
		/// </summary>
		public class RoadData
		{
			public List<Vector2Int> pos = new List<Vector2Int>();    // 道の座標

			public void Add(Vector2Int pos) =>
				this.pos.Add(pos);
		}

		#endregion


		#region public, protected 変数


		#endregion


		#region private 変数

		private Data[] _data = null;        // 区画データ

		#endregion


		#region プロパティ

		/// <summary>
		/// 現在の最新の区画データを返す
		/// </summary>
		public Data LatestData => _data[RectCount - 1];

		public Data this[int index] => _data[index];

		/// <summary>
		/// 区画の数
		/// </summary>
		public int RectCount { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		public MapRect()
		{
			// 最大の区画分しか作らない(使い回しを考える)
			_data = new Data[SplitConst.MaxRectNum];

			for (int i = 0; i < SplitConst.MaxRectNum; ++i)
			{
				_data[i] = new Data();
			}
		}

		#endregion


		#region public, protected 関数



		/// <summary>
		/// 区画を作成する
		/// 指定した引数がそのまま区画の大きさとなる
		/// </summary>
		public Data CreateRect(int left, int top, int right, int bottom)
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
		public Data GetRandomData() =>
			_data[Utility.Random.Range(RectCount - 1)];

		/// <summary>
		/// Cell座標から区画データを返す
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public Data FindDataByCellPos(Vector2Int pos) =>
			System.Array.Find(_data, data_ => data_.InRectRange(pos));

		/// <summary>
		/// 隣接している区画同士のデータをつなげる
		/// </summary>
		public void ConnectNeighbor(Data targetData)
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
