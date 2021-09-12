//
// CharaAttackCalculator.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.09
//

using System;

namespace Ling.Chara.Calculator
{
	/// <summary>
	/// スキルに関する計算関連
	/// </summary>
	public static class CharaSkillCalculator
	{
		/// <summary>
		/// unitがopponentに与えるダメージを計算して返す
		/// </summary>
		public static long Calculate(ICharaController unit, ICharaController opponent)
		{
			// 今は単純に引くか
			return Math.Max(unit.Status.TotalAttack - opponent.Status.TotalDefence, 0);
		}
	}
}
