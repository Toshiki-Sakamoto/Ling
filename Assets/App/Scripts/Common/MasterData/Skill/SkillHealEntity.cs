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
	/// 回復
	/// </summary>
	[System.Serializable, InlineProperty]
	public class SkillHealEntity
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("HP")]
		private long _hp = default;

		[SerializeField, FieldName("スタミナ")]
		private long _stamina = default;

		#endregion


		#region プロパティ

		public long HP => _hp;
		public long Stamina => _stamina;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
