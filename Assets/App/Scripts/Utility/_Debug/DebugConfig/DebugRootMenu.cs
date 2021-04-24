//
// DebugRootMenu.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.06
//

using Utility._Debug.Menu;
using Zenject;

namespace Utility.DebugConfig
{
	/// <summary>
	/// 規定のDebugMenu
	/// </summary>
	public abstract class DebugRootMenuData : DebugMenuItem.Data, IInitializable
	{
		public DebugRootMenuData(string name)
			: base(name) 
		{
		}

		public abstract void Initialize();
	}
}
