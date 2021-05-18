//
// DropItemManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.10
//

using System.Collections.Generic;
using Ling.Const;
using UnityEngine;
using Ling.MasterData.Item;
using Zenject;
using UnityEngine.Tilemaps;
using Utility.Extensions;
using Sirenix.OdinInspector;

namespace Ling.Map
{
	/// <summary>
	/// DropItemControllerを纏めている場所
	/// </summary>
	public class DropItemController : MonoBehaviour
	{
		private Const.TileFlag BlockTileFlag = Const.TileFlag.Hole | Const.TileFlag.Item;

		[SerializeField] private ItemPool _pool = default;
		[ShowInInspector] private Dictionary<int, Item.ItemControl> _itemObjectDict = new Dictionary<int, Item.ItemControl>();


		private IMapObjectInstaller _mapObjectInstaller;
		private int _level;
		private MapData _mapData;
		private Tilemap _tileMap;

		// 生成されたアイテムを保持する

		public void Setup(IMapObjectInstaller mapObjectInstaller)
		{
			_mapObjectInstaller = mapObjectInstaller;
		}

		/// <summary>
		/// マップに配置する
		/// </summary>
		public void CreateAtBuild(int level, MasterData.Stage.MapMaster mapMaster, MapData mapData, Tilemap tilemap)
		{
			_level = level;
			_mapData = mapData;
			_tileMap = tilemap;

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
					
					var itemObject = CreateItemObject(tileData, itemMaster);
					if (itemObject == null) continue;
				}
			}
		}

		/// <summary>
		/// アイテム情報を破棄する
		/// </summary>
		public void Release()
		{
			// すべてプールに戻す
			foreach (var pair in _itemObjectDict)
			{
				pair.Value.Release();
			}

			_itemObjectDict.Clear();
		}

		public Item.ItemControl CreateItemObject(TileData tileData, ItemMaster itemMaster)
		{
			if (!CanPlaced(tileData)) return null;

			// 配置
			tileData.AddFlag(Const.TileFlag.Item);

			// アイテムを作成する
			var item = _pool.Pop<Item.ItemControl>(itemMaster.PrefabType);
			item.Setup(itemMaster, tileData);
			
			// Poolに戻った時を検知する
			var poolItem = item.GetComponent<Utility.Pool.PoolItem>();
			poolItem.OnRelease = new Utility.FunctionParam1<Item.ItemControl>(item, 
				item_ => 
				{
					_itemObjectDict.Remove(item_.TileData.Index);
				});

			_itemObjectDict.Add(tileData.Index, item);

			// マップに配置する
			_mapObjectInstaller.PlaceObject(item.gameObject, _level, tileData.Pos, OrderType.Item);

			return item;
		}

		/// <summary>
		/// 配置可能箇所か
		/// </summary>
		private bool CanPlaced(TileData tileData)
		{
			if (tileData.HasFlag(BlockTileFlag)) return false;

			return true;
		}

		public bool TryGetItemObject(int index, out Item.ItemControl itemControl)
		{
			itemControl = null;

			if (_itemObjectDict.TryGetValue(index, out var result))
			{
				itemControl = result;
				return true;
			}

			return false;
		}
	}
}
