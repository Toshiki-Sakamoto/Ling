//
// MoveAIFactory.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.10
//

namespace Ling.AI.Move
{
	/// <summary>
	/// 移動AI作成Factory
	/// </summary>
	public class MoveAIFactory
    {
		private Const.MoveAIType _moveAIType;
		private int _param1;

		public MoveAIFactory(Const.MoveAIType moveAIType, int param1)
		{
			_moveAIType = moveAIType;
			_param1 = param1;
		}

		public AIBase Create()
		{
			AIBase moveAI = null;

			switch (_moveAIType)
			{
				case Const.MoveAIType.Random:
					moveAI = new AIRandom();
					break;

				case Const.MoveAIType.NormalTracking:
					moveAI = new AINormalTracking();
					break;

				default:
					Utility.Log.Error("MoveAIを作成できませんでした。無効のタイプ " + _moveAIType);
					return null;
			}

			moveAI.Param1 = _param1;

			return moveAI;
		}
	}
}
