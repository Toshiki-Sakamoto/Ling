//
// HealSkillPorcess.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.04
//

namespace Ling.Chara.Skill
{
	/// <summary>
	/// ダメージスキル計算のイベント
	/// </summary>
	public interface IDamageCalculateEvent
	{
		/// <summary>
		/// 与えるダメージ量
		/// </summary>
		void SetCalcDamageValue(int damage);
	}
}
