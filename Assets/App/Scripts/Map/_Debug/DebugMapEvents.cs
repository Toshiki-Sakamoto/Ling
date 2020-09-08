//
// DebugMapEvents.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.09.06
//

using UnityEngine;

namespace Ling.Map._Debug
{
#if DEBUG

	// デバッグUIをすべてクリアする
	public class EventDebugUIClearAll
	{
		public int mapLevel;
	}

	// マップ探索中にNode作成された場合
	public class EventSearchNodeCreated
	{
		public int mapLevel;
		public Vector2Int position;
		public Utility.Algorithm.Astar.Node node;
	}
#endif
}
