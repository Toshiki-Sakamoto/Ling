//
// TileData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.13
//

using UnityEngine;

namespace Ling.Map
{
	/// <summary>
	/// TileData系を直接扱う拡張メソッド群
	/// </summary>
	public static class TileDataMapTileData
    {
		/// <summary>
		/// [x, y] から指定したタイル情報を返す
		/// </summary>
		public static ref TileData GetTile(this TileDataMap self, int x, int y)
		{
			Utility.Log.Assert(x >= 0 && x <= self.Width && y >= 0 && y <= self.Height, "範囲から飛び出してます");

			return ref self.GetTile(y * self.Width + x);
		}
		public static ref TileData GetTile(this TileDataMap self, in Vector2Int pos) =>
			ref self.GetTile(pos.x, pos.y);

		public static ref TileData GetTile(this TileDataMap self, int index) =>
			ref self.Tiles[index];


		/// <summary>
		/// [x, y] から指定した<see cref="TileFlag"/>を返す
		/// </summary>
		public static TileFlag GetTileFlag(this TileDataMap self, int x, int y)
		{
			Utility.Log.Assert(x >= 0 && x <= self.Width && y >= 0 && y <= self.Height, "範囲から飛び出してます");
			return self.GetTileFlag(y * self.Width + x);
		}

		public static TileFlag GetTileFlag(this TileDataMap self, in Vector2Int pos) =>
			self.GetTileFlag(pos.x, pos.y);

		public static TileFlag GetTileFlag(this TileDataMap self, in Vector3Int pos) =>
			self.GetTileFlag(pos.x, pos.y);

		public static TileFlag GetTileFlag(this TileDataMap self, int index) =>
			self.GetTile(index).Flag;

		public static void SetTileFlag(this TileDataMap self, in Vector2Int pos, TileFlag tileFlag)
		{
			ref var tileData = ref self.GetTile(pos.x, pos.y);
			tileData.SetFlag(tileFlag);
		}

		
		/// <summary>
		/// 指定したマスが指定したTileFlagを持っている場合true
		/// </summary>
		public static bool HasFlag(this TileDataMap self, int x, int y, TileFlag tileFlag) =>
			self.GetTileFlag(x, y).HasAny(tileFlag);

		public static bool HasFlag(this TileDataMap self, in Vector2Int pos, TileFlag tileFlag) =>
			self.HasFlag(pos.x, pos.y, tileFlag);
	}
}
