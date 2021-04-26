//
// MoveAIData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.15
//

using UnityEngine;
using Utility.Attribute;

namespace Ling.MasterData.Chara
{
	/// <summary>
	/// 移動AIデータ
	/// </summary>
	[System.Serializable]
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

		[SerializeField, FieldName("穴を無視する(浮遊系)")]
		private bool _isHoleIgnore = default;

		[SerializeField, FieldName("移動AIの最も優先すべきターゲット")]
		private Const.TileFlag _firstTarget = default;

		[SerializeField, FieldName("移動AIの二番目に優先すべきターゲット")]
		private Const.TileFlag _secondTarget = default;

		#endregion


		#region プロパティ

		public Const.MoveAIType MoveAIType => _moveAIType;

		public int MoveAIParam1 => _moveAIParam1;

		public bool IsHoleIgnore => _isHoleIgnore;

		public Const.TileFlag FirstTarget => _firstTarget;

		public Const.TileFlag SecondTarget => _secondTarget;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
