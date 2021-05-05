//
// SkillActionData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.03
//

using UnityEngine;
using Utility.MasterData;
using Utility.Attribute;
using System.Collections.Generic;


namespace Ling.MasterData.Skill
{
	/// <summary>
	/// 一つのアクションデータ
	/// </summary>
	[System.Serializable]
	public class SkillActionEntity
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField]
		private SkillHealEntity _heal = default;

		[SerializeField]
		private SkillDamageEntity _damage = default;

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
