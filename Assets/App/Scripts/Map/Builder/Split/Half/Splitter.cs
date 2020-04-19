//
// Splitter.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.01.01
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Ling.Map.Builder.Split.Half
{
	/// <summary>
	/// 半分の半分の半分.. と矩形を分割していく
	/// </summary>
	public class Splitter : ISplitter
	{
		#region 定数, class, enum

		/// <summary>
		/// Zenject Factory
		/// </summary>
		public class Factory : PlaceholderFactory<Splitter> { }

		#endregion


		#region public, protected 変数


		#endregion


		#region private 変数

		[Inject] private IManager _manager = null;

		private MapRect _mapRect;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 矩形を分割するとき呼び出される
		/// </summary>
		public void SplitRect(MapRect mapRect)
		{
			_mapRect = mapRect;

			SplitRect(ref _mapRect[0], isVertical: true);
		}

		#endregion


		#region private 関数

		/// <summary>
		/// 区画を細かく分割する
		/// </summary>
		/// <param name="parentData"></param>
		/// <param name="isVertical"></param>
		private void SplitRect(ref MapRect.Data parentData, bool isVertical)
		{
			// 分ける区画情報を取得
			ref var parentRect = ref parentData.rect;
			var data = _manager.Data;

			int pointA, pointB, distance, point;

			// 分割する
			if (isVertical)
			{
				// 縦

				if (parentRect.height < (data.RoomMinSize + 3) * 2 + 1)
				{
					return;
				}

				// 上部のA点を求める
				pointA = data.RoomMinSize + 3;

				// 下部のB点を求める
				pointB = parentRect.height - data.RoomMinSize - 4;

				// ABの距離を求める
				distance = pointB - pointA;

				// AB間のどこかに決定する
				point = pointA + UnityEngine.Random.Range(0, distance + 1);

				// 新しく右の区画を作成する 
				_mapRect.CreateRect(parentRect.x, parentRect.y + point, parentRect.width, parentRect.height);
			}
			else
			{
				//　横

				// 区分を分割できるか？チェック
				if (parentRect.width < (data.RoomMinSize + 3) * 2 + 1)
				{
					// 分割できるほど広くないので終了
					return;
				}

				// 左端のA点を求める
				pointA = data.RoomMinSize + 3;

				// 右端のB点を求める
				pointB = parentRect.width - data.RoomMinSize - 4;

				// ABの距離を求める
				distance = pointB - pointA;

				// AB間のどこかに決定する
				point = pointA + UnityEngine.Random.Range(0, distance + 1);

				// 新しく右の区画を作成する 
				_mapRect.CreateRect(parentRect.x + point, parentRect.y, parentRect.width, parentRect.height);
			}

			// 最新のRectを返すか一個前のRectを返すかをランダムで決める
			if (UnityEngine.Random.Range(0, 2) == 1)
			{
				SplitRect(ref _mapRect.LatestData, isVertical: !isVertical);
			}
			else
			{
				SplitRect(ref parentData, isVertical: !isVertical);
			}
		}

		#endregion
	}
}
