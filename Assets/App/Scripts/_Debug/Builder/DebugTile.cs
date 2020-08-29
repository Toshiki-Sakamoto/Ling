// 
// DebugTile.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.29
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling._Debug.Builder
{
	/// <summary>
	/// デバッグ表示用のタイルデータ
	/// </summary>
	public class DebugTile : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Utility.TouchPointEventTrigger _touchPointEventTrigger = default;

		#endregion


		#region プロパティ

		public Map.TileData TileData { get; set; }
		public Const.TileFlag TileFlag { get; set; }

		public Vector2Int Pos => TileData.Pos;

		#endregion


		#region public, protected 関数

		public void SetTileData(Map.TileData tileData) 
		{
			_touchPointEventTrigger.IntParam = tileData.Index;
			TileData = tileData;
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}