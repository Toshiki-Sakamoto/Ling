//
// MiniMapTileSpriteData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.03
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Common.Tile
{
	/// <summary>
	/// 
	/// </summary>

	[System.Serializable]
	public class MiniMapTileSpriteData
	{
		private bool _isValid = false;  // 有効

		public Sprite[] Sprites { get; private set; }

		public Map.Builder.TileFlag TileFlag { get; private set; }


		public void Setup(Map.Builder.TileFlag tileFlag, int spriteMax)
		{
			TileFlag = tileFlag;
			
			if (Sprites.IsNullOrEmpty())
			{
				Sprites = new Sprite[spriteMax];
			}
		}

		public bool HasFlag(Map.Builder.TileFlag tileFlag) =>
			TileFlag.HasFlag(tileFlag);

		public string GetTileFlagString() =>
			TileFlag.ToString();

		public Sprite GetSpriteByInex(int index)
		{
			if (index < 0 || index >= Sprites.Length)
			{
				Utility.Log.Error($"Spriteが設定されていません Index {index.ToString()}");
				return null;
			}

			return Sprites[index];
		}
	}
}
