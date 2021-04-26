//
// ItemRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using Ling.UserData.Item;

namespace Ling.UserData.Repository
{
	/// <summary>
	/// プレイヤーが持っている持ち物
	/// </summary>
	public class ItemUserDataRepository : Utility.UserData.UserDataRepository<ItemUserData>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

#if DEBUG
		protected override bool EnableDebugMode => true; // todo: 強制ON
#endif

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

#if DEBUG
		protected override void DebugAddFinished()
		{
			var entities = new ItemUserData[]
				{
					new ItemUserData { ID = 1 }
				};

			Add(entities);
		}
#endif

		#endregion


		#region private 関数

		#endregion
	}
}
