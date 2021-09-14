//
// MapEvents.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.08
//

using UnityEngine;

namespace Ling.Map
{
	/// <summary>
	/// フラグ
	/// </summary>
	public class EventTileFlagUpdate
	{
		public enum Type { None, Add, Remove };

		public int level;
		public int x, y;
		public Const.TileFlag tileFlag;
		public Type type;

		public static EventTileFlagUpdate CreateAtAdd(int level, int x, int y, Const.TileFlag tileFlag)
		{
			var instance = new EventTileFlagUpdate();
			instance.level = level;
			instance.x = x;
			instance.y = y;
			instance.tileFlag = tileFlag;
			instance.type = Type.Add;

			return instance;
		}

		public static EventTileFlagUpdate CreateAtRemove(int level, int x, int y, Const.TileFlag tileFlag)
		{
			var instance = new EventTileFlagUpdate();
			instance.level = level;
			instance.x = x;
			instance.y = y;
			instance.tileFlag = tileFlag;
			instance.type = Type.Remove;

			return instance;
		}
	}

	/// <summary>
	/// マップが削除された
	/// </summary>
	public class EventRemoveMap
	{
		public int level;
	}

	/// <summary>
	/// マップオブジェクトが配置された時のイベント
	/// </summary>
	public class EventSpawnMapObject
	{
		public Const.TileFlag Flag;
		public int MapLevel;
		public GameObject followObj;
	}

	/// <summary>
	/// マップオブジェクトから削除された時のイベント
	/// </summary>
	public class EventDestroyMapObject
	{
		public int MapLevel;
		public GameObject followObj;
	}
}
