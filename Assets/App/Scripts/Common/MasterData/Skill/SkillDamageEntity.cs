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

		[SerializeField, FieldName("威力")]
		private int _power;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
