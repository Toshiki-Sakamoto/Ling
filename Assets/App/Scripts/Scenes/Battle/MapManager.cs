// 
// MapView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.01
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using Zenject;


namespace Ling.Scenes.Battle
{
	/// <summary>
	/// Map管理者
	/// ミニマップも管理させる
	/// </summary>
	public class MapManager : Utility.MonoSingleton<MapManager> 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Transform _root = null;

		[Inject] private Map.Builder.IManager _builderManager = null;

		#endregion


		#region プロパティ

		/// <summary>
		/// Map/MiniMapの管理
		/// </summary>
		public BattleMap.MapControl MapControl { get; private set; }
		public BattleMap.MiniMapControl MiniMapControl { get; private set; }

		/// <summary>
		/// Tilemapの参照
		/// </summary>
		public Tilemap MapTilemap => MapControl.Tilemap;
		public Tilemap MiniMapTilemap => MiniMapControl.Tilemap;

		#endregion


		#region public, protected 関数

		public void Setup()
		{
			// マップの設定
			MapControl.Setup(_builderManager.Builder.TileDataMap);

			// ミニマップの設定
			MiniMapControl.Setup(_builderManager.Builder.TileDataMap);
		}

		public Vector3 GetCellCenterWorldByMap(int x, int y) =>
			MapControl.GetCellCenterWorld(x, y);

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour


		protected override void Awake()
		{
			base.Awake();

			// 各種Controlの生成
			MapControl = new BattleMap.MapControl();
			MiniMapControl = new BattleMap.MiniMapControl();
		}

		#endregion
	}
}