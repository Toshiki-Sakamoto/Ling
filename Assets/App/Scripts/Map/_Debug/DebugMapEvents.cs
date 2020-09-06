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
	public class EventSearchNodeCreated
	{
		public Vector2Int position;
		public Utility.Algorithm.Astar.Node node;
	}
#endif
}
