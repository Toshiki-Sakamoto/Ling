//
// DebugMenu.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.30
//

using System.Collections.Generic;

namespace Ling.Common.DebugConfig
{
	public class DebugMenu
	{
		private List<IItemData> _dataList = new List<IItemData>();
		public string Name { get; private set; }
		public int DataCount => _dataList.Count;


		/// <summary>
		/// リストの中身
		/// </summary>
		public IItemData this[int index] => _dataList[index];

		public DebugMenu(string name) =>
			Name = name;

		public void Add(IItemData item)
		{
			if (item == null)
			{
				Utility.Log.Error("追加しようとしたItemがNull");
				return;
			}

			_dataList.Add(item);
		}
	}

	/// <summary>
	/// Menuクラスを子供として保持する
	/// 親のMenuに子供のMenuを登録したい場合はこれに包んでからMenuクラスのAddItemに指定する
	/// </summary>
	public class DebugMenuItem : ItemDataBase<DebugMenu>
	{
		public DebugMenu SubMenu { get; private set; }

		public DebugMenuItem(string title, DebugMenu menu)
			: base(title)
		{
			SubMenu = menu;
		}

		protected override void DataUpdateInternal(DebugMenu obj) {}

		public override Const.MenuType GetMenuType() =>
			Const.MenuType.MenuItem;
	}
}
