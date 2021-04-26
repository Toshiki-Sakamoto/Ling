//
// DebugUserDataMenu.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.23
//

namespace Ling.UserData._Debug
{
	/// <summary>
	/// UserDataメニュー
	/// </summary>
	public class DebugUserData : Utility.DebugConfig.DebugMenuItem.Data
	{
		public Utility.DebugConfig.DebugCheckItem.Data enableItemDebugLoad;

		public DebugUserData()
			: base("UserData")
		{
			enableItemDebugLoad = Utility.DebugConfig.DebugCheckItem.Create(true, "アイテムをデバッグ読み込み");

			Add(enableItemDebugLoad);
		}
	}
}
