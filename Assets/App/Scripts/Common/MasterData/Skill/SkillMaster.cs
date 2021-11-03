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

#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;
#endif

namespace Ling.MasterData.Skill
{
	/// <summary>
	/// スキル情報マスタ
	/// </summary>
	[CreateAssetMenu(menuName = "MasterData/SkillMaster", fileName = "SkillMaster")]
	public class SkillMaster : MasterDataBase
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

		[SerializeField, LabelText("演出"), InlineProperty]
		private SkillEffectEntity _effect;

		[SerializeField, ToggleGroup("_enableHeal", "回復")]
		private bool _enableHeal = false;

		[SerializeField, ToggleGroup("_enableHeal")]
		private SkillHealEntity _heal = default;

		[SerializeField, ToggleGroup("_enableDamage", "ダメージ")]
		private bool _enableDamage = false;

		[SerializeField, ToggleGroup("_enableDamage")]
		private SkillDamageEntity _damage = default;

		#endregion


		#region プロパティ

		public string Filename => _filename;
		public int HitMin => _hitMin;
		public int HitMax => _hitMax;
		public SkillEffectEntity Effect => _effect;
		public SkillHealEntity Heal => _enableHeal ? _heal : null;
		public SkillDamageEntity Damage => _enableDamage ? _damage : null;

		public Const.EffectType EffectType
		{
			get
			{
				if (_enableDamage) return Const.EffectType.Damage;
				if (_enableDamage) return Const.EffectType.Heal;

				return Const.EffectType.None;
			}
		}

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion



	}
}
