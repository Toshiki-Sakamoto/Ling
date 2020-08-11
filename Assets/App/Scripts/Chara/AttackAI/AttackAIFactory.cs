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
		private int _param1;

		public AttackAIFactory(Const.AttackAIType attackAIType, int param1)
		{
			_attackAIType = attackAIType;
			_param1 = param1;
		}

		public AIBase Create()
		{
			return null;
		}
	}
}
