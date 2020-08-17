//
// RoomData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.16
//

using System.Collections.Generic;
using UnityEngine;
using Ling.Const;

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

		public int MapIndex { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Add(in TileData tileData)
		{
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
