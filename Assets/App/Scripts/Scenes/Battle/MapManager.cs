﻿// 
// MapView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.01
// 
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using Zenject;


namespace Ling.Scenes.Battle
{
	/// <summary>
	/// Map管理者
	/// ミニマップも管理させる
	/// 
	/// マップにしてがなければ現在のマップを参照する
	/// </summary>
	public class MapManager : Utility.MonoSingleton<MapManager> 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Transform _root = null;
		[SerializeField] private BattleMap.MapControl _control = null;

		[Inject] private Map.Builder.IManager _builderManager = null;
		[Inject] private Map.Builder.BuilderFactory _builderFactory = null;

		private BattleMap.MapModel _mapModel;

		#endregion


		#region プロパティ

		/// <summary>
		/// 現在のマップIndex(階層)
		/// </summary>
		public int CurrentMapIndex => _mapModel.CurrentMapIndex;

		/// <summary>
		/// 現在の<see cref="Map.TileDataMap"/>
		/// </summary>
		public Map.TileDataMap CurrentTileDataMap => _mapModel.CurrentTileDataMap;

		/// <summary>
		/// 現在の<see cref="BattleMap.MapData"/>
		/// </summary>
		public BattleMap.MapData CurrentMapData => _mapModel.CurrentMapData;

		/// <summary>
		/// 現在のTilemapを取得する
		/// </summary>
		public Tilemap CurrentTilemap => MapControl.FindTilemap(CurrentMapIndex);

		/// <summary>
		/// Map/MiniMapの管理
		/// </summary>
		public BattleMap.MapControl MapControl => _control;
		public BattleMap.MiniMapControl MiniMapControl { get; private set; }

		public Tilemap MiniMapTilemap => MiniMapControl.Tilemap;

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 指定したマップを現在のマップとして設定する
		/// </summary>
		/// <param name="mapIndex"></param>
		public void SetupCurrentMap(int mapIndex)
		{
			// 初期マップの設定
			MapControl.Startup(mapIndex);

			// ミニマップの設定
			MiniMapControl.Setup(_mapModel.CurrentTileDataMap);
		}

		/// <summary>
		/// 次のマップに移動する
		/// </summary>
		public IObservable<Unit> CreateAndMoveNextMap()
		{
			var nextMapIndex = CurrentMapIndex + 1;

			return MapControl.CreateAndMoveNextMap(nextMapIndex, nextMapIndex + BattleConst.AddShowMap);
		}

		/// <summary>
		/// マップから指定したセルのワールド座標を取得する
		/// </summary>
		public Vector3 GetCellCenterWorldByMap(int x, int y) =>
			GetCellCenterWorldByMap(CurrentMapIndex, x, y);

		public Vector3 GetCellCenterWorldByMap(int mapIndex, int x, int y) =>
			MapControl.GetCellCenterWorld(mapIndex, x, y);

		/// <summary>
		/// キャラが移動できるか
		/// </summary>
		/// <param name="chara"></param>
		public bool CanMoveChara(Chara.Base chara, in Vector3Int addMoveDir) =>
			CanMoveChara(CurrentMapIndex, chara, addMoveDir);

		public bool CanMoveChara(int mapIndex, Chara.Base chara, in Vector3Int addMoveDir)
		{
			var tileDataMap = _mapModel.FindTileDataMap(mapIndex);
			var destPos = chara.CellPos + addMoveDir;

			// 範囲外なら移動できない
			if (!tileDataMap.InRange(destPos.x, destPos.y))
			{
				return false;
			}

			var tileFlag = tileDataMap.GetTileFlag(destPos.x, destPos.y);

			// 移動できないフラグ
			if (tileFlag.HasFlag(chara.CanNotMoveTileFlag))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// 指定した階層のマップを作成する
		/// </summary>
		public IObservable<AsyncUnit> BuildMap(params int[] mapIDs)
		{
			// 存在する場合は削除して作成する.. か？
			return LoadAsync(mapIDs).ToObservable();
		}

		public IObservable<AsyncUnit> BuildNextMap()
		{
			return BuildMap(CurrentMapIndex + BattleConst.AddShowMap + 1);
		}

		#endregion


		#region private 関数

		/// <summary>
		/// 非同期マップ読み込み
		/// </summary>
		/// <param name="mapIDs"></param>
		/// <returns></returns>
		private async UniTask LoadAsync(int[] mapIDs)
		{
			foreach (var mapID in mapIDs)
			{
				var builderData = new Map.Builder.BuilderData();

				var builder = _builderFactory.Create(Map.Builder.BuilderConst.BuilderType.Split);
				builder.Initialize(20, 20);

				_builderManager.SetData(builderData);
				_builderManager.SetBuilder(builder);

				// マップの作成
				await builder.Execute();

				var mapData = new BattleMap.MapData();
				mapData.Setup(builder, builder.TileDataMap);

				// すでに存在する場合も上書きする
				_mapModel.SetMapData(mapID, mapData);
			}
		}

		#endregion


		#region MonoBegaviour


		protected override void Awake()
		{
			base.Awake();

			_mapModel = new BattleMap.MapModel();

			MapControl.SetModel(_mapModel);

			MiniMapControl = new BattleMap.MiniMapControl();
		}

		#endregion
	}
}