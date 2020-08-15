﻿//
// AIBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.26
//

using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Ling.AI.Move
{
	using CharaMaster = Ling.MasterData.Chara;

	/// <summary>
	/// 移動AIのベースクラス
	/// </summary>
	public abstract class AIBase
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private CharaMaster.MoveAIData _masterAIData;
		private Vector2Int? _destination;	// 目的地

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
			_destination = null;
		}

		public virtual async UniTask ExecuteAsync(Chara.ICharaController unit)
		{
			// もっとも優先すべきものがあればそこに向かって歩く

			// 目的地があればそこに向かって歩く
			if (_destination != null)
			{

			}

			// 目的地がなければ
		}

		#endregion


		#region private 関数

		protected virtual void Route()
		{

		}

		/// <summary>
		/// 最も優先すべきターゲット
		/// </summary>
		protected virtual void MustTarget()
		{

		}

		#endregion
	}
}
