//
// DebugRootMenu.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.06
//

using Zenject;

namespace Ling._Debug
{
	/// <summary>
	/// 規定のDebugMenu
	/// </summary>
	public class DebugRootMenuData : Utility.DebugConfig.DebugRootMenuData
	{
		public _Debug.BattleMenuData battleMenu;

		public DebugRootMenuData()
			: base("Root")
		{
		}

		public override void Initialize()
		{
			battleMenu = CreateAndAddItem<_Debug.BattleMenuData>();
		}
	}
}
