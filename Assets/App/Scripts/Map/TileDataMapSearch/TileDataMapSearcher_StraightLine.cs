//
// MapSearcher.cs
// ProductName Ling
//
// Created by  on 2021.09.06
//

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ling.Map.TileDataMapExtensions;

namespace Ling.Map
{
	/// <summary>
	/// マップの検索
	/// </summary>
	public partial class TileDataMapSearcher
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ


		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 直線を検索する
		/// </summary>
		public Map.TileData SearchLine(in Vector2Int srcPos, in Vector2Int dir, Const.TileFlag flag) =>
			SearchLines(srcPos, dir, flag).LastOrDefault();

		public List<Map.TileData> SearchLines(in Vector2Int srcPos, in Vector2Int dir, Const.TileFlag flag, List<Map.TileData> result = null)
		{
			result = result ?? new List<TileData>();
			
			if (!_tileDataMap.InRange(srcPos.x, srcPos.y)) return result;
			
			result.Add(_tileDataMap.GetTileData(srcPos.x, srcPos.y));

			// 見つかったら終わり
			if (_tileDataMap.HasFlag(srcPos, flag)) return result;

			return SearchLines(srcPos + dir, dir, flag, result);
		}

		/// <summary>
		/// オプションから直線でターゲットフラグを検索する
		/// </summary>
		public SearchResult SearchStraightLine(Vector2Int srcPos, Const.TileFlag targetFlag, SearchOption option)
		{
			Setup(option);

			var searchLimitNum = option.TileLimitNum;

			// ターゲットと同じ部屋にいるか確認する
			if (option.NeedsSameRoom)
			{
				// 自分が部屋の中じゃない場合周り１マス以内にいなければ何もしない
				if (!_tileDataMap.TryGetRoomData(srcPos.x, srcPos.y, out var roomData))
				{
					searchLimitNum = 1;
				}
				else
				{
					// 同じ部屋じゃない場合1マス以内にいなければ何もしない
					if (!roomData.ExistsTileFlags(targetFlag))
					{
						searchLimitNum = 1;
					}
				}
			}

			DirectionTypeUtility.CallDirection(dir => 
				{
					return SearchInternal(srcPos, targetFlag, dir, searchLimitNum);
				});

			return _result;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
