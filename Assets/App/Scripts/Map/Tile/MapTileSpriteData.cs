//
// MiniMapTileSpriteData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.03
//


using Ling.Const;
using Utility.Extensions;
using UnityEngine;

namespace Ling.Map.Tile
{
	/// <summary>
	/// 
	/// </summary>

	[System.Serializable]
	public class MapTileSpriteData
	{
		[SerializeField] private bool _isValid = false;
		[SerializeField] private Sprite[] _sprites = null;
		[SerializeField] private Const.TileFlag _tileFlag = Const.TileFlag.None;

		/// <summary>
		/// 描画有効/無効か
		/// </summary>
		public bool IsValid { get { return _isValid; } set { _isValid = value; } }

		public Sprite[] Sprites { get { return _sprites; } private set { _sprites = value; } }

		public Const.TileFlag TileFlag { get { return _tileFlag; } private set { _tileFlag = value; } }


		public void Setup(Const.TileFlag tileFlag, int spriteMax)
		{
			TileFlag = tileFlag;

			if (Sprites.IsNullOrEmpty())
			{
				Sprites = new Sprite[spriteMax];
			}
		}

		public bool HasFlag(Const.TileFlag tileFlag) =>
			TileFlag.HasAny(tileFlag);

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
