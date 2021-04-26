//
// ItemUserData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using Ling.MasterData.Item;
using Ling.Common.Item;

namespace Ling.UserData.Item
{
	/// <summary>
	/// ユーザーが持っているアイテムデータ
	/// </summary>
	public class ItemUserData : Utility.UserData.UserDataBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public ItemMaster Master { get; private set; }
		public ItemEntity Entity { get; private set; } = new ItemEntity();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数


		#endregion


		#region private 関数

		#endregion
	}
}
