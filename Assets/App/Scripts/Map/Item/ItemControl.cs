//
// ItemControl.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.15
//

using UnityEngine;
using Ling.MasterData.Item;

namespace Ling.Map.Item
{
	/// <summary>
	/// アイテムPrefabをコントロールする
	/// </summary>
	public class ItemControl : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private ItemView _view = default;

		private DropItemController _dropItemController;

		#endregion


		#region プロパティ

		public ItemMaster Master { get; private set; }

		/// <summary>
		/// 現在配置されてるTileData
		/// </summary>
		public TileData TileData { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(ItemMaster itemMaster, TileData tileData)
		{
			Master = itemMaster;
			TileData = tileData;

			_view.Setup();
		}

		/// <summary>
		/// マップから自分の情報を剥がす
		/// </summary>
		public void DetachByMap()
		{
			TileData.RemoveFlag(Const.TileFlag.Item);
		}

		/// <summary>
		/// 自分を削除してプールに戻す
		/// </summary>
		public void Release()
		{
			var pool = GetComponent<Utility.Pool.PoolItem>();
			if (pool == null)
			{
				Utility.Log.Error("プール管理されていない");
				return;
			}

			pool.Detach();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
