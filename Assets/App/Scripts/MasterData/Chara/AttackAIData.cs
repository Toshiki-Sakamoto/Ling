//
// AttackAIData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.15
//

using UnityEngine;
using Ling.Utility.Attribute;

namespace Ling.MasterData.Chara
{
	using AttackAI = Ling.AI.Attack;

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

		#endregion


		#region プロパティ

		public Const.AttackAIType AttackAIType => _attackAIType;

		public string AttackAIParam1 => _attackAIParam1;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public AttackAI.AttackAIFactory CreateFactory() =>
			new AttackAI.AttackAIFactory(this);
			
		#endregion


		#region private 関数

		#endregion
	}
}
