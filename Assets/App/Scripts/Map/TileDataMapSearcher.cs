//
// MapSearcher.cs
// ProductName Ling
//
// Created by  on 2021.09.06
//

using System.Collections.Generic;
using System.Linq;
using Ling.Const;
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

		/// <summary>
		/// ターゲット最大数
		/// </summary>
		public int TargetMaxNum { get; set; } = 1;
	}

	public class SearchResult
	{
		public class Entity
		{
			public Const.TileFlag Flag;
			public Vector2Int Pos;
		}

		/// <summary>
		/// 見つけたターゲット情報
		/// </summary>
		public List<Entity> Entities { get; } = new List<Entity>();


		public void Add(in Vector2Int pos, Const.TileFlag flag) =>
			Entities.Add(new Entity { Flag = flag, Pos = pos });

		public int Count() => Entities.Count();
	}

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

		private TileDataMap _tileDataMap;
		private SearchOption _option;
		private SearchResult _result = null;

		private int _remTargetNum;

		#endregion


		#region プロパティ

		private bool FoundMaxCount => Result.Count() == _option.TargetMaxNum;
		private SearchResult Result => _result ?? (_result = new SearchResult());


		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public TileDataMapSearcher(TileDataMap tileDataMap)
		{
			_tileDataMap = tileDataMap;
		}


		/// <summary>
		/// 1マス以内にターゲットがいるか検索
		/// </summary>
		public bool SearchWithin1Square(Vector2Int srcPos, Const.TileFlag targetFlag)
		{
			var result = Vector2Int.zero;
			var resultFlag = Const.TileFlag.None;

			var exists = DirectionTypeUtility.CallDirection(dir => 
				{
					result = srcPos + dir.ToVec2Int();
					return ExistsTargetInDirection(srcPos, targetFlag, dir, out var resultFlag);
				});

			if (exists)
			{
				Result.Add(result, resultFlag);
				return true;
			}

			return false;
		}

		/// <summary>
		/// 指定した方向1マスを検索して存在すればtrue
		/// </summary>
		public bool ExistsTargetInDirection(in Vector2Int srcPos, Const.TileFlag targetFlag, DirectionType dirType, out Const.TileFlag resultFlag)
		{
			resultFlag = Const.TileFlag.None;

			var pos = srcPos + dirType.ToVec2Int();

			if (CanSearch(pos.x, pos.y, dirType)) return false;

			resultFlag = _tileDataMap.GetTileFlag(pos.x, pos.y);
			return resultFlag.HasAny(targetFlag);
		}

		/// <summary>
		/// サーチ可能の場合true
		/// </summary>
		public bool CanSearch(int x, int y, DirectionType dirType)
		{
			if (dirType.IsDiagonal())
			{
				if (_tileDataMap.HasFlag(0, y, _option.DiagonalInvalidFlag)) return false;
				if (_tileDataMap.HasFlag(x, 0, _option.DiagonalInvalidFlag)) return false;
 			}

			var addPos = dirType.ToVec2Int();
			var dstX = x + addPos.x;
			var dstY = y + addPos.y;
			
			// 壁を無視できない時
			if (!_option.IsWallIgnore)
			{
				if (_tileDataMap.HasFlag(dstX, dstY, Const.TileFlag.Wall)) return false;
			}
			
			return true;
		}

		#endregion


		#region private 関数

		private void Setup(SearchOption option)
		{
			_option = option;
			_remTargetNum = _option.TargetMaxNum;

			_result = null;
		}

		/// <summary>
		/// 再帰呼び出して特定の方向を調べていく
		/// </summary>
		private bool SearchInternal(in Vector2Int srcPos, Const.TileFlag targetFlag, DirectionType dirType, int searchLimitNum)
		{
			if (searchLimitNum <= 0) return false;

			if (ExistsTargetInDirection(srcPos, targetFlag, dirType, out var resultFlag))
			{
				Result.Add(srcPos + dirType.ToVec2Int(), resultFlag);

				if (FoundMaxCount) return true;
			}

			return SearchInternal(srcPos, targetFlag, dirType, --searchLimitNum);
		}

		#endregion
	}
}
