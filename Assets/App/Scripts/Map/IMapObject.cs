//
// IMapObject.cs
// ProductName Ling
//
// Created by  on 2021.09.06
//

using UnityEngine;
using UnityEngine.Tilemaps;
using Utility.Extensions;

namespace Ling.Map
{
	/// <summary>
	/// マップ上に存在するオブジェクトが持つ情報
	/// </summary>
	public interface IMapObject
	{
		/// <summary>
		/// どのマップ上に存在するか
		/// </summary>
		int Level { get; }

		/// <summary>
		/// マップ上の座標
		/// </summary>
		Vector2Int CellPos { get; }
	}


	public static class IMapObjectExtensions
	{
		public static Map.TileDataMap FindTileDataMap(this IMapObject self, MapManager manager) =>
			manager.MapControl.FindTileDataMap(self);

		/// <summary>
		/// Tilemapを取得する
		/// </summary>
		public static Tilemap FindTilemap(this IMapObject self, MapManager manager) =>
			manager.MapControl.FindTilemap(self.Level);

		public static Vector3 CellToWorld(this IMapObject self, MapManager manager) =>
			self.CellToWorld(self.CellPos, manager);

		public static Vector3 CellToWorld(this IMapObject self, Vector2Int pos, MapManager manager) =>
			self.FindTilemap(manager)?.GetCellCenterWorld(pos.ToVector3Int()) ?? Vector3.zero;
	}
}
