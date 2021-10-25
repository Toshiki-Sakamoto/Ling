//
// AttackAIData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.15
//

using UnityEngine;
using Utility.Attribute;

namespace Ling.MasterData.Chara
{
	/// <summary>
	/// 攻撃AIデータ
	/// </summary>
	[System.Serializable]
	public class AttackAIData
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数


		[SerializeField, FieldName("攻撃AIの種類")]
		private Const.AttackAIType _attackAIType = default;

		[SerializeField, FieldName("攻撃AIパラメータ１")]
		private string _attackAIParam1 = default;

		[SerializeField, FieldName("攻撃AIの最も優先すべきターゲット")]
		private Const.TileFlag _firstTarget = default;

		[SerializeField]
		private Skill.SkillMaster[] _skillMasters = default;

		#endregion


		#region プロパティ

		public Const.AttackAIType AttackAIType => _attackAIType;

		public string AttackAIParam1 => _attackAIParam1;

		public Const.TileFlag FirstTarget => _firstTarget;

		public Skill.SkillMaster[] SkillMasters => _skillMasters;

		public Skill.SkillMaster FirstSkillMaster => SkillMasters[0];

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
