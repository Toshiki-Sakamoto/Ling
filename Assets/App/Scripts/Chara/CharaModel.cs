//
// CharaModel.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.09
//

using UniRx;
using UnityEngine;
using Ling.Const;
using Ling.Common.ReactiveProperty;

namespace Ling.Chara
{
	/// <summary>
	/// <see cref="CharaManager"/>に管理されるデータ
	/// </summary>
	public class CharaModel : MonoBehaviour
    {
		#region 定数, class, enum

		public class Param
		{
			public CharaType charaType;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Param _param = null;
        private EventPosUpdate _eventPosUpdate = new EventPosUpdate();
        [SerializeField] private Vector2IntReactiveProperty _cellPosition = default; // マップ上の自分の位置
		[SerializeField] private bool _isReactiveCellPosition = true;

		#endregion


		#region プロパティ

		/// <summary>
		/// ステイタス
		/// </summary>
		public CharaStatus Status { get; private set; }

		/// <summary>
		/// キャラの種類
		/// </summary>
		public CharaType CharaType => _param.charaType;

		/// <summary>
		/// マップ階層
		/// </summary>
		public int MapLevel { get; private set; }
		
		/// <summary>
		/// 現在のセル上の座標
		/// </summary>
		public Vector2IntReactiveProperty CellPosition => _cellPosition;

		/// <summary>
		/// CellPositionを反映させるか
		/// </summary>
		public bool IsReactiveCellPosition => _isReactiveCellPosition;

		/// <summary>
		/// 向き
		/// </summary>
		public Vector2ReactiveProperty Dir { get; private set; } = new Vector2ReactiveProperty(new Vector2(0f, -1f));

		/// <summary>
		/// 攻撃AI
		/// </summary>
		public AI.Attack.AIBase AttackAI { get; private set; }

		/// <summary>
		/// 移動AI
		/// </summary>
		public AI.Move.AIBase MoveAI { get; private set; }

        /// <summary>
        /// 移動することができないタイルフラグ。
        /// これ以外は移動できるとする
        /// </summary>
        public virtual Const.TileFlag UnmovableTileFlag =>
            Const.TileFlag.None | 
			Const.TileFlag.Wall | 
			Const.TileFlag.Hole | 
			Const.TileFlag.Chara;

		/// <summary>
		/// 斜め移動できないフラグ
		/// </summary>
		public virtual Const.TileFlag UnDiagonalMovableTileFlag =>
			Const.TileFlag.None |
			Const.TileFlag.Wall;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(Param param)
		{
			_param = param;
		}

		/// <summary>
		/// ステイタスを設定
		/// </summary>
		public void SetStatus(MasterData.Chara.StatusData masterStatus)
		{
			SetStatus(new CharaStatus(masterStatus));
		}
		public void SetStatus(CharaStatus status)
		{
			Status = status;
		}

		/// <summary>
		/// AI設定
		/// </summary>
		public void SetMoveAI(AI.Move.AIBase moveAI) =>
			MoveAI = moveAI;

		public void SetAttackAI(AI.Attack.AIBase attackAI) =>
			AttackAI = attackAI;

		public void SetMapLevel(int mapLevel) 
		{
			MapLevel = mapLevel;
		}

		public void InitPos(in Vector2Int pos)
		{
			_isReactiveCellPosition = true;
			CellPosition.Value = pos;

			_eventPosUpdate.prevPos = null;
            _eventPosUpdate.newPos = pos;
            _eventPosUpdate.charaType = CharaType;
            _eventPosUpdate.mapLevel = MapLevel;
			
            // 移動したことのイベントを発行する
			Utility.EventManager.SafeTrigger(_eventPosUpdate);
		}

		/// <summary>
		/// 座標の設定
		/// </summary>
		public void SetCellPosition(in Vector2Int pos, bool reactive, bool sendEvent = true)
        {
			if (sendEvent)
			{
				_eventPosUpdate.prevPos = CellPosition.Value;
				_eventPosUpdate.newPos = pos;
				_eventPosUpdate.charaType = CharaType;
				_eventPosUpdate.mapLevel = MapLevel;

				// 移動したことのイベントを発行する
				Utility.EventManager.SafeTrigger(_eventPosUpdate);
			}

			_isReactiveCellPosition = reactive;
			_cellPosition.SetValueAndForceNotify(pos);
        }

		/// <summary>
		/// 現在の座標に指定した分を加算する
		/// </summary>
		public void AddCellPosition(in Vector2Int pos, bool reactive) =>
			SetCellPosition(CellPosition.Value + pos, reactive);

		/// <summary>
		/// 向き情報をセットする
		/// </summary>
		public void SetDirection(in Vector2 dir) =>
			Dir.Value = dir;

		/// <summary>
		/// 移動できるかどうか
		/// 一つでも引っかかった場合は移動できない
		/// </summary>
		public bool CanMoveTileFlag(TileFlag tileFlag) =>
			!UnmovableTileFlag.HasAny(tileFlag);

		/// <summary>
		/// 移動できない場所か
		/// </summary>
		public bool CanNotMoveTileFlag(TileFlag tileFlag) =>
			UnmovableTileFlag.HasAny(tileFlag);

		/// <summary>
		/// 斜め移動できるか
		/// </summary>
		public bool CanDiagonalMoveTileFlag(TileFlag tileFlag) =>
			!UnDiagonalMovableTileFlag.HasAny(tileFlag);

		/// <summary>
		/// コストを取得
		/// </summary>
		public int GetCostTileFlag(TileFlag tileFlag)
		{
			// キャラクタがいるところはコストを高くする
			if (TileFlag.Chara.HasAny(tileFlag))
			{
				return 3;
			}

			// 移動できない場所はマイナスで返す
			if (!CanMoveTileFlag(tileFlag))
			{
				return -1;
			}

			return 1;
		}
			
		#endregion


		#region private 関数

		#endregion
	}
}
