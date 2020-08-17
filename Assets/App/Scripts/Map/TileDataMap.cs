//
// MapData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.21
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Ling.Const;


namespace Ling.Map
{
	/// <summary>
	/// <see cref="TileData"/>を管理する
	/// </summary>
	public class TileDataMap : IEnumerable<TileData>
    {
		#region 定数, class, enum

		public struct Enumerator : IEnumerator<TileData>
		{
			private readonly TileDataMap _list;
			private int _index;


			public Enumerator(TileDataMap list)
			{
				_list = list;
				_index = -1;
			}

			public TileData Current => _list.GetTile(_index);
			object IEnumerator.Current => _list.GetTile(_index);
			public  void Dispose() {}
			public bool MoveNext() => ++_index < _list.Size;
			public void Reset() => _index = 0;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public int Width { get; private set; }
		public int Height { get; private set; }

		public int Size => Width * Height;

		public TileData[] Tiles { get; private set; }

		/// <summary>
		/// 部屋MAP
		/// つながってる部屋は同じ値が入る 1～
		/// </summary>
		public Dictionary<int, RoomData> Rooms { get; } = new Dictionary<int, RoomData>();

		/// <summary>
		/// 道Map
		/// </summary>
		public int[] RoadMapArray { get; private set; }
		public Dictionary<int, List<Vector2Int>> RoadMap { get; } = new Dictionary<int, List<Vector2Int>>();

		/// <summary>
		/// 下り階段の場所
		/// </summary>
		public Vector2Int StepDownPos { get; private set; }

		/// <summary>
		/// 走査
		/// </summary>
		public TileDataMapScanner Scanner { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public IEnumerator<TileData> GetEnumerator() => new Enumerator(this);
		IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

		public void Initialize(int width, int height)
		{
			Width = width;
			Height = height;

			Tiles = new TileData[width * height];

			for (int y = 0; y < Height; ++y)
			{
				for (int x = 0; x < Width; ++x)
				{
					var index = y * Width + x;

					Tiles[index].SetPos(x, y);
					Tiles[index].SetIndex(index);
				}
			}
		}

		public void AllTilesSetWall()
		{
			Tiles.ForEach((ref TileData tileData_) => tileData_.SetWall());
		}

		/// <summary>
		/// 部屋のマップを作成する
		/// </summary>
		public void BuildRoomMap() =>
			BuildMapInternal(Rooms, TileFlag.Floor);

		/// <summary>
		/// 指定した場所のTileDataを部屋とする
		/// </summary>
		public void AddRoomMap(int x, int y, int roomIndex)
		{
			if (TryGetRoomData(roomIndex, out var roomData))
			{
				var tileData = GetTileData(x, y);
				tileData.SetFlag(TileFlag.Floor);

				roomData.Add(tileData);
			}
		}

		public bool TryGetRoomData(int roomIndex, out RoomData roomData) =>
			Rooms.TryGetValue(roomIndex, out roomData);

		/// <summary>
		/// 道のマップを作成する
		/// </summary>
		//public void BuildRoadMap() =>
		//	BuildMapInternal(RoadMap, TileFlag.Road);


        /// <summary>
        /// 指定区画を指定フラグで上書きする
        /// </summary>
        public void FillRect(int left, int top, int right, int bottom, TileFlag flag, System.Predicate<TileData> predicate = null)
        {
            for (int y = top; y < bottom; ++y)
            {
                for (int x = left; x < right; ++x)
                {
					if (!InRange(x, y)) continue;

					var tileData = this.GetTile(x, y);

					// 許可されたところのみフラグを設定する
					if (predicate != null)
					{
						if (!predicate(tileData))
						{
							continue;
						}
					}

					tileData.SetFlag(flag);
                }
            }
        }

		/// <summary>
		/// 道を作成する。
		/// 途中に部屋と隣接する場合は上書きしない
		/// </summary>
		/// <param name="left"></param>
		/// <param name="top"></param>
		/// <param name="right"></param>
		/// <param name="bottom"></param>
		public void FillRectRoad(int left, int top, int right, int bottom, System.Predicate<TileData> predicate = null)
		{
			for (int y = top; y < bottom; ++y)
			{
				for (int x = left; x < right; ++x)
				{
					var tileData = this.GetTile(x, y);
					if (!predicate?.Invoke(tileData) ?? false) continue;

					// 部屋の場所は書き換えない
				//	if (tileData.HasFlag(TileFlag.Floor)) continue;

					// 部屋と隣接していたらtrueをいれる
					tileData.SetFlag(TileFlag.Road);
				}
			}
		}
		public void FillRectRoadReverse(int left, int top, int right, int bottom, System.Predicate<TileData> predicate = null)
		{
			for (int y = bottom - 1; y >= top; --y)
			{
				for (int x = right - 1; x >= left; --x)
				{
					var tileData = this.GetTile(x, y);
					if (!predicate?.Invoke(tileData) ?? false) continue;

					// 部屋の場所は書き換えない
					//	if (tileData.HasFlag(TileFlag.Floor)) continue;

					// 部屋と隣接していたらtrueをいれる
					tileData.SetFlag(TileFlag.Road);
				}
			}
		}

		/// <summary>
		/// TileDataを取得する
		/// </summary>
		public TileData GetTileData(int x, int y) =>
			Tiles[y * Width + x];

		/// <summary>
		/// 範囲内かどうか
		/// </summary>
		public bool InRange(int x, int y) =>
			x >= 0 && x < Width && y >= 0 && y < Height;

		/// <summary>
		/// 下り階段の場所を設定する
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void SetStepDownFlag(int x, int y)
		{
			StepDownPos = new Vector2Int(x, y);

			this.GetTile(x, y).AddFlag(TileFlag.StepDown);
		}

		/// <summary>
		/// 指定した座標の部屋番号を取得する
		/// </summary>
		public int GetRoomMapIndex(int x, int y)
		{
			if (!InRange(x, y)) return -1;
			return GetTileData(x, y).RoomIndex;
		}

		/// <summary>
		/// 指定した部屋番号と一致していればtrue
		/// </summary>
		public bool EqualRoomMapValue(int x, int y, int value) =>
			GetRoomMapIndex(x, y) == value;

		/// <summary>
		/// 指定したTileFlagと隣接していたらtrue
		/// </summary>
		public bool IsAdjacent(int x, int y, TileFlag tileFlag) =>
			Utility.Map.CallDirection(x, y, (_x, _y) => this.GetTile(_x, _y).HasFlag(tileFlag));

		/// <summary>
		/// 部屋と隣接する場合true
		/// </summary>
		public bool IsRoomAdjacent(int x, int y) =>
			IsAdjacent(x, y, TileFlag.Floor);

		public bool IsRoomAdjacent(in Vector2Int pos) =>
			IsRoomAdjacent(pos.x, pos.y);

		/// <summary>
		/// 隣接しているtileFlagの数を返す
		/// </summary>
		public void GetAdjastPosList(in Vector2Int pos, TileFlag tileFlag, List<Vector2Int> lists)
		{
			lists.Clear();

			Utility.Map.CallDirection(pos.x, pos.y,
				(_x, _y) =>
				{ 
					if (this.GetTile(_x, _y).HasFlag(tileFlag))
					{
						lists.Add(new Vector2Int(_x, _y));
					}
				});
		}
		public int GetAdjastNum(in Vector2Int pos, TileFlag tileFlag)
		{
			int result = 0;

			Utility.Map.CallDirection(pos.x, pos.y,
				(_x, _y) =>
				{
					if (this.GetTile(_x, _y).HasFlag(tileFlag))
					{
						++result;
					}
				});

			return result;
		}

		/// <summary>
		/// 部屋情報の更新
		/// </summary>
		public void UpdateRoomData()
		{

		}

		#endregion


		#region private 関数


		private void BuildMapInternal(Dictionary<int, RoomData> rooms, TileFlag tileFlag)
		{
			rooms.Clear();

			void Scan(int x, int y, int roomIndex, RoomData roomData)
			{
				if (!InRange(x, y)) return;

				var index = y * Width + x;

				// すでに値が入ってる場合
				var tileData = GetTileData(x, y);
				if (tileData.RoomIndex != 0) return;

				// 部屋以外の場合
				if (!tileData.HasFlag(tileFlag)) return;

				tileData.SetRoomIndex(roomIndex);
				roomData.Add(tileData);

				// 上下左右
				Scan(x - 1, y, roomIndex, roomData);
				Scan(x + 1, y, roomIndex, roomData);
				Scan(x, y - 1, roomIndex, roomData);
				Scan(x, y + 1, roomIndex, roomData);
			}

			int value = 0;

			for (int y = 0; y < Height; ++y)
			{
				for (int x = 0; x < Width; ++x)
				{
					// すでに値が入っていたら何もしない
					var tileData = GetTileData(x, y);

					if (tileData.RoomIndex != 0) continue;
					if (!tileData.HasFlag(tileFlag)) continue;

					var roomIndex = ++value;
					if (!rooms.TryGetValue(roomIndex, out var roomData))
					{
						roomData = new RoomData(roomIndex);
						rooms.Add(value, roomData);
					}

					Scan(x, y, value, roomData);
				}
			}
		}

		#endregion
	}
}
