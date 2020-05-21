//
// RectData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.20
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Map.Builder.Split
{
	/// <summary>
	/// 区画情報
	/// </summary>
	public class RectData
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		public RectInt rect;   // 区画範囲
		public RectInt room;   // 部屋範囲
		public List<RoadData> roads;    // 道情報

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		// 隣接
		public List<RectData> nearbyAll = new List<RectData>();
		public List<RectData> nearbyTop = new List<RectData>();
		public List<RectData> nearbyBottom = new List<RectData>();
		public List<RectData> nearbyLeft = new List<RectData>();
		public List<RectData> nearbyRight = new List<RectData>();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

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



		/// <summary>
		/// 余分な道を取り除く
		/// </summary>
		public void RemoveExtraRoad(TileDataMap tileDataMap)
		{
			foreach (var road in roads)
			{
				var roadPos = road.pos;

				// true, true, true.. と続くところは部屋と隣接する道が続いているので真ん中の道を削除する
				for (int i = 0; i < roadPos.Count; ++i)
				{
					var pos = roadPos[i];

					if (tileDataMap.GetTileFlag(pos) == TileFlag.Floor) continue;

					// 部屋に隣接しているとき、前と後ろが道ならば自分を壁にする
					if (!tileDataMap.IsRoomAdjacent(pos)) continue;

					// 前後が道ならば自分は壁になる
					if (i - 1 < 0) continue;
					if (i + 1 >= roadPos.Count) continue;

					if (tileDataMap.GetTileFlag(roadPos[i - 1]) == TileFlag.Road &&
						tileDataMap.GetTileFlag(roadPos[i + 1]) == TileFlag.Road)
					{
						ref var tileData = ref tileDataMap.GetTile(pos);
						tileData.SetFlag(TileFlag.Wall);
					}
				}
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
