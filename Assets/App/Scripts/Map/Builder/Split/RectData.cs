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
using Ling.Const;
using Ling.Map.TileDataMapExtension;

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
			var adjastList = new List<Vector2Int>();

			foreach (var road in roads)
			{
				var roadPos = road.pos;

				// true, true, true.. と続くところは部屋と隣接する道が続いているので真ん中の道を削除する
				for (int i = 0; i < roadPos.Count; ++i)
				{
					var pos = roadPos[i];

					if (tileDataMap.GetTileFlag(pos).HasFloor())
					{
						continue;
					}

					// 部屋に隣接しているとき、前と後ろが道ならば自分を壁にする
					if (!tileDataMap.IsRoomAdjacent(pos))
					{
						continue;
					}

					// 部屋に三箇所隣接している道は部屋とする
					tileDataMap.GetAdjastPosList(pos, TileFlag.Floor, adjastList);
					if (adjastList.Count >= 3)
					{
						var tileData = tileDataMap.GetTile(pos);
						tileData.SetFlag(TileFlag.Floor);

						// MapValueにも書き込む
						// 作り直す
						tileDataMap.BuildRoomMap();
						continue;
					}

					// 同じ部屋に囲まれているときは同じものとする
					if (adjastList.Count == 2)
					{
						var mapValueA = tileDataMap.GetRoomIndex(adjastList[0].x, adjastList[0].y);
						var mapValueB = tileDataMap.GetRoomIndex(adjastList[1].x, adjastList[1].y);

						if (mapValueA == mapValueB)
						{
							// MapValueにも書き込む
							tileDataMap.AddRoomData(pos.x, pos.y, mapValueA);
						}

						continue;
					}

					if (i - 1 < 0) continue;
					if (i + 1 >= roadPos.Count) continue;

					// 一箇所で次も部屋が隣接している場合は部屋とする
					if (adjastList.Count == 1)
					{
						// 自分の部屋と同じであれば自分を部屋にする
						var roomPos = adjastList[0];
						var roomIndex = tileDataMap.GetRoomIndex(roomPos.x, roomPos.y);

						tileDataMap.GetAdjastPosList(roadPos[i + 1], TileFlag.Floor, adjastList);
						if (adjastList.Count != 1) continue;

						var nextRoomPos = adjastList[0];
						var nextRoomIndex = tileDataMap.GetRoomIndex(nextRoomPos.x, nextRoomPos.y);

						if (roomIndex == nextRoomIndex)
						{
							// MapValueにも書き込む
							tileDataMap.AddRoomData(pos.x, pos.y, nextRoomIndex);
						}
					}

					/*
					// 前後が道ならば自分は壁になる
					if (tileDataMap.GetTileFlag(roadPos[i - 1]) == TileFlag.Road &&
						tileDataMap.GetTileFlag(roadPos[i + 1]) == TileFlag.Road)
					{
						ref var tileData = ref tileDataMap.GetTile(pos);
						tileData.SetFlag(TileFlag.Wall);
					}*/
				}
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
