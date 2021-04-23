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
	public class DebugRootMenuData : Common.DebugConfig.DebugRootMenuData, IInitializable
	{
		public _Debug.BattleMenuData battleMenu;

		public DebugRootMenuData()
			: base("Root")
		{
		}

		void IInitializable.Initialize()
		{
			battleMenu = CreateAndAddItem<_Debug.BattleMenuData>();

			Add(battleMenu);
		}
	}
}
