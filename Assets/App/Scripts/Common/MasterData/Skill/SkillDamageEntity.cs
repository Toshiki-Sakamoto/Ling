//
// SkillDamageEntity.cs
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
	/// よくあるダメージ
	/// </summary>
	[System.Serializable, InlineProperty]
	public class SkillDamageEntity
	{
		#region 定数, class, enum
		
		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, LabelText("威力")]
		private int _power;

		[SerializeField, LabelText("固定ダメージ")]
		private bool _fixedDamage;

		[SerializeField, LabelText("範囲")]
		private RangeType _rangeType;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public int Power => _power;
		public bool FixedDamage => _fixedDamage;
		public RangeType Range => _rangeType;

		#endregion


		#region private 関数

		#endregion
	}
}
