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

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		/// <summary>
		/// ステイタス
		/// </summary>
		public CharaStatus Status { get; private set; }

		/// <summary>
		/// 現在座標
		/// </summary>
		public Vector2Int Pos { get; private set; }

		/// <summary>
		/// 攻撃AI
		/// </summary>
		public AttackAI.AIBase AttackAI { get; private set; }

		/// <summary>
		/// 移動AI
		/// </summary>
		public MoveAI.AIBase MoveAI { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// ステイタスを生成する
		/// </summary>
		public void Setup(MasterData.Chara.StatusData masterStatus)
		{
			Setup(new CharaStatus(masterStatus));
		}

		public void Setup(CharaStatus status)
		{
			Status = status;
		}

		/// <summary>
		/// AIを決定する
		/// </summary>
		public void SetAI(AttackAI.AIBase attackAI, MoveAI.AIBase moveAI)
		{
			AttackAI = attackAI;
			MoveAI = moveAI;
		}

		public void SetPos(in Vector2Int pos) =>
			Pos = pos;

		#endregion


		#region private 関数

		#endregion
	}
}
