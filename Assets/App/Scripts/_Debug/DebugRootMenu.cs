//
// DebugRootMenu.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.06
//

namespace Ling._Debug
{
	/// <summary>
	/// 規定のDebugMenu
	/// </summary>
	public class DebugRootMenuData : Common.DebugConfig.DebugRootMenuData
	{
		public _Debug.BattleMenuData battleMenu;

		public DebugRootMenuData()
			: base("Root")
		{
			battleMenu = new _Debug.BattleMenuData();

			Add(battleMenu);
		}
	}
}
