//
// CharaEvents.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.09
//

using UnityEngine;

namespace Ling.Chara
{
	/// <summary>
	/// 移動した
	/// </summary>
	public class EventPosUpdate
	{
		public Vector2Int? prevPos;	// 以前の座標
		public Vector2Int newPos;	// 今回の座標
		public int mapLevel;
		public CharaType charaType;
	}

	/// <summary>
	/// 削除された
	/// </summary>
	public class EventRemove
	{
		public ICharaController chara;
	}
}
