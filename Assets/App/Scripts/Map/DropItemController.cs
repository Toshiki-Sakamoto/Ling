//
// DropItemManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.10
//

using System;
using System.Collections.Generic;
using Ling.Common.Item;
using Ling.Const;
using UnityEngine;
using Ling.MasterData.Item;
using Zenject;
using UnityEngine.Tilemaps;
using Utility.Extensions;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;
using System.Linq;
using MessagePipe;

namespace Ling.Map
{
	/// <summary>
	/// DropItemControllerを纏めている場所
	/// </summary>
	public class DropItemController : MonoBehaviour
	{
		private Const.TileFlag BlockTileFlag = Const.TileFlag.Hole | Const.TileFlag.Item;

		[Inject] private Utility.SaveData.ISaveDataHelper _saveHelper;
		[Inject] private Utility.IEventManager _eventManager;
		[Inject] private IPublisher<EventSpawnMapObject> _eventSpawnMapObject;
		
		[SerializeField] private ItemPool _pool = default;
		[ShowInInspector] private Dictionary<int, Item.ItemControl> _itemObjectDict = new Dictionary<int, Item.ItemControl>();
		
		private Dictionary<int, Common.Item.ItemEntity> _itemEntityDict = new Dictionary<int, ItemEntity>();


		private IMapObjectInstaller _mapObjectInstaller;
		private int _level;
		private MapData _mapData;
		private Tilemap _tileMap;
		private MasterData.Stage.MapMaster _mapMaster;

		// 生成されたアイテムを保持する

		public void Setup(IMapObjectInstaller mapObjectInstaller)
		{
			_mapObjectInstaller = mapObjectInstaller;
		}
		
		/// <summary>
		/// アイテム情報を保存する
		/// </summary>
		public void Save()
		{
			_saveHelper.Save("Item.save", $"ItemEntities{_level}", _itemEntityDict);
		}

		public void SetMapInfo(int level, MasterData.Stage.MapMaster mapMaster, MapData mapData, Tilemap tilemap)
		{
			_level = level;
			_mapData = mapData;
			_tileMap = tilemap;
			_mapMaster = mapMaster;
		}

		/// <summary>
		/// マップに配置する
		/// </summary>
		public void CreateAtBuild()
		{
			// アイテム生成数を決める
			var dropTableMaster = _mapMaster.DropItemTableMaster;
			var num = dropTableMaster.GetRandomDropNum();

			var tileDataMap = _mapData.TileDataMap;

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
					
					// 配置できる箇所か
					if (!CanPlaced(tileData)) continue;

					// 配置
					tileData.AddFlag(Const.TileFlag.Item);
					
					var itemEntity = new Common.Item.ItemEntity();
					itemEntity.CreateUniqKey();
					itemEntity.SetMaster(itemMaster);
					
					_itemEntityDict.Add(tileData.Index, itemEntity);
					
					var itemObject = CreateItemObject(tileData, itemEntity);
					if (itemObject == null) continue;
				}
			}
		}
		
		/// <summary>
		/// 指定したマップにあるアイテムに対して配置を行う
		/// </summary>
		public void Resume()
		{
			_itemEntityDict = _saveHelper.Load<Dictionary<int, Common.Item.ItemEntity>>("Item.save", $"ItemEntities{_level}");
			
			var tileDataMap = _mapData.TileDataMap;
			
			foreach (var pair in _itemEntityDict)
			{
				var tile = tileDataMap.Tiles[pair.Key];
				
				var itemObject = CreateItemObject(tile, pair.Value);
				if (itemObject == null) continue;
			}
		}

		/// <summary>
		/// アイテム情報を破棄する
		/// </summary>
		public void ReleaseAll()
		{
			// すべてプールに戻す
			var tmps = _itemObjectDict.Values.ToArray();
			foreach (var value in tmps)
			{
				value.Release();
			}

			_itemObjectDict.Clear();
		}
		
		public void Release(int index)
		{
			if (!TryGetItemObject(index, out var itemControl)) return;
			
			itemControl.Release();
		}

		public Item.ItemControl CreateItemObject(TileData tileData, Common.Item.ItemEntity itemEntity)
		{
			// アイテムを作成する
			var item = _pool.Pop<Item.ItemControl>(itemEntity.Master.PrefabType);
			item.Setup(itemEntity, tileData);
			
			// Poolに戻った時を検知する
			var poolItem = item.GetComponent<Utility.Pool.PoolItem>();
			poolItem.OnRelease = new Utility.FunctionParam1<Item.ItemControl>(item, 
				item_ => 
				{
					var releaseIndex = item_.TileData.Index;
					_itemObjectDict.Remove(releaseIndex);
					_itemEntityDict.Remove(releaseIndex);
				});

			_itemObjectDict.Add(tileData.Index, item);

			// マップに配置する
			_mapObjectInstaller.PlaceObject(item.gameObject, _level, tileData.Pos, OrderType.Item);

			// 生成したことを伝える
			_eventSpawnMapObject.Publish(new EventSpawnMapObject 
				{ 
					Flag = Const.TileFlag.Item, 
					MapLevel = _level, 
					followObj = item.gameObject 
				});

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
		
		private void Awake()
		{
			_eventManager.Add<Utility.SaveData.EventSaveCall>(this, ev =>
				{
					Save();
				});
		}

		private void OnDestroy()
		{
			_eventManager.RemoveAll(this);
		}
	}
}
