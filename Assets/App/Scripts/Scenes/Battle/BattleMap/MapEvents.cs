//
// MapEvents.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.08
//

namespace Ling.Scenes.Battle.BattleMap
{
	/// <summary>
	/// Map関連のイベント
	/// </summary>
	public class MapEvents
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
	}
}
