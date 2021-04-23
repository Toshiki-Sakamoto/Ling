//
// DebugConfigData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.06
//

namespace Ling._Debug
{
	public class BattleMenuData : Common.DebugConfig.DebugMenuItem.Data
	{
		public Common.DebugConfig.DebugCheckItem.Data aStarScoreShow;

		public BattleMenuData()
			: base("BattleMenu")
		{
			aStarScoreShow = Common.DebugConfig.DebugCheckItem.Create(false, "AStarのスコアを表示する");

			Add(aStarScoreShow);
		}
	}
}
