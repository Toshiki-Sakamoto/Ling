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
	public class SearchOption
	{
		/// <summary>
		/// ターゲット以外のキャラを無視するか
		/// </summary>
		public bool IsCharaIgnore { get; set; } = false;

		/// <summary>
		/// 壁を無視するか
		/// </summary>
		public bool IsWallIgnore { get; set; } = false;

		/// <summary>
		/// 同じ部屋であることが必要か(部屋じゃない場合1マスしか見ない)
		/// </summary>
		public bool NeedsSameRoom { get; set; } = true;

		/// <summary>
		/// 検索マスの制限がある場合
		/// </summary>
		public int TileLimitNum { get; set; } = 99; // 最大可能検索数を入れとく

		/// <summary>
		/// 直線検索のみ
		/// </summary>
		public bool IsStraightLine { get; set; }

		/// <summary>
		/// 斜め検索無効フラグ
		/// </summary>
		public Const.TileFlag DiagonalInvalidFlag { get; set; }
	}

	public class SearchResult
	{
		/// <summary>
		/// 見つけたターゲット座標
		/// </summary>
		public Vector2Int TargetPos { get; set; }
	}

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

			SearchResult result = null;

			DirectionTypeUtility.CallDirection(dir => 
				{
					result = SearchInternal(srcPos, targetFlag, dir, option, searchLimitNum);
					return result != null;
				});

			return result;
		}

		/// <summary>
		/// 1マス以内にターゲットがいるか検索
		/// </summary>
		public SearchResult SearchWithin1Square(Vector2Int srcPos, Const.TileFlag targetFlag, SearchOption option)
		{
			var result = Vector2Int.zero;

			var exists = DirectionTypeUtility.CallDirection(dir => 
				{
					result = srcPos + dir.ToVec2Int();
					return ExistsTargetInDirection(srcPos, targetFlag, dir, option);
				});

			if (exists)
			{
				return new SearchResult { TargetPos = result };
			}

			return null;
		}

		/// <summary>
		/// 指定した方向1マスを検索して存在すればtrue
		/// </summary>
		public bool ExistsTargetInDirection(in Vector2Int srcPos, Const.TileFlag targetFlag, DirectionType dirType, SearchOption option)
		{
			var pos = srcPos + dirType.ToVec2Int();

			if (CanSearch(pos.x, pos.y, dirType, option)) return false;

			return _tileDataMap.HasFlag(pos.x, pos.y, targetFlag);
		}

		/// <summary>
		/// サーチ可能の場合true
		/// </summary>
		public bool CanSearch(int x, int y, DirectionType dirType, SearchOption option)
		{
			if (dirType.IsDiagonal())
			{
				if (_tileDataMap.HasFlag(0, y, option.DiagonalInvalidFlag)) return false;
				if (_tileDataMap.HasFlag(x, 0, option.DiagonalInvalidFlag)) return false;
 			}

			var addPos = dirType.ToVec2Int();
			var dstX = x + addPos.x;
			var dstY = y + addPos.y;
			
			// 壁を無視できない時
			if (!option.IsWallIgnore)
			{
				if (_tileDataMap.HasFlag(dstX, dstY, Const.TileFlag.Wall)) return false;
			}
			
			return true;
		}

		#endregion


		#region private 関数

		/// <summary>
		/// 再帰呼び出して特定の方向を調べていく
		/// </summary>
		private SearchResult SearchInternal(in Vector2Int srcPos, Const.TileFlag targetFlag, DirectionType dirType, SearchOption option, int searchLimitNum)
		{
			if (searchLimitNum <= 0) return null;

			if (ExistsTargetInDirection(srcPos, targetFlag, dirType, option))
			{
				return new SearchResult { TargetPos = srcPos + dirType.ToVec2Int() };
			}

			return SearchInternal(srcPos, targetFlag, dirType, option, --searchLimitNum);
		}

		#endregion
	}
}
