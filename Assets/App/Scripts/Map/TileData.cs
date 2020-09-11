//
// TileData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.23
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Ling.Const;


namespace Ling.Map
{
	/// <summary>
	/// マップの中の一マスのデータ
	/// </summary>
	public class TileData
	{
		// TileDataが更新されたときに呼び出される
		public System.Action<TileData, bool /* Add:true Remove:false*/> onUpdateTileFlag;


		/// <summary>
		/// タイルデータがもつフラグ
		/// </summary>
		public TileFlag Flag { get; private set; }

		/// <summary>
		/// タイルデータの座標
		/// </summary>
		public Vector2Int Pos { get; private set; }
		public int X => Pos.x;
		public int Y => Pos.y;

		public int Index { get; private set; }

		/// <summary>
		/// 部屋の場合、各部屋のIndexを割り当てる
		/// </summary>
		public int? RoomIndex { get; private set; }

		/// <summary>
		/// 壁ならtrue
		/// </summary>
		public bool IsWall => HasFlag(TileFlag.Wall);

		/// <summary>
		/// 上階段ならtrue
		/// </summary>
		public bool IsStepUp => HasFlag(TileFlag.StepUp);


		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
		{
			Flag = TileFlag.None;
			Index = 0;
			RoomIndex = null;
		}

		public void SetPos(int x, int y) =>
			Pos = new Vector2Int(x, y);

		public void SetIndex(int index) => 
			Index = index;

		public void SetRoomIndex(int roomIndex) =>
			RoomIndex = roomIndex;

		/// <summary>
		/// フラグとして情報を追加する
		/// </summary>
		/// <param name="tileFlag"></param>
		public void AddFlag(TileFlag tileFlag)
		{
			Flag |= tileFlag;

			onUpdateTileFlag?.Invoke(this, true);
		}

		public void SetFlag(TileFlag tileFlag)
		{
			Flag = tileFlag;

			onUpdateTileFlag?.Invoke(this, false);
		}

		/// <summary>
		/// フラグを削除する
		/// </summary>
		/// <param name="tileFlag"></param>
		public void RemoveFlag(TileFlag tileFlag)
		{
			Flag &= ~tileFlag;
		}

		/// <summary>
		/// 指定したフラグを持っているか
		/// </summary>
		/// <returns></returns>
		public bool HasFlag(TileFlag tileFlag)
		{
			// enum のHasFlagは引数のflagをどちらとも持っていないと0を返すので注意
			//return Flag.HasFlag(tileFlag);
			return (tileFlag & Flag) != 0;
		}


		/// <summary>
		/// 壁にする
		/// </summary>
		public void SetWall()
		{
			AddFlag(TileFlag.Wall);
		}

	}
}
