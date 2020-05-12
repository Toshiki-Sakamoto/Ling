//
// Tracking.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.10
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Map.Route
{
	/// <summary>
	/// 
	/// </summary>
	public class Tracking
    {
		#region 定数, class, enum

		public readonly static int[,] DirVec = new int[4, 2] { { 0, -1 }, { -1, 0 }, { 1, 0 }, { 0, 1 } };  

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private TileDataMap _tileDataMap;
		private int _width, _height;
		private int[,] _array = null;
		private bool _isForceFinish = false;
		private System.Func<Tracking, Vector2Int, int, int, int> _func;

		#endregion


		#region プロパティ

		/// <summary>
		/// 強制終了したときの最後の位置
		/// </summary>
		public Vector2Int ForceFinishPos { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(TileDataMap tileDataMap)
		{
			_tileDataMap = tileDataMap;

			_width = tileDataMap.Width;
			_height = tileDataMap.Height;

			_array = new int[_width, _height];
		}

		public void Execute(int startX, int startY, System.Func<Tracking, Vector2Int, int, int, int> func)
		{
			if (_tileDataMap == null) return;

			_isForceFinish = false;
			ForceFinishPos = Vector2Int.zero;

			for (int y = 0; y < _height; ++y)
			{
				for (int x = 0; x < _width; ++x)
				{
					_array[x, y] = 0;
				}
			}

			// 壁なら何もしない

			// 1にして実行
			_func = func;

			ExecuteInternal(new Vector2Int(startX, startY), 1);
		}

		/// <summary>
		/// 処理を終了させる
		/// </summary>
		public void ProcessFinish()
		{
			_isForceFinish = true;
		}

		/// <summary>
		/// 指定したセルポジションから走査して値が一番小さいところまでのルートを取得する
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public List<Vector2Int> GetRoute(Vector2Int startPos, bool useDiagonal)
		{
			var result = new List<Vector2Int>();

			GetRouteInternal(result, startPos, _array[startPos.x, startPos.y]);

			return result;
		}

		public void SetValue(int x, int y, int value)
		{
			if (x < 0 || y < 0 || x >= _width || y >= _height)
			{
				return;
			}

			_array[x, y] = value;
		}

		public int GetValue(int x, int y)
		{
			if (x < 0 || y < 0 || x >= _width || y >= _height)
			{
				return 0;
			}

			return _array[x, y];
		}


		#endregion


		#region private 関数

		public void ExecuteInternal(in Vector2Int dPos, int newValue)
		{
			if (dPos.x < 0 || dPos.y < 0 || dPos.x >= _width || dPos.y >= _height)
			{
				return;
			}

			newValue = newValue + 1;

			if (newValue == 15)
			{
				int i = 0;
				i = 0;
			}
			var randRange = Enumerable.Range(0, 4).ToList();

			for (int i = 0; i < 4; ++i)
			{
				// 強制終了するとき
				if (_isForceFinish)
				{
					return;
				}

				var random = Utility.Random.Range(0, randRange.Count - 1);
				var index = randRange[random];
				randRange.RemoveAt(random);

				var pos = new Vector2Int(dPos.x + DirVec[index, 0], dPos.y + DirVec[index, 1]);
				if (pos.x < 0 || pos.y < 0 || pos.x >= _width || pos.y >= _height)
				{
					continue;
				}

				var oldValue = GetValue(pos.x, pos.y);

				if (_func != null)
				{
					// 0以外なら値を書き込む
					var expValue = _func(this, pos, newValue, oldValue);

					// 強制終了時の座標
					if (_isForceFinish)
					{
						ForceFinishPos = pos;
					}

					// 0未満はスキップ
					if (expValue < 0)
					{
						continue;
					}

					if (expValue > 0)
					{
						SetValue(pos.x, pos.y, expValue);

						// 再帰処理
						ExecuteInternal(pos, expValue);
						
						continue;
					}
				}

				// 小さい値が書き込まれたら何もしない
				if (oldValue != 0 && newValue >= oldValue)
				{
					continue;
				}

				SetValue(pos.x, pos.y, newValue);

				if (_isForceFinish)
				{
					continue;
				}

				// 再帰処理
				ExecuteInternal(pos, newValue);
			}
		}



		private void GetRouteInternal(List<Vector2Int> result, in Vector2Int pos, int value)
		{
			result.Add(pos);

			// 上下左右調べて一番小さい場所に行く
			int newX = 0, newY = 0;

			bool isEnd = true;

			if (TryGetNewValue(pos.x, pos.y - 1, value, out int topValue))
			{
				newX = pos.x;
				newY = pos.y - 1;
				value = topValue;
				isEnd = false;
			}

			if (TryGetNewValue(pos.x, pos.y + 1, value, out int bottomValue))
			{
				newX = pos.x;
				newY = pos.y + 1;
				value = bottomValue;
				isEnd = false;
			}

			if (TryGetNewValue(pos.x - 1, pos.y, value, out int leftValue))
			{
				newX = pos.x - 1;
				newY = pos.y;
				value = leftValue;
				isEnd = false;
			}

			if (TryGetNewValue(pos.x + 1, pos.y, value, out int rightValue))
			{
				newX = pos.x + 1;
				newY = pos.y;
				value = rightValue;
				isEnd = false;
			}

			if (isEnd)
			{
				// 終了
				return;
			}

			GetRouteInternal(result, new Vector2Int(newX, newY), value);

			/*
			// 斜めも許可されていれば斜めも
			if (useDiagonal)
			{
				if (IsAddable(pos.x - 1, pos.y - 1, value)) return;
				if (IsAddable(pos.x + 1, pos.y - 1, value)) return;
				if (IsAddable(pos.x - 1, pos.y + 1, value)) return;
				if (IsAddable(pos.x + 1, pos.y + 1, value)) return;
			}*/
		}

		private bool TryGetNewValue(int x, int y, int value, out int newValue)
		{
			newValue = 0;

			var posValue = GetValue(x, y);
			if (posValue == 0 || posValue >= value)
			{
				return false;
			}

			newValue = posValue;
			return true;
		}

		#endregion
	}
}
