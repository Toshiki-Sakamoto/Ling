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

		public void Attach<TModel, TView>(Chara.CharaControl<TModel, TView> charaControl)
			where TModel : Chara.CharaModel 
			where TView : Chara.ViewBase
		{			
			AIBase attackAI = null;

			switch (_attackAIData.AttackAIType)
			{
				case Const.AttackAIType.Normal:
					attackAI = charaControl.AttachAttackAI<AINormalAttack>();
					break;

				default:
					Ling.Utility.Log.Error("AttackAIを作成できませんでした。無効のタイプ " + _attackAIData.AttackAIType);
					return;
			}

			attackAI.Setup(_attackAIData);
		}
	}
}
