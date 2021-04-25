//
// RepositoryDebugMenu.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.23
//

using Zenject;

namespace Utility._Debug.Menu
{
#if DEBUG
	/// <summary>
	/// Repository関連
	/// </summary>
	public class RepositoryDebugMenu : Utility.DebugConfig.DebugMenuItem.Data
	{
		// 保存ファイルの削除
		protected Utility.DebugConfig.DebugButtonItem.Data _fileRemovebutton;

		public RepositoryDebugMenu()
			: base("Repository")
		{
		}
	}
#endif
}
