//
// AIBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.26
//

using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using Ling.Map.TileDataMapExtensions;
using Ling.Map.TileDataMapExtensionss.Chara;
using System.Collections.Generic;
using Ling.Const;
using Ling.Utility.Extensions;

namespace Ling.AI.Move
{
	using CharaMaster = Ling.MasterData.Chara;

	/// <summary>
	/// 移動AIのベースクラス
	/// </summary>
	public abstract class AIBase : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Map.MapManager _mapManager = default;
		[Inject] private Chara.CharaManager _charaManager = default;

		private CharaMaster.MoveAIData _masterAIData;
		private Vector2Int? _destination;	// 目的地
		private List<Vector2Int> _destinationRoutes;
		private Vector2Int? _nextMovePos;	// 次に移動する座標
		private Chara.ICharaController _unit;
		private Map.TileDataMap _tileDataMap;
		private Map.RoomData _roomData;
		private Vector2Int? _prevPos;	// 一つ前にいた座標
		private int _waitCount;	// 何回動けずに待機したか

		#endregion


		#region プロパティ

		/// <summary>
		/// AIの種類
		/// </summary>
		public abstract Const.MoveAIType AIType { get; }

		/// <summary>
		/// 汎用パラメータ
		/// </summary>
		public int Param1 { get; set; }

		public Map.TileDataMap TileDataMap
		{
			get 
			{
				if (_tileDataMap != null) return _tileDataMap;

				_tileDataMap = _mapManager.MapControl.FindTileDataMap(_unit.Model.MapLevel);
				return _tileDataMap;
			}
		}

		public Map.RoomData RoomData
		{
			get
			{
				if (_roomData != null) return _roomData;

				_roomData = TileDataMap.FindRoomData(_unit);
				return _roomData;
			}
		}

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(CharaMaster.MoveAIData moveAIData)
		{
			_masterAIData = moveAIData;
		}

		/// <summary>
		/// 保存してあるルートをリセットする
		/// </summary>
		public void ResetDestination()
		{
			_waitCount = 0;
			_destination = null;
			_destinationRoutes?.Clear();
		}

		public virtual async UniTask ExecuteAsync(Chara.ICharaController unit, Ling.Utility.Async.WorkTimeAwaiter timeAwaiter)
		{
			_unit = unit;
			_tileDataMap = null;
			_roomData = null;

			await ExexuteInternalAsync(timeAwaiter);
		}

		#endregion


		#region private 関数

		protected virtual async UniTask ExexuteInternalAsync(Ling.Utility.Async.WorkTimeAwaiter timeAwaiter)
		{
			// もっとも優先すべきものがあればそこに向かって歩く
			if (await SearchMustDestinationAsync())
			{
				await SearchNextMovePosAsync();
				return;
			}

			await timeAwaiter.Wait();

			// 目的地が設定されていればそこに向かう
			do
			{
				if (_destination == null) break;
					
				// すでに目的地にいる場合は何もしない
				if (_destination == _unit.Model.Pos)
				{
					ResetDestination();
					break;
				}

				// 目的地が見つかったら終了
				if (await SearchNextMovePosAsync())
				{
					return;
				}

				if (_waitCount >= 2)
				{
					// 2回以上失敗している場合は目的地をリセットする
					ResetDestination();

					// 前回いた位置もリセットする
					_prevPos = null;
				}

			} while (false);

			await timeAwaiter.Wait();

			// 目的地がなければ現在自分が動ける範囲で目的地を探す
			if (SearchDestination())
			{
				await SearchNextMovePosAsync();
			}
		}

		/// <summary>
		/// 次に移動すべき座標を目的地から計算する
		/// </summary>
		protected async UniTask<bool> SearchNextMovePosAsync()
		{
			_nextMovePos = null;

			// 目的地がない場合は動けない
			if (_destination == null)
			{
				return false;
			}

			// 次の場所に移動する処理
			bool ProcessSetNextMovePos(in Vector2Int pos)
			{
				// 移動できない場合は現在地に待機
				var tileData = _tileDataMap.GetTileData(pos.x, pos.y);
				if (_unit.Model.CanNotMoveTileFlag(tileData.Flag))
				{
					///////++ _waitCount;
				}
				else
				{
					SetNextMovePos(pos);
				}
				
				return true;
			}

			// ルートがすでに存在する場合は使用する
			if (!_destinationRoutes.IsNullOrEmpty())
			{
				var pos = _destinationRoutes.Front();
				_destinationRoutes.Clear();
				
				return ProcessSetNextMovePos(pos);
			}

			// 目的地から最短距離を求める
			var result = await _tileDataMap.Scanner.GetShotestDisancePositionAsync(_unit, _destination.Value);
			if (result != null)
			{
				return ProcessSetNextMovePos(result.routePositions.Front());
			}

			// 移動できなかった
			++_waitCount;
			return false;
		}


		/// <summary>
		/// 優先すべきターゲット座標取得する
		/// </summary>
		protected virtual async UniTask<bool> SearchMustDestinationAsync()
		{
			async UniTask<bool> Process(Const.TileFlag target)
			{
				if (_masterAIData.FirstTarget == Const.TileFlag.None)
				{
					return false;
				}

				var result = await SearchPosByTargetTypeAsync(_masterAIData.FirstTarget);
				if (result)
				{
					// 見つかった
					_waitCount = 0;
					return true;
				}

				return false;
			}

			if (await Process(_masterAIData.FirstTarget)) 
			{
				return true;
			}
			
			if (await Process(_masterAIData.SecondTarget)) 
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// ターゲット座標を取得する
		/// </summary>
		protected virtual async UniTask<bool> SearchPosByTargetTypeAsync(Const.TileFlag targetType)
		{
			// 部屋である必要があるか →　継承先で特別なことはやる

			// 部屋か

			// 同じ部屋にターゲットがいるかどうか
			if (RoomData == null)
			{
				// 部屋じゃない
				return false;
			}

			// 同じ部屋にターゲットタイプがいないので終了
			if (!RoomData.TryGetTilePositionList(targetType, out var targetPositions))
			{
				return false;
			}

			// 存在する場合、もっとも自分から距離が近い対象座標を取得する
			_destinationRoutes = null;

			var shotestDistanceResult = await TileDataMap.Scanner.GetShotestDistancePositionsAsync(_unit, targetPositions);
			if (shotestDistanceResult == null)
			{
				return false;
			}

			_destinationRoutes = shotestDistanceResult.routePositions;
			_destination = shotestDistanceResult.targetPos;

			return true;
		}

		/// <summary>
		/// 次の目的地の座標を取得する
		/// </summary>
		/// <returns></returns>
		protected virtual bool SearchDestination()
		{
			if (RoomData != null)
			{
				// 部屋
				if (TryGetRoomExitPosition(out var targetPos))
				{
					_destination = targetPos;
					return true;
				}
			}
			else
			{
				// 部屋以外
				if (TryGetNextMovePositionOutRoom(out var targetPos))
				{
					_destination = targetPos;
					return true;
				}
			}

			// どこにもいけない場合は現在の座標に待機する
			return false;
		}

		/// <summary>
		/// 部屋にいるとき、出口の座標を取得する
		/// </summary>
		protected bool TryGetRoomExitPosition(out Vector2Int targetPos)
		{
			targetPos = Vector2Int.zero;

			// 部屋にいない場合何もしない
			if (RoomData == null) return false;

			// 出口を目的地とする
			
			// 自分の周りに出口があるならそこに行く
			if (TryGetNextMoveAroundPosition(out targetPos, TileFlag.Road))
			{
				return true;
			}

			// 部屋に入ったばかりのときは以前入ってきた通路以外の場所を探す。
			// なければ戻る
			var exitPositions = RoomData.ExitPositions;
			if (exitPositions.Count > 0)
			{
				if (_prevPos != null)
				{
					if (exitPositions.TryGetRandomWithoutValue(_prevPos.Value, out targetPos))
					{
						return true;
					}
				}
				else
				{
					targetPos = exitPositions.GetRandom();
					return true;
				}
			}
			
			// 出口がない場合は現在の部屋ランダム座標を目的地にする
			targetPos = RoomData.GetRandom().Pos;
			return true;
		}

		/// <summary>
		/// 自分の周りの座標で行ける場所を探す
		/// </summary>
		protected bool TryGetNextMoveAroundPosition(out Vector2Int targetPos, TileFlag targetTileFlag)
		{
			targetPos = Vector2Int.zero;

			// 現在の座標の周りを調べ行ける場所を目的地とする
			var pos = _unit.Model.Pos;
			var dirArray = Ling.Utility.Map.GetDirArray(true);
			for (int i = 0, size = dirArray.GetLength(0); i < size; ++i)
			{
				var addX = dirArray[i, 0];
				var addY = dirArray[i, 1];

				var nextX = pos.x + addX;
				var nextY = pos.y + addY;

				if (!_tileDataMap.InRange(nextX, nextY)) continue;
				
				// 以前の座標と同じ場合は移動できない
				if (_prevPos != null)
				{
					var prevPos = _prevPos.Value;
					if (prevPos.x == nextX && prevPos.y == nextY) continue;
				}

				var tileFlag = _tileDataMap.GetTileFlag(nextX, nextY);

				// 指定したTileFlagか
				if (targetTileFlag != TileFlag.None)
				{
					if (tileFlag != targetTileFlag)
					{
						continue;
					}
				}

				// 移動できない場合は終了
				if (!_unit.Model.CanMoveTileFlag(tileFlag)) continue;

				// 斜めの場合は障害物を貫通できるかどうか確認
				if (addX != 0 && addY != 0)
				{
					// どちらかに障害物があるか
					var tmp = TileFlag.None;
					tmp |= _tileDataMap.GetTileFlag(pos.x + addX, pos.y);
					tmp |= _tileDataMap.GetTileFlag(pos.x, pos.y + addY);

					// その障害物が通れるか
					if (!_unit.Model.CanDiagonalMoveTileFlag(tmp))
					{
						// 通れないので失敗
						continue;
					}
				}

				targetPos = new Vector2Int(nextX, nextY);

				return true;
			}

			return false;
		}

		/// <summary>
		/// 部屋の外で周りに行ける座標を検索する
		/// </summary>
		protected bool TryGetNextMovePositionOutRoom(out Vector2Int targetPos)
		{
			targetPos = Vector2Int.zero;

			// 部屋の場合だめ
			if (RoomData != null) return false;

			return TryGetNextMoveAroundPosition(out targetPos, TileFlag.None);
		}


		/// <summary>
		/// 次移動するマスの座標
		/// </summary>
		protected void SetNextMovePos(in Vector2Int movePos)
		{
			_nextMovePos = movePos;
			_waitCount = 0;

			// 移動したことをキャラに伝え、アニメーションも設定させる
			_prevPos = _unit.Model.Pos;	// 以前の座標を保持しておく
			_unit.Model.SetPos(movePos);

			// 移動プロセスの設定
			var process = _unit.AddMoveProcess<Chara.Process.ProcessMove>();
			process.SetPos(_unit.MoveController, _prevPos.Value, movePos);
		}

		#endregion
	}
}
