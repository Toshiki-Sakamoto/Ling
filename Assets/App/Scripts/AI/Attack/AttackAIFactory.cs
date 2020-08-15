//
// AttackAIFactory.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.10
//

using Ling;
using Ling.MasterData.Chara;

namespace Ling.AI.Attack
{
	/// <summary>
	/// 攻撃AIファクトリ
	/// </summary>
	public class AttackAIFactory
    {
		private AttackAIData _attackAIData;

		public AttackAIFactory(AttackAIData attackAIData)
		{
			_attackAIData = attackAIData;
		}

		public AIBase Create()
		{			
			AIBase attackAI = null;

			switch (_attackAIData.AttackAIType)
			{
				case Const.AttackAIType.Normal:
					attackAI = new AINormalAttack();
					break;

				default:
					Utility.Log.Error("AttackAIを作成できませんでした。無効のタイプ " + _attackAIData.AttackAIType);
					return null;
			}

			return attackAI;
		}
	}
}
