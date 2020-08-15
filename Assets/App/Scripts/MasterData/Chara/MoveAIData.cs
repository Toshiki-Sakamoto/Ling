//
// MoveAIData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.15
//

using UnityEngine;
using Ling.Utility.Attribute;

namespace Ling.MasterData.Chara
{
	using MoveAI = Ling.AI.Move;

	/// <summary>
	/// 移動AIデータ
	/// </summary>
	public class MoveAIData
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("移動AIの種類")]
		private Const.MoveAIType _moveAIType = default;

		[SerializeField, FieldName("移動AIパラメータ１")]
		private int _moveAIParam1 = default;

		[SerializeField, FieldName("移動AIの最も優先すべきターゲット")]
		private Const.MoveAITarget _firstMoveTarget = default;

		[SerializeField, FieldName("移動AIの二番目に優先すべきターゲット")]
		private Const.MoveAITarget _secondMoveTarget = default;

		#endregion


		#region プロパティ

		public Const.MoveAIType MoveAIType => _moveAIType;

		public int MoveAIParam1 => _moveAIParam1;

		private Const.MoveAITarget FirstMoveTarget => _firstMoveTarget;

		private Const.MoveAITarget SecondMoveTarget => _secondMoveTarget;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public MoveAI.MoveAIFactory CreateMoveAIFactory() =>
			new MoveAI.MoveAIFactory(this);

		#endregion


		#region private 関数

		#endregion
	}
}
