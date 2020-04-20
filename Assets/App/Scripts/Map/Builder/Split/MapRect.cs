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

			public void Initialize()
			{
				rect = new RectInt();
				room = new RectInt();
			}
		}

		#endregion


		#region public, protected 変数


		#endregion


		#region private 変数

		private int _rectCount = 0;			// 区画の数
		private Data[] _data = null;        // 区画データ

		#endregion


		#region プロパティ

		/// <summary>
		/// 現在の最新の区画データを返す
		/// </summary>
		public ref Data LatestData => ref _data[_rectCount - 1];

		public ref Data this[int index] => ref _data[index];

		#endregion


		#region コンストラクタ, デストラクタ

		public MapRect()
		{
			// 最大の区画分しか作らない(使い回しを考える)
			_data = new Data[Const.MaxRectNum];
		}

		#endregion


		#region public, protected 関数



		/// <summary>
		/// 区画を作成する
		/// 指定した引数がそのまま区画の大きさとなる
		/// </summary>
		public ref Data CreateRect(int left, int top, int right, int bottom)
		{
			ref var rect = ref _data[_rectCount];
			rect.Initialize();

			rect.rect = new RectInt(left, top, right, bottom);

			++_rectCount;

			return ref rect;
		}


		#endregion


		#region private 関数

		#endregion
	}
}
