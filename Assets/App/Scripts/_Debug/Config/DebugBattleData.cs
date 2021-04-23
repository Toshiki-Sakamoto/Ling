//
// DebugConfigData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.06
//

namespace Ling._Debug
{
	public class BattleMenuData : Utility.DebugConfig.DebugMenuItem.Data
	{
		public Utility.DebugConfig.DebugCheckItem.Data aStarScoreShow;

		public BattleMenuData()
			: base("BattleMenu")
		{
			aStarScoreShow = Utility.DebugConfig.DebugCheckItem.Create(false, "AStarのスコアを表示する");

			Add(aStarScoreShow);
		}
	}
}
