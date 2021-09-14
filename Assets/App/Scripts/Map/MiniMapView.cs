// 
// MiniMapView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.02
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using Zenject;


namespace Ling.Map
{
	/// <summary>
	/// 
	/// </summary>
	public class MiniMapView : MonoBehaviour, IMiniMapView
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Grid _grid = null;
		[SerializeField] private Tilemap _tileMap = null;
		[SerializeField] private Transform _root;

		private Tile.MapTile _mapTile;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数


		public void Setup()
		{
			_mapTile = Resources.Load<Tile.MapTile>("Tiles/MiniMapTile");
			if (_mapTile == null)
			{
				Utility.Log.Error("MiniMapTileリソースが見つかりません");
			}
		}

		public void SetActive(bool isActive)
		{
			gameObject.SetActive(isActive);
		}

		public void SetTileDataMap(Map.TileDataMap tileDataMap)
		{
			Clear();

			_mapTile.SetTileDataMap(tileDataMap);

			var height = tileDataMap.Height;
			var width = tileDataMap.Width;

			for (int y = 0; y <= height; ++y)
			{
				for (int x = 0; x <= width; ++x)
				{
					_tileMap.SetTile(new Vector3Int(x, y, 0), _mapTile);
				}
			}

			// 画面中央に持ってくる
			var tileMapSize = _tileMap.size;
			_tileMap.transform.localPosition = new Vector3(tileMapSize.x * -0.5f, 0.0f, 0.0f);
		}

		/// <summary>
		/// オブジェクトをマップに適用する
		/// </summary>
		public void DeployView(GameObject followObj, MiniMapPointObject point)
		{
			point.transform.SetParent(_root, false);
		}

		public void Clear()
		{
			_tileMap.ClearAllTiles();
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}