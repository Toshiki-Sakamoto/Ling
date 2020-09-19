//
// AINormalAttack.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.11
//

using Cysharp.Threading.Tasks;
using UnityEngine;
using Ling.Const;
using Ling.Map.TileDataMapExtensions;

namespace Ling.AI.Attack
{
	/// <summary>
	/// 通常攻撃しかしない。
	/// 隣のマスにPlayerがいたら攻撃するだけ
	/// </summary>
	public class AINormalAttack : AIBase
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

		protected override async UniTask ExexuteInternalAsync(Ling.Utility.Async.WorkTimeAwaiter timeAwaiter) 
		{
			// 自分の８方向のマスにターゲットが存在するか
			// なければもう何もしない
			if (TryGetAttackTargetPosition(_masterAIData.FirstTarget, out var targetPos))
			{
				SetAttackProcess(targetPos);
				CanActable = true;
			}
		}

		/// <summary>
		/// 自分の周りの座標で攻撃できる場所を探す
		/// </summary>
		protected bool TryGetAttackTargetPosition(TileFlag targetTileFlag, out Vector2Int targetPos)
		{
			targetPos = Vector2Int.zero;

			// 現在の座標の周りを調べ行ける場所を目的地とする
			var pos = _unit.Model.CellPosition.Value;
			var dirArray = Ling.Utility.Map.GetDirArray(true);
			for (int i = 0, size = dirArray.GetLength(0); i < size; ++i)
			{
				var addX = dirArray[i, 0];
				var addY = dirArray[i, 1];

				var nextX = pos.x + addX;
				var nextY = pos.y + addY;

				if (!TileDataMap.InRange(nextX, nextY)) continue;
				
				var tileFlag = TileDataMap.GetTileFlag(nextX, nextY);

				// 指定したTileFlagか
				if (!tileFlag.HasAny(targetTileFlag))
				{
					continue;
				}

				// 斜めの場合は障害物を貫通できるかどうか確認
				if (addX != 0 && addY != 0)
				{
					// どちらかに障害物があるか
					var tmp = TileFlag.None;
					tmp |= _tileDataMap.GetTileFlag(pos.x + addX, pos.y);
					tmp |= _tileDataMap.GetTileFlag(pos.x, pos.y + addY);

					// その障害物を越えて攻撃できるか
					if (!_unit.Model.CanDiagonalAttackTileFlag(tmp))
					{
						// 攻撃できないので失敗
						continue;
					}
				}

				targetPos = new Vector2Int(nextX, nextY);

				return true;
			}

			return false;
		}

		/// <summary>
		/// 次移動するマスの座標
		/// </summary>
		protected void SetAttackProcess(in Vector2Int targetPos)
		{
			// 移動プロセスの設定
			var process = _unit.AddAttackProcess<Chara.Process.ProcessAttack>();
			process.SetChara(_unit, ignoreIfNoTarget: false);
			process.SetTargetPos(targetPos);
		}

		#endregion


		#region private 関数
		

		#endregion
	}
}
