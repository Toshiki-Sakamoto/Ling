//
// ScanExtensions.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.13
//

using UnityEngine;
using System.Collections.Generic;

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

	/// <summary>
	/// マップを走査する
	/// </summary>
	public class TileDataMapScanner
    {
		private TileDataMap _tileDataMap;

		public TileDataMapScanner(TileDataMap tileDataMap)
		{
			_tileDataMap = tileDataMap;
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

		public bool GetRouteAtTileFlag()
		{

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

				return MapUtility.CallDirection(posX, posY, 
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
