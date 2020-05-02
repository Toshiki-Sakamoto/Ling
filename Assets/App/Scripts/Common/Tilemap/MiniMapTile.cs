// 
// MiniMapTile.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.02
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Ling.Utility.Extensions;
using Zenject;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Ling.Common.Tilemap
{
	/// <summary>
	/// 
	/// </summary>
	[System.Serializable]
	public class MiniMapTile : TileBase 
    {
		#region 定数, class, enum

		[System.Serializable]
		public class MiniMapData
		{
			public Map.Builder.TileFlag tileFlag = Map.Builder.TileFlag.None;
			public Sprite sprite = null;
			public Color color = Color.white;

			public string GetTileFlagString() =>
				tileFlag.ToString();
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private MiniMapData[] _miniMapData = null;
		[SerializeField] private Map.Builder.TileFlag _tileFlag = Map.Builder.TileFlag.None;

		#endregion


		#region プロパティ

		public MiniMapData[] MiniMapDataArray => _miniMapData;

		#endregion


		#region public, protected 関数

		public void SetupMiniMapData()
		{
			// すでに存在する場合は何もしない
			if (!_miniMapData.IsNullOrEmpty()) return;

			var tileFlags = System.Enum.GetValues(typeof(Map.Builder.TileFlag));
			_miniMapData = new MiniMapData[tileFlags.Length];

			int count = 0;
			foreach (Map.Builder.TileFlag tileFlag in tileFlags)
			{
				var miniMapData = new MiniMapData();
				miniMapData.tileFlag = tileFlag;

				_miniMapData[count++] = miniMapData;
			}
		}

		public override void RefreshTile(Vector3Int position, ITilemap tilemap)
		{
			if (HasTileValue(tilemap, position))
			{
				tilemap.RefreshTile(position);
			}
		}

		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			UpdateTile(position, tilemap, ref tileData);
		}

		#endregion


		#region private 関数

		private bool HasTileValue(ITilemap tilemap, Vector3Int position)
		{
			var tile = tilemap.GetTile(position);
			return (tile != null && tile == this);
		}

		private void UpdateTile(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			tileData.transform = Matrix4x4.identity;
			tileData.color = Color.white;

			var miniMapData = System.Array.Find(_miniMapData, miniMapData_ => miniMapData_.tileFlag == _tileFlag);
			if (miniMapData == null)
			{
				tileData.sprite = null;
				tileData.color = Color.clear;
				return;
			}

			tileData.sprite = miniMapData.sprite;
			tileData.color = miniMapData.color;
		}

		#endregion


		#region MonoBegaviour

		#endregion
	}
}