//
// ItemRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using Ling.UserData.Item;
using UnityEngine;

namespace Ling.UserData.Repository
{
	/// <summary>
	/// プレイヤーが持っている持ち物
	/// </summary>
	public class ItemUserDataRepository : Utility.UserData.UserDataRepository<ItemUserDataRepository, ItemUserData>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private int _maxCapacity = 15;	// 最大容量 : todo:生成時マスタデータから初期値を決めるようにする

		#endregion


		#region プロパティ

#if DEBUG
		protected override bool EnableDebugMode => true; // todo: 強制ON
#endif

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 容量確認後追加する
		/// </summary>
		public bool ConfirmAndAdd(MasterData.Item.ItemMaster item)
		{
			if (Entities.Count >= _maxCapacity)
			{
				// 容量いっぱいなので追加できなかった
				return false;
			}

			// マスタからユーザーデータを作成する
			var userData = Item.ItemUserData.Create(item);

			Add(userData);
			return true;
		}

#if DEBUG
		protected override void DebugAddFinished()
		{
			var entities = new ItemUserData[]
				{
					new ItemUserData { ID = 1, Category = Const.Item.Category.Food },
					new ItemUserData { ID = 2, Category = Const.Item.Category.Food },
				};

			Add(entities);
		}
#endif

		#endregion


		#region private 関数

		#endregion
	}
}
