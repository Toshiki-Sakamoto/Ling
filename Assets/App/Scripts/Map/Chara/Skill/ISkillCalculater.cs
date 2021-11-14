//
// ISkillCalculater.cs
// ProductName Ling
//
// Created by Toshiki Sakamoto on 2021.11.14
//

using Cysharp.Threading.Tasks;
using Ling.MasterData.Skill;

namespace Ling.Chara.Skill
{
	/// <summary>
	/// スキル計算について持つ
	/// </summary>
	public interface ISkillCalculater
	{
		/// <summary>
        /// スキルダメージを計算する
        /// </summary>
		UniTask<int> ExecuteAsync(ICharaController unit, ICharaController target, SkillDamageEntity entity);
	}


	/// <summary>
	/// スキル計算について持つ
	/// </summary>
	public class SkillCalculater : ISkillCalculater, IDamageCalculateEvent
	{
		private bool _isFinish;
		private int _value;


		async UniTask<int> ISkillCalculater.ExecuteAsync(ICharaController unit, ICharaController target, SkillDamageEntity entity)
		{
			// 終了するまで待機する
			await UniTask.WaitWhile(() => _isFinish);

			return _value;
		}

		void IDamageCalculateEvent.SetCalcDamageValue(int value)
		{
			_value = value;

			_isFinish = true;
		}
	}
}
