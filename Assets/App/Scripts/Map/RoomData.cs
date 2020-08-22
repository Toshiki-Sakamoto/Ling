//
// RoomData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.16
//

using System.Collections.Generic;
using UnityEngine;
using Ling.Const;
using System.Linq;

namespace Ling.Map
{
	/// <summary>
	/// 部屋データ
	/// </summary>
	public class RoomData
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Dictionary<Const.TileFlag, List<TileData>> _tileFlagAndData = new Dictionary<Const.TileFlag, List<TileData>>();

		#endregion


		#region プロパティ

		public List<TileData> TileData { get; } = new List<TileData>();

		public int RoomIndex { get; private set; }

		/// <summary>
		/// 出口座標
		/// </summary>
		public List<Vector2Int> ExitPositions { get; } = new List<Vector2Int>();

		#endregion


		#region コンストラクタ, デストラクタ

		public RoomData(int roomIndex)
		{
			RoomIndex = roomIndex;
		}

		#endregion


		#region public, protected 関数

		public void Add(in TileData tileData)
		{
			tileData.SetRoomIndex(RoomIndex);
			TileData.Add(tileData);
		}

		public void UpdateFlagDataMap()
		{
			_tileFlagAndData.Clear();

			foreach (var tileData in TileData)
			{
				AddFlagData(tileData.Flag, tileData);
			}
		}

		/// <summary>
		/// 部屋の中のランダムなTileDataを取得する
		/// </summary>
		public TileData GetRandom()
		{
			return TileData.GetRandom();
		}

		/// <summary>
		/// 指定したTileFlagを持っているか
		/// </summary>
		public bool ExistsTileFlags(Const.TileFlag tileFlag) =>
			_tileFlagAndData.ContainsKey(tileFlag);

		public bool TryGetTileDataList(Const.TileFlag tileFlag, out List<TileData> list)
		{
			if (_tileFlagAndData.TryGetValue(tileFlag, out list))
			{
				return true;
			}

			return false;
		}
		public bool TryGetTilePositionList(Const.TileFlag tileFlag, out List<Vector2Int> list)
		{
			list = null;

			if (TryGetTileDataList(tileFlag, out var tileDataList))
			{
				list = tileDataList.Select(tileData_ => tileData_.Pos).ToList();
				return true;
			}

			return false;
		}

		/// <summary>
		/// 出口座標を追加する
		/// </summary>
		public void AddExitPosition(in Vector2Int pos)
		{
			// すでに追加されていれば何もしない
			if (ExitPositions.Contains(pos)) return;

			ExitPositions.Add(pos);
		}

		/// <summary>
		/// 出口から削除
		/// </summary>
		public void RemoveExitPosition(in Vector2Int pos) =>
			ExitPositions.Remove(pos);

		#endregion


		#region private 関数

		private void AddFlagData(TileFlag tileFlag, TileData tileData)
		{
			tileFlag.GetFlags(flag_ =>
				{
					if (!_tileFlagAndData.TryGetValue(tileFlag, out var list))
					{
						list = new List<TileData>();
						_tileFlagAndData.Add(tileFlag, list);
					}

					list.Add(tileData);
				});
		}

		#endregion
	}
}
