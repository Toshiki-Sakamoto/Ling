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

		public void Attach<TModel, TView>(Chara.CharaControl<TModel, TView> charaControl)
			where TModel : Chara.CharaModel
			where TView : Chara.ViewBase
		{
			AIBase moveAI = null;

			switch (_moveAIData.MoveAIType)
			{
				case Const.MoveAIType.Random:
					moveAI = charaControl.AttachMoveAI<AIRandom>();
					break;

				case Const.MoveAIType.NormalTracking:
					moveAI = charaControl.AttachMoveAI<AINormalTracking>();
					break;

				default:
					Ling.Utility.Log.Error("MoveAIを作成できませんでした。無効のタイプ " + _moveAIData.MoveAIType);
					return;
			}

			moveAI.Setup(_moveAIData);
		}
	}
}
