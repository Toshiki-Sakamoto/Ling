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
using Ling.Utility.Extensions;

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

			// TileData内のフラグが更新されたとき
			tileData.onUpdateTileFlag = (tileData_, isAdded_) =>
				{
					if (isAdded_)
					{
						// 追加されたとき
						AddFlagData(tileData_.Flag, tileData_);
					}
					else
					{
						// 削除されたとき
						RemoveFlagData(tileData_.Flag, tileData_);
					}
				};
		}

		/// <summary>
		/// 現在の部屋情報を更新する
		/// 部屋に更新があるたびに呼び出す
		/// </summary>
		public void UpdateFlagDataMap()
		{
			foreach (var pair in _tileFlagAndData)
			{
				pair.Value.Clear();
			}

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
		public bool ExistsTileFlags(Const.TileFlag tileFlag)
		{
			if (_tileFlagAndData.TryGetValue(tileFlag, out var list))
			{
				return list.Count > 0;
			}

			return false;
		}

		public bool TryGetTileDataList(Const.TileFlag tileFlag, out List<TileData> list)
		{
			list = null;

			if (_tileFlagAndData.TryGetValue(tileFlag, out var result))
			{
				if (result.Count <= 0) return false;
				list = result;

				return true;
			}

			return false;
		}
		public bool TryGetTilePositionList(Const.TileFlag tileFlag, out List<Vector2Int> list)
		{
			list = null;

			if (TryGetTileDataList(tileFlag, out var tileDataList))
			{
				if (tileDataList.Count <= 0) return false;

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
					if (!_tileFlagAndData.TryGetValue(flag_, out var list))
					{
						list = new List<TileData>();
						_tileFlagAndData.Add(flag_, list);
					}

					list.Add(tileData);
				});
		}

		private void RemoveFlagData(TileFlag tileFlag, TileData tileData)
		{
			tileFlag.GetFlags(flag_ =>
				{
					if (_tileFlagAndData.TryGetValue(flag_, out var list))
					{
						list.Remove(tileData);
					}
				});
		}

		#endregion
	}
}
