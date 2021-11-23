﻿//
// ISkillCalculater.cs
// ProductName Ling
//
// Created by Toshiki Sakamoto on 2021.11.14
//

using Cysharp.Threading.Tasks;
using Ling.MasterData.Skill;
using Unity.VisualScripting;
using UnityEngine;

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
	public class SkillCalculater : MonoBehaviour, ISkillCalculater, IDamageCalculateEvent
	{
		private bool _isFinish;
		private int _value;


		async UniTask<int> ISkillCalculater.ExecuteAsync(ICharaController unit, ICharaController target, SkillDamageEntity entity)
		{
			// 呼び出し
			CustomEvent.Trigger(gameObject, "Execute", this, entity, unit, target);

			// 終了するまで待機する
			if (!_isFinish)
			{
				await UniTask.WaitUntil(() => _isFinish);
			}

			return _value;
		}

		void IDamageCalculateEvent.SetCalcDamageValue(int value)
		{
			_value = value;

			_isFinish = true;
		}
	}
}