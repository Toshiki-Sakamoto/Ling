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


namespace Ling.Scenes.Battle.MiniMap
{
	/// <summary>
	/// 
	/// </summary>
	public class MiniMapView : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Grid _grid = null;
		[SerializeField] private Tilemap _tileMap = null;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void Setup(int width, int height)
		{
			for (int y = 0; y <= height; ++y)
			{
				for (int x = 0; x <= width; ++x)
				{
					var tile = Resources.Load<Tile>("Tiles/RandomTile");
					_tileMap.SetTile(new Vector3Int(x, y, 0), tile);
				}
			}
		}

		public void Clear()
		{
			_tileMap.ClearAllTiles();
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
			var tile = _tileMap.GetTile(new Vector3Int(0, 0, 0));

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