//
// DropItemManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.10
//

using System.Collections.Generic;
using Ling.Const;
using UnityEngine;

namespace Ling.Map
{
	/// <summary>
	/// DropItemControllerを纏めている場所
	/// </summary>
	public class DropItemController : MonoBehaviour
	{
		private Const.TileFlag BlockTileFlag = Const.TileFlag.Hole | Const.TileFlag.Item;

		[SerializeField] private ItemPool _pool = default;
		
		// 生成されたアイテムを保持する


		/// <summary>
		/// マップに配置する
		/// </summary>
		public void Apply(MasterData.Stage.MapMaster mapMaster, MapData mapData)
		{
			// アイテム生成数を決める
			var dropTableMaster = mapMaster.DropItemTableMaster;
			var num = dropTableMaster.GetRandomDropNum();

			var tileDataMap = mapData.TileDataMap;

			// 回数分配置
			for (int i = 0; i < num; ++i)
			{
				// 配置するアイテムマスタ
				var itemMaster = dropTableMaster.GetRandomItem();

				// 部屋に何もない場所に配置
				var roomData = tileDataMap.GetRandomRoom();

				// 配置できる場所に配置する
				// 配置できる箇所がない場合はもう何もしないとする
				// 最大 5 回
				for (int count = 0; count < 5; ++count)
				{
					var tileData = roomData.GetRandom();
					if (!CanPlaced(tileData)) continue;

					// 配置
					tileData.AddFlag(Const.TileFlag.Item);
				}
			}
		}

		/// <summary>
		/// アイテム情報を破棄する
		/// </summary>
		public void Release()
		{

		}

		/// <summary>
		/// 配置可能箇所か
		/// </summary>
		private bool CanPlaced(TileData tileData)
		{
			if (tileData.HasFlag(BlockTileFlag)) return false;

			return true;
		}
	}
}
