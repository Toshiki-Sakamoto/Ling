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


namespace Ling.Map.Builder
{
	/// <summary>
	/// マップの中の一マスのデータ
	/// </summary>
	public struct TileData
	{
		/// <summary>
		/// タイルデータがもつフラグ
		/// </summary>
		public Const.TileFlag Flag { get; private set; }

		/// <summary>
		/// 壁ならtrue
		/// </summary>
		public bool IsWall { get { return HasFlag(Const.TileFlag.Wall); } }

		/// <summary>
		/// 上階段ならtrue
		/// </summary>
		public bool IsStepUp { get { return HasFlag(Const.TileFlag.StepUp); } }


		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
		{
			Flag = Const.TileFlag.None;
		}

		/// <summary>
		/// 壁にする
		/// </summary>
		public void SetWall()
		{
			// 壁にするときに初期化する
			Initialize();

			AddFlag(Const.TileFlag.Wall);
		}

		/// <summary>
		/// フラグとして情報を追加する
		/// </summary>
		/// <param name="tileFlag"></param>
		public void AddFlag(Const.TileFlag tileFlag)
		{
			Flag |= tileFlag;
		}

		/// <summary>
		/// 指定したフラグを持っているか
		/// </summary>
		/// <returns></returns>
		public bool HasFlag(Const.TileFlag tileFlag)
		{
			return Flag.HasFlag(tileFlag);
		}
	}
}
