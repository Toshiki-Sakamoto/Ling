//
// MapSearcher.cs
// ProductName Ling
//
// Created by  on 2021.09.06
//

using UnityEngine;
using Ling.Map.TileDataMapExtensions;

namespace Ling.Map
{
	/// <summary>
	/// マップの検索
	/// </summary>
	public class TileDataMapSearcher
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private TileDataMap _tileDataMap;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public TileDataMapSearcher(TileDataMap tileDataMap)
		{
			_tileDataMap = tileDataMap;
		}

		/// <summary>
		/// 直線を検索する
		/// </summary>
		public Map.TileData SearchLine(in Vector2Int srcPos, in Vector2Int dir, Const.TileFlag flag)
		{
			if (_tileDataMap.InRange(srcPos.x, srcPos.y)) 
			{
				// 範囲外に出たときは最後のマスを返す
				return _tileDataMap.GetTileData(srcPos.x, srcPos.y);
			}

			if (_tileDataMap.HasFlag(srcPos, flag))
			{
				// 終了
				return _tileDataMap.GetTileData(srcPos.x, srcPos.y);
			}

			return SearchLine(srcPos + dir, dir, flag);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
