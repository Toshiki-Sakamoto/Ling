//
// SkillHealEntity.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.03
//

using UnityEngine;
using Utility.Attribute;
using Sirenix.OdinInspector;

namespace Ling.MasterData.Skill
{
	/// <summary>
	/// 演出に必要なファイル
	/// </summary>
	[System.Serializable, InlineProperty]
	public class SkillEffectEntity
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, LabelText("範囲")]
		private RangeType _rangeType;

		[SerializeField, LabelText("スピード係数")]
		private float _speed = 1f;

		[SerializeField, LabelText("ターゲット")]
		private TargetType _targetType;

		#endregion


		#region プロパティ

		public RangeType Range => _rangeType;
		public float Speed => _speed;
		public TargetType Target => _targetType;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
