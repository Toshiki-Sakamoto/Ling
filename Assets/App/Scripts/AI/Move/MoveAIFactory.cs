//
// MoveAIFactory.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.10
//

using Ling.MasterData.Chara;

namespace Ling.AI.Move
{
	/// <summary>
	/// 移動AI作成Factory
	/// </summary>
	public class MoveAIFactory
    {
		private MoveAIData _moveAIData;

		public MoveAIFactory(MoveAIData moveAIData)
		{
			_moveAIData = moveAIData;
		}

		public AIBase Create()
		{
			AIBase moveAI = null;

			switch (_moveAIData.MoveAIType)
			{
				case Const.MoveAIType.Random:
					moveAI = new AIRandom();
					break;

				case Const.MoveAIType.NormalTracking:
					moveAI = new AINormalTracking();
					break;

				default:
					Ling.Utility.Log.Error("MoveAIを作成できませんでした。無効のタイプ " + _moveAIData.MoveAIType);
					return null;
			}

			moveAI.Setup(_moveAIData);

			return moveAI;
		}
	}
}
