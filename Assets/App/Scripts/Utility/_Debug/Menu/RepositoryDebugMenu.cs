//
// RepositoryDebugMenu.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.23
//

namespace Utility._Debug.Menu
{
#if DEBUG
	/// <summary>
	/// Repository関連
	/// </summary>
	public class RepositoryDebugMenu : Utility.DebugConfig.DebugMenuItem.Data
	{
		public RepositoryDebugMenu()
			: base("Repository")
		{
		}
	}
#endif
}
