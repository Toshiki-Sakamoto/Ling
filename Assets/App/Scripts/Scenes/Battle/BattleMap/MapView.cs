// 
// MapView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.03
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using Zenject;


namespace Ling.Scenes.Battle.BattleMap
{
	/// <summary>
	/// ダンジョンマップView
	/// </summary>
	public class MapView : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		// Ground用
		[SerializeField] private Grid _groundGrid = null;
		[SerializeField] private Tilemap _groundTileMap = null;

		#endregion


		#region プロパティ

		public Tilemap Tilemap => _groundTileMap;

		#endregion


		#region public, protected 関数

		public void Setup(int width, int height, Common.Tile.MapTile tile)
		{
			for (int y = 0; y <= height; ++y)
			{
				for (int x = 0; x <= width; ++x)
				{
					_groundTileMap.SetTile(new Vector3Int(x, y, 0), tile);
				}
			}
		}

		public void Clear()
		{
			_groundTileMap.ClearAllTiles();
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
		}

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update()
		{
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		void OnDestoroy()
		{
		}

		#endregion
	}
}