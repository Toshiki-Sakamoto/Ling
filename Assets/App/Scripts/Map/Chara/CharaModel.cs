﻿//
// CharaModel.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.09
//

using UniRx;
using UnityEngine;
using Ling.Const;
using Ling.Common.ReactiveProperty;
using System.Collections.Generic;
using System.Linq;

namespace Ling.Chara
{
	/// <summary>
	/// <see cref="CharaManager"/>に管理されるデータ
	/// </summary>
	public abstract class CharaModel : MonoBehaviour
	{
		#region 定数, class, enum

		public class Param
		{
			public CharaType charaType;
		}

		#endregion


		#region public, protected 変数

		public EventRemove EventRemove = new EventRemove();

		#endregion


		#region private 変数

		[SerializeField] private Vector2IntReactiveProperty _cellPosition = default; // マップ上の自分の位置
		[SerializeField] private bool _isReactiveCellPosition = true;
		
		[ES3Serializable] private Param _param = null;
		private EventPosUpdate _eventPosUpdate = new EventPosUpdate();
		private Map.MapData _mapData;	// 現在自分が配置されているマップ情報
		

		#endregion


		#region プロパティ

		/// <summary>
		/// キャラクタID
		/// </summary>
		public int ID { get; }

		/// <summary>
		/// 名前
		/// </summary>
		public virtual string Name { get; }

		/// <summary>
		/// ステイタス
		/// </summary>
		[ES3Serializable]
		public CharaStatus Status { get; protected set; }

		/// <summary>
		/// 死んでいる場合true
		/// </summary>
		public bool IsDead => Status.IsDead.Value;

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
		/// セル上のインデックス値
		/// </summary>
		[ES3Serializable]
		public int CellIndex { get; protected set; }

		/// <summary>
		/// CellPositionを反映させるか
		/// </summary>
		public bool IsReactiveCellPosition => _isReactiveCellPosition;

		/// <summary>
		/// 向き
		/// </summary>
		[ES3Serializable]
		public Vector2IntReactiveProperty Dir { get; protected set; } = new Vector2IntReactiveProperty(new Vector2Int(0, -1));

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

		/// <summary>
		/// 斜め攻撃できないフラグ
		/// </summary>
		public virtual Const.TileFlag UnDiagonalAttacableTileFlag =>
			Const.TileFlag.None |
			Const.TileFlag.Wall;

		/// <summary>
		/// アクション終了後に行う追加処理のリスト
		/// </summary>
		/// <typeparam name="ICharaPostProcesser"></typeparam>
		public LinkedList<ICharaPostProcesser> PostProcessers { get; } = new LinkedList<ICharaPostProcesser>();

		/// <summary>
		/// 獲得経験値
		/// </summary>
		public abstract int Exp { get; }

		/// <summary>
		/// キャラが持つタイル上のフラグ
		/// </summary>
		public abstract Const.TileFlag TileFlag { get; }

		/// <summary>
		/// 行動思考部分を切り離し
		/// </summary>
		public CharaActionThinkCore ActionThinkCore { get; } = new CharaActionThinkCore();

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

		public void SetMapLevel(int mapLevel, Map.MapData mapData)
		{
			MapLevel = mapLevel;
			_mapData = mapData;
		}

		public void InitPos(in Vector2Int pos)
		{
			_isReactiveCellPosition = true;
			CellPosition.Value = pos;

			CellIndex = _mapData.GetIndex(pos);

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

			CellIndex = _mapData.GetIndex(pos);
		}

		/// <summary>
		/// 現在の座標に指定した分を加算する
		/// </summary>
		public void AddCellPosition(in Vector2Int pos, bool reactive) =>
			SetCellPosition(CellPosition.Value + pos, reactive);

		/// <summary>
		/// 向き情報をセットする
		/// </summary>
		public void SetDirection(in Vector2Int dir) =>
			Dir.Value = dir;

		/// <summary>
		/// ターゲットの座標から向きを設定する
		/// </summary>
		public void SetDirectionByTargetPos(in Vector2Int targetPos)
		{
			var pos = CellPosition.Value;
			var x = targetPos.x - pos.x;
			var y = targetPos.y - pos.y;
			SetDirection(new Vector2Int(x != 0 ? x / Mathf.Abs(x) : 0, y != 0 ? y / Mathf.Abs(y) : 0));
		}

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
		/// 斜め攻撃できるか
		/// </summary>
		public bool CanDiagonalAttackTileFlag(TileFlag tileFlag) =>
			!UnDiagonalAttacableTileFlag.HasAny(tileFlag);

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

		/// <summary>
		/// 行動の後に追加処理を行う対象を追加する
		/// </summary>
		public void AddPostProcess(ICharaPostProcesser postProcesser)
		{
			if (PostProcessers.Count == 0)
			{
				PostProcessers.AddFirst(postProcesser);
				return;
			}

			var node = PostProcessers.First;
			while (node != null)
			{
				if (postProcesser.Order < node.Value.Order) break;

				node = node.Next;
			}

			PostProcessers.AddBefore(node, postProcesser);
		}
		public void RemovePostProcess(ICharaPostProcesser postProcesser)
		{
			PostProcessers.Remove(postProcesser);
		}

		/// <summary>
		/// セーブデータをロードされた時の情報をセットする
		/// </summary>
		public void ApplySaveData(CharaModel model)
		{
			_cellPosition.Value = model._cellPosition.Value;
		}

		/// <summary>
		/// 自分から見たターゲットにあったCharaTypeを返す
		/// </summary>
		public Chara.CharaType ConvertTargetCharaType(Const.TargetType targetType)
		{
			switch (targetType)
			{
				case Const.TargetType.Ally:
					return CharaType;

				case Const.TargetType.Enemy:
					if (CharaType == Chara.CharaType.Player) return Chara.CharaType.Enemy;
					if (CharaType == Chara.CharaType.Enemy) return Chara.CharaType.Player;
					break;
			}

			throw new System.ArgumentException("");
		}

		#endregion


		#region private 関数

		#endregion
	}
}
