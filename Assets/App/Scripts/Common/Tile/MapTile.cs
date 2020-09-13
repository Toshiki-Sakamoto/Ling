// 
// MiniMapTile.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.02
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Ling.Utility.Extensions;
using Zenject;
using Ling.Map;
using Ling.Map.TileDataMapExtensions;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Ling.Common.Tile
{
	/// <summary>
	/// MiniMap用のTile
	/// </summary>
	[System.Serializable]
	public class MapTile : TileBase 
    {
		#region 定数, class, enum

		public static readonly int SpriteMax = 15;


		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private MapTileSpriteData[] _spriteData = null;
		[SerializeField] private Color _color = Color.white;

		private Const.TileFlag _currentTileFlag;

		#endregion


		#region プロパティ

		public MapTileSpriteData[] SpriteData => _spriteData;

		public Color TileColor { get { return _color; } set { _color = value; } }

		/// <summary>
		/// タイルデータ
		/// </summary>
		public Map.TileDataMap TileDataMap { get; private set; }

		#endregion


		#region public, protected 関数

		/// <summary>
		/// MiniMapのSpriteデータを持つクラスの設定
		/// </summary>
		public void SetupMapData()
		{
			// TileFlagの数だけSpriteデータクラスを作成しセットアップする
			var tileFlags = System.Enum.GetValues(typeof(Const.TileFlag));
			_spriteData = new MapTileSpriteData[tileFlags.Length];

			int count = 0;
			foreach (Const.TileFlag tileFlag in tileFlags)
			{
				var miniMapData = new MapTileSpriteData();
				miniMapData.Setup(tileFlag, SpriteMax);

				_spriteData[count++] = miniMapData;
			}
		}

		/// <summary>
		/// タイルマップ情報
		/// ミニマップ生成時に設定すること
		/// </summary>
		/// <param name="tileDataMap"></param>
		public void SetTileDataMap(Map.TileDataMap tileDataMap)
		{
			TileDataMap = tileDataMap;
		}

		/// <summary>
		/// 周り9つの点から呼ばれている
		/// </summary>
		/// <param name="position"></param>
		/// <param name="tilemap"></param>
		public override void RefreshTile(Vector3Int position, ITilemap tilemap)
		{
			for (int y = -1; y <= 1; ++y)
			{
				for (int x = -1; x <= 1; ++x)
				{
					var tilePos = new Vector3Int(position.x + x, position.y + y, position.z);

					if (IsNighbor(tilemap, position, tilePos))
					{
						tilemap.RefreshTile(tilePos);
					}
				}
			}
		}

		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref UnityEngine.Tilemaps.TileData tileData)
		{
			UpdateTile(position, tilemap, ref tileData);
		}

		#endregion


		#region private 関数

		/// <summary>
		/// Tileは全てインスタンス同じ
		/// TilemapにつきTileBaseも一つ
		/// </summary>
		private bool IsNighbor(ITilemap tilemap, Vector3Int position)
		{
			var tile = tilemap.GetTile(position);
			return tile != null && tile == this;
		}

		private bool IsNighbor(ITilemap tilemap, Vector3Int position, Vector3Int oppsitePosition)
		{
			// まだタイル情報が設定されていない
			if (TileDataMap == null) return false;

			var tile = tilemap.GetTile(position);
			if (tile == null || tile != this) return false;

			// タイル同士が同じか、特定の組み合わせの場合true
			if (!TileDataMap.InRange(position.x, position.y) || !TileDataMap.InRange(oppsitePosition.x, oppsitePosition.y))
			{
				return false;
			}

			var tileData = TileDataMap.GetTile(position.x, position.y);
			var oppsiteTileData = TileDataMap.GetTile(oppsitePosition.x, oppsitePosition.y);

			if (tileData.HasFlag(oppsiteTileData.Flag))
			{
				return true;
			}

			// 壁と道は隣接とする
			var floorAndRoad = Const.TileFlag.Floor | Const.TileFlag.Road;
			if (tileData.HasFlag(floorAndRoad) && oppsiteTileData.HasFlag(floorAndRoad))
			{
				return true;
			}

			return false;
		}

		private void UpdateTile(Vector3Int position, ITilemap tilemap, ref UnityEngine.Tilemaps.TileData tileData)
		{
			if (TileDataMap == null) return;

			tileData.transform = Matrix4x4.identity;
			tileData.color = Color.white;

			if (!IsNighbor(tilemap, position)) return;

			var tileFlag = TileDataMap.GetTileFlag(position.x, position.y);

			// Spriteデータがない場合は何もしない
			var miniMapData = System.Array.Find(_spriteData, miniMapData_ => miniMapData_.HasFlag(tileFlag));
			if (miniMapData == null)
			{
				tileData.sprite = null;
				return;
			}

			int mask = 0;

			// 全部存在する場合255
			mask += IsNighbor(tilemap, position, position + new Vector3Int(0, 1, 0)) ? 1 : 0;	// y + 1
			mask += IsNighbor(tilemap, position, position + new Vector3Int(1, 1, 0)) ? 2 : 0;
			mask += IsNighbor(tilemap, position, position + new Vector3Int(1, 0, 0)) ? 4 : 0;	// x + 1
			mask += IsNighbor(tilemap, position, position + new Vector3Int(1, -1, 0)) ? 8 : 0;
			mask += IsNighbor(tilemap, position, position + new Vector3Int(0, -1, 0)) ? 16 : 0;	// y - 1
			mask += IsNighbor(tilemap, position, position + new Vector3Int(-1, -1, 0)) ? 32 : 0;
			mask += IsNighbor(tilemap, position, position + new Vector3Int(-1, 0, 0)) ? 64 : 0;	// x - 1
			mask += IsNighbor(tilemap, position, position + new Vector3Int(-1, 1, 0)) ? 128 : 0;

			byte original = (byte)mask;
			if ((original | 254) < 255) mask &= 125;    // 254(11111110), 125(01111101)
			if ((original | 251) < 255) mask &= 245;    // 251(11111011), 245(11110101)
			if ((original | 239) < 255) mask &= 215;    // 239(11101111), 215(11010111)
			if ((original | 191) < 255) mask &= 95;     // 191(10111111), 95 (01011111)

			int index = GetIndex((byte)mask);
			if (index >= 0)
			{
				tileData.sprite = miniMapData.GetSpriteByInex(index);
				tileData.transform = GetTransform((byte)mask);	// 回転させてSpriteを合わせる
				tileData.color = _color;
				tileData.flags = (TileFlags.LockTransform | TileFlags.LockColor);
				tileData.colliderType = UnityEngine.Tilemaps.Tile.ColliderType.None;
			}
		}

		private int GetIndex(byte mask)
		{
			// https://qiita.com/RyotaMurohoshi/items/1629c50a9d9f70f31c83
			// mask値によって壁があるマスを作り、後は回転させる
			switch (mask)
			{
				// Filled 周り全てが異なる
				case 0: 
					return 0;

				// Three Sides 上方向だけつながっている
				case 1: 
				case 4:
				case 16:
				case 64:
					return 1;

				// Two Sides and One Corner 上と右がつながっていて、右斜め上が角
				case 5:
				case 20:
				case 80:
				case 65: return 2;

				// Two Adjacent Sides 上と右上と右がつながっている
				case 7:
				case 28:
				case 112:
				case 193: return 3;

				// Two Oppsite Sides 上と下がつながっている
				case 17:
				case 68: return 4;

				// One Side and Two Corners 上と右と下がつながっている
				case 21:
				case 84:
				case 81:
				case 69: return 5;

				// One Side and One Lower Conner 上と右上と右と下がつながっている
				case 23:
				case 92:
				case 113:
				case 197: return 6;

				// One Side and One Upper Conrner 上と右と右下と下がつながっている
				case 29:
				case 116:
				case 209:
				case 71: return 7;

				// One Side 上と右上と右と右下と下がつながっている
				case 31:
				case 124:
				case 241:
				case 199: return 8;

				// Four Corners 上と右と下と左がつながっている
				case 85: return 9;

				// Three Corners 上と右上と右と下と左がつながっている
				case 87:
				case 93:
				case 117:
				case 213: return 10;

				// Two Adjacent Corners 上と右上と右と右下と下と左がつながっている
				case 95:
				case 125:
				case 245:
				case 215: return 11;

				// Two Oppsite Corners 上と右上と右と下と左下と左がつながっている
				case 119:
                case 221: return 12;

				// Oner Corner 上と右上と右と右下と下と左下と左がつながっている
				case 127:
                case 253:
				case 247:
				case 223: return 13;

				// Empty 全てがつながっている
				case 255: return 14;
			}

			return -1;
		}


		private Matrix4x4 GetTransform(byte mask)
		{
			switch (mask)
			{
				case 4:
				case 20:
				case 28:
				case 68:
				case 84:
				case 92:
				case 116:
				case 124:
				case 93:
				case 125:
				case 221:
				case 253:
					return Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -90f), Vector3.one);

				case 16:
				case 80:
				case 112:
				case 81:
				case 113:
				case 209:
				case 241:
				case 117:
				case 245:
				case 247:
					return Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -180f), Vector3.one);

				case 64:
				case 65:
				case 193:
				case 69:
				case 197:
				case 71:
				case 199:
				case 213:
				case 215:
				case 223:
					return Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -270f), Vector3.one);
			}

			return Matrix4x4.identity;
		}
		
		#endregion


		
		#region MonoBegaviour

		
		#endregion
	}
}