//
// ScanExtensions.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.13
//

using UnityEngine;
using System.Collections.Generic;
using Ling.Const;
using Ling.Map.TileDataMapExtension;
using Zenject;

namespace Ling.Map
{
	/// <summary>
	/// 走査時に指定するオプション
	/// </summary>
	public class ScanOption 
	{
		public bool isEnemyIgnore = false;	// 敵を無視するか
		public bool isWallIgnore = false;	// 壁を無視するか
		public bool isHoleIgnore = false;	// 穴を無視するか
		public bool needsSameRoom = true;	// 同じ部屋であることが必要か(部屋じゃない場合1マスしか見ない)
	}

	public class CharaMoveChecker
	{
		private TileDataMap _tileDataMap;
		private Chara.ICharaController _chara;

		public CharaMoveChecker(TileDataMap tileDataMap)
		{
			_tileDataMap = tileDataMap;
		}

		public void Setup(Chara.ICharaController chara)
		{
			_chara = chara;
		}

		/// <summary>
		/// 移動できるか
		/// </summary>
		public bool CanMove(in Vector2Int pos)
		{
			var tileFlag = _tileDataMap.GetTileFlag(pos);

			// 移動できるか
			return _chara.Model.CanMoveTileFlag(tileFlag);
		}

		public bool CanDiagonalMove(in Vector2Int pos)
		{
			var tileFlag = _tileDataMap.GetTileFlag(pos);

			// 斜め移動のとき
			return _chara.Model.CanDiagonalMoveTileFlag(tileFlag);
		}

		/// <summary>
		/// 移動コスト
		/// </summary>
		public int GetMoveCost(in Vector2Int pos)
		{
			return 1;
		}
	}

	/// <summary>
	/// マップを走査する
	/// </summary>
	public class TileDataMapScanner
    {
		private TileDataMap _tileDataMap;
		private Utility.Algorithm.Search _search;
		private CharaMoveChecker _charaMoveChecker;

		public TileDataMapScanner(TileDataMap tileDataMap)
		{
			_tileDataMap = tileDataMap;
			_search = Utility.Algorithm.Search.Instance;
		}

		/// <summary>
		/// 全方位にTileFlagを検索する
		/// </summary>
		/// <param name="tileFlag">検索対象</param>
		/// <param name="cellNum">指定検索マス</param>
		/// <param name="predicate">trueで検索を終了する</param>
		/// <returns></returns>
		public bool ScanAllWithInCell(in Vector2Int srcPos, TileFlag tileFlag, int cellNum, ScanOption option, System.Predicate<Vector2Int> predicate)
		{
			if (!CanExecute(srcPos.x, srcPos.y, option)) return false;

			return true;
		}

		/// <summary>
		/// 指定した座標までのルートを取得する
		/// </summary>
		/// <param name="srcPos"></param>
		/// <param name="tileFlag"></param>
		/// <param name="cellNum"></param>
		/// <param name="option"></param>
		/// <returns></returns>
		//public bool TryGetRouteAtEndPos(in Vector2Int srcPos, TileFlag tileFlag, int cellNum, ScanOption option, out List<Vector2Int> routes)
		//{
		//}

		public bool TryGetRoutePositions(Chara.ICharaController chara, in Vector2Int targetPos, out List<Vector2Int> routePositions) =>
			TryGetScoreAndRoutePositions(chara, targetPos, out var score, out routePositions);

		/// <summary>
		/// 指定座標までのルート座標を取得する
		/// </summary>
		public bool TryGetScoreAndRoutePositions(Chara.ICharaController chara, in Vector2Int targetPos, out int score, out List<Vector2Int> routePositions)
		{
			score = 0;
			routePositions = null;

			var param = new Utility.Algorithm.Astar.Param();
			param.start = chara.Model.Pos;
			param.end = targetPos;
			param.width = _tileDataMap.Width;

			if (_charaMoveChecker == null)
			{
				_charaMoveChecker = new CharaMoveChecker(_tileDataMap);
			}

			_charaMoveChecker.Setup(chara);
			param.onCanMove = pos_ => _charaMoveChecker.CanMove(pos_);
			param.onCanDiagonalMove = pos_ => _charaMoveChecker.CanDiagonalMove(pos_);
			param.onTileCostGetter = pos_ => _charaMoveChecker.GetMoveCost(pos_);

			_search.Astar.Execute(param);
			if (!_search.Astar.IsSuccess)
			{
				return false;
			}

			if (!_search.Astar.TryGetScoreAndPositions(out score, out routePositions))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// 指定した座標の最短距離の座標ルートを取得する
		/// </summary>
		public bool TryGetShotestDisancePosition(Chara.ICharaController chara, in Vector2Int targetPos, out List<Vector2Int> routePositions) =>
			TryGetShotestDistancePositions(chara, new List<Vector2Int> { targetPos }, out var _, out routePositions);

		/// <summary>
		/// 指定した座標の中で最短距離の座標ルートを取得する
		/// </summary>
		public bool TryGetShotestDistancePositions(Chara.ICharaController chara, List<Vector2Int> targets, out Vector2Int targetPos, out List<Vector2Int> routePositions)
		{
			targetPos = Vector2Int.zero;
			routePositions = null;

			int minScore = int.MaxValue;
			
			foreach (var target in targets)
			{
				if (!TryGetScoreAndRoutePositions(chara, target, out var score, out var tmpPositions))
				{
					continue;
				}

				// 新しくルート取得した方のスコアのほうが小さい場合、そっちを採用
				if (score < minScore)
				{
					minScore = score;
					routePositions = tmpPositions;
					targetPos = target;
				}
			}

			if (routePositions == null)
			{
				return false;
			}

			return true;
		}


		/// <summary>
		/// 指定した地点から指定マス分走査する
		/// </summary>
		/// <param name="srcPos">検索開始位置</param>
		/// <param name="cellNum">検索マス数</param>
		/// <param name="option">検索オプション</param>
		public void ScanAllWithInCell(in Vector2Int srcPos, int cellNum, ScanOption option, System.Func<Vector2Int, int, bool> func)
		{
			if (func == null) return;
			if (!CanExecute(srcPos.x, srcPos.y, option)) return;

			// 全８方向を歩けるとする（例外はなし）
			// 一度通ったところは二度と通らないようにする
			var scanneds = new HashSet<int>();
			var width = _tileDataMap.Width;

			// 自分が部屋の中ではなく、同じ部屋であることが必要であれば１マスしか見ない
			if (option.needsSameRoom)
			{
				if (!_tileDataMap.HasFlag(srcPos, TileFlag.Floor))
				{
					cellNum = 1;
				}
			}

			bool ScanAllInternal(int scanNum, int remCellNum, int posX, int posY)
			{
				// 回数が0以下になった場合何もできないので何もしない
				if (remCellNum <= 0) return false;

				// すでに走査済みなら何もしない
				var index = posY * width + posX;
				if (scanneds.Contains(index)) return false;

				scanneds.Add(index);

				var tileFlag = _tileDataMap.GetTileFlag(posX, posY);

				// 同じ部屋である必要がある場合、部屋以外に来たとき走査回数が2回以上であれば何もしない
				if (!tileFlag.HasAny(TileFlag.Floor))
				{
					if (scanNum >= 2 && option.needsSameRoom)
					{
						return false;
					}
				}

				// 穴がある場合無視するかどうか
				if (tileFlag.HasAny(TileFlag.Hole))
				{
					if (!option.isHoleIgnore)
					{
						// 無視できない
						return false;
					}
				}

				// 最初の一回目は開始位置なので無視
				if (scanNum > 1)
				{
					// trueが帰ってきたら終了
					if (func(new Vector2Int(posX, posY), remCellNum))
					{
						return true;
					}
				}

				// 左、左上、上、右上、右、右下、下、左下の順番で走査する
				++scanNum;
				--remCellNum;

				return Utility.Map.CallDirection(posX, posY, 
					(posX_, posY_) => 
					{
						return ScanAllInternal(scanNum, remCellNum, posX_, posY_);
					});
			}

			ScanAllInternal(1, cellNum, srcPos.x, srcPos.y);
		}

		/// <summary>
		/// Scanが可能か
		/// </summary>
		private bool CanExecute(int x, int y, ScanOption option)
		{
			if (!_tileDataMap.InRange(x, y)) return false;

			// 壁を無視しないのに指定位置が壁なら警告を出して終了する
			if (!option.isWallIgnore)
			{
				if (_tileDataMap.HasFlag(x, y, TileFlag.Wall))
				{
					Utility.Log.Error("検索指定元のマスが壁");
					return false;
				}
			}

			return true;
		}

	}
}
