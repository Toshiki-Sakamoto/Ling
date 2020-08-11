//
// AttackAIFactory.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.10
//

namespace Ling.Chara.AttackAI
{
	/// <summary>
	/// 攻撃AIファクトリ
	/// </summary>
	public class AttackAIFactory
    {
		private Const.AttackAIType _attackAIType;
		private string _param1;

		public AttackAIFactory(Const.AttackAIType attackAIType, string param1)
		{
			_attackAIType = attackAIType;
			_param1 = param1;
		}

		public AIBase Create()
		{			
			AIBase attackAI = null;

			switch (_attackAIType)
			{
				case Const.AttackAIType.Normal:
					attackAI = new AINormalAttack();
					break;

				default:
					Utility.Log.Error("AttackAIを作成できませんでした。無効のタイプ " + _attackAIType);
					return null;
			}

			return attackAI;
		}
	}
}
