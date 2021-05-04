//
// SkillMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.03
//

using UnityEngine;
using Utility.MasterData;
using Utility.Attribute;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Ling.MasterData.Skill
{
	/// <summary>
	/// スキル情報マスタ
	/// </summary>
	[System.Serializable]
	public class SkillMaster
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("詠唱演出ファイル名")]
		private string _filename = default;

		[SerializeField, FieldName("ヒット回数(最小)")]
		private int _hitMin = 1;

		[SerializeField, FieldName("ヒット回数(最大)")]
		private int _hitMax = 1;

		[SerializeField, FieldName("回復")]
		private bool _enableHeal = false;

		[SerializeField, HideLabel, ShowIf("_enableHeal")]
		private SkillHealEntity _heal = default;

		[SerializeField, FieldName("ダメージ")]
		private bool _enableDamage = false;

		[SerializeField, HideLabel, ShowIf("_enableDamage")]
		private SkillDamageEntity _damage = default;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public SkillHealEntity Heal => _enableHeal ? _heal : null;
		public SkillDamageEntity Damage => _enableDamage ? _damage : null;

		#endregion


		#region private 関数

		#endregion
	}
}
