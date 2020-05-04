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

		private Map.TileDataMap _tileDataMap;

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
			_tileDataMap = _builderManager.Builder.TileDataMap;

			// マップの設定
			MapControl.Setup(_tileDataMap);

			// ミニマップの設定
			MiniMapControl.Setup(_tileDataMap);
		}

		public Vector3 GetCellCenterWorldByMap(int x, int y) =>
			MapControl.GetCellCenterWorld(x, y);

		/// <summary>
		/// キャラが移動できるか
		/// </summary>
		/// <param name="chara"></param>
		public bool CanMoveChara(Chara.Base chara, in Vector3Int addMoveDir)
		{
			var destPos = chara.CellPos + addMoveDir;

			// 範囲外なら移動できない
			if (!_tileDataMap.InRange(destPos.x, destPos.y))
			{
				return false;
			}

			var tileFlag = _tileDataMap.GetTileFlag(destPos.x, destPos.y);

			// 移動できないフラグ
			if (tileFlag.HasFlag(chara.CanNotMoveTileFlag))
			{
				return false;
			}

			return true;
		}

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