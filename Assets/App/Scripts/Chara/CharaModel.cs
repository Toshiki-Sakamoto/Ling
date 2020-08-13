//
// CharaModel.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.09
//

using Ling.MasterData.Chara;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Ling.Chara
{
	/// <summary>
	/// <see cref="CharaManager"/>に管理されるデータ
	/// </summary>
	public class CharaModel
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
		/// 現在座標
		/// </summary>
		public Vector2Int Pos { get; private set; }

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
        public virtual Map.TileFlag CanNotMoveTileFlag =>
            Map.TileFlag.None | Map.TileFlag.Wall;

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
		/// AIを決定する
		/// </summary>
		public void SetAI(AI.Attack.AIBase attackAI, AI.Move.AIBase moveAI)
		{
			AttackAI = attackAI;
			MoveAI = moveAI;
		}

		public void SetMapLevel(int mapLevel) 
		{
			MapLevel = mapLevel;
		}

		/// <summary>
		/// 座標の設定
		/// </summary>
		public void SetPos(in Vector2Int pos)
        {
            _eventPosUpdate.prevPos = Pos;
            _eventPosUpdate.newPos = pos;
            _eventPosUpdate.charaType = CharaType;
            _eventPosUpdate.mapLevel = MapLevel;

            Pos = pos;

            // 移動したことのイベントを発行する
			Utility.EventManager.SafeTrigger(_eventPosUpdate);
        }

		/// <summary>
		/// 現在の座標に指定した分を加算する
		/// </summary>
		public void AddPos(in Vector2Int pos) =>
			SetPos(Pos + pos);

		#endregion


		#region private 関数

		#endregion
	}
}
