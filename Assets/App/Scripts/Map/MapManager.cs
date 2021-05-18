// 
// MapView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.01
// 
using Cysharp.Threading.Tasks;
using Ling.MasterData.Stage;
using UnityEngine;
using UnityEngine.Tilemaps;
using Ling.Map.TileDataMapExtensions;
using Ling.Const;
using Utility.Extensions;

using Zenject;


namespace Ling.Map
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

		[SerializeField] private MapControl _control = default;
		[SerializeField] private MiniMapControl _minimapControl = default;

		[Inject] private Builder.IManager _builderManager = default;
		[Inject] private Builder.BuilderFactory _builderFactory = default;

		private MapModel _mapModel;
		
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
		public Map.MapData CurrentMapData => _mapModel.CurrentMapData;

		/// <summary>
		/// 現在のTilemapを取得する
		/// </summary>
		public Tilemap CurrentTilemap => MapControl.FindTilemap(CurrentMapIndex);

		/// <summary>
		/// 最後に作成したマップのレベル
		/// </summary>
		public int LastBuildMapLevel { get; private set; }

		/// <summary>
		/// Map/MiniMapの管理
		/// </summary>
		public Map.MapControl MapControl => _control;
		public Map.MapView MapView => MapControl.View;

		public Map.MiniMapControl MiniMapControl => _minimapControl;
		public Map.MiniMapView MiniMapView => _minimapControl.View;

		#endregion


		#region public, protected 関数

		public void Setup(StageMaster stageMaster)
		{
			_mapModel.Setup(stageMaster, 1);
		}

		/// <summary>
		/// 指定したマップを現在のマップとして設定する
		/// </summary>
		/// <param name="mapIndex"></param>
		public void SetCurrentMap(int mapIndex)
		{
			// 初期マップの設定
			MapControl.Startup(mapIndex);

			// ミニマップの設定
			MiniMapControl.Setup(_mapModel.CurrentTileDataMap);
		}

		/// <summary>
		/// 次のマップViewを作成する
		/// </summary>
		public void CreateMapView(int level) =>
			MapControl.CreateMapView(level);

		/// <summary>
		/// マップから指定したセルのワールド座標を取得する
		/// </summary>
		public Vector3 GetCellCenterWorldByMap(int x, int y) =>
			GetCellCenterWorldByMap(CurrentMapIndex, x, y);

		public Vector3 GetCellCenterWorldByMap(int mapIndex, int x, int y) =>
			MapControl.GetCellCenterWorld(mapIndex, x, y);

		public Vector3 GetCellCenterWorldByMap(int mapIndex, in Vector2Int cellPos) =>
			GetCellCenterWorldByMap(mapIndex, cellPos.x, cellPos.y);

		/// <summary>
		/// キャラが移動できるか
		/// </summary>
		public bool CanMoveChara(Chara.CharaModel charaModel, in Vector2Int addMoveDir) =>
			CanMoveChara(CurrentMapIndex, charaModel, addMoveDir);

		public bool CanMoveChara(int mapIndex, Chara.CharaModel charaModel, in Vector2Int addMoveDir)
		{
			var tileDataMap = _mapModel.FindTileDataMap(mapIndex);
			var pos = charaModel.CellPosition.Value;

			System.Func<int, int, Const.TileFlag, bool> canMove = (x, y, checkFlag) =>
				{
					// 範囲外なら移動できない
					if (!tileDataMap.InRange(x, y))
					{
						return false;
					}

					var tileFlag = tileDataMap.GetTileFlag(x, y);

					// 移動できないフラグ
					// 斜めの場合、上下左右も問題ない時のみ移動できる
					if (tileFlag.HasAny(checkFlag))
					{
						return false;
					}

					return true;
				};

			// 移動先
			if (!canMove(pos.x + addMoveDir.x, pos.y + addMoveDir.y, charaModel.UnmovableTileFlag)) return false;

			// 斜めの場合
			if (addMoveDir.x != 0 && addMoveDir.y != 0)
			{
				if (!canMove(pos.x + addMoveDir.x, pos.y, charaModel.UnDiagonalMovableTileFlag)) return false;
				if (!canMove(pos.x, pos.y + addMoveDir.y, charaModel.UnDiagonalMovableTileFlag)) return false;
			}

			return true;
		}

		/// <summary>
		/// 指定した階層のマップを作成する
		/// </summary>
		public UniTask BuildMapAsync(params int[] mapIDs)
		{
			// 存在する場合は削除して作成する.. か？
			return LoadAsync(mapIDs);
		}

		public UniTask BuildNextMapAsync()
		{
			LastBuildMapLevel = CurrentMapIndex + _mapModel.AddMap + 1;

			return BuildMapAsync(LastBuildMapLevel);
		}

		/// <summary>
		/// 次の階層に変化する
		/// </summary>
		public void ChangeNextLevel(int level)
		{
			_control.ChangeMap(level);

			// 座標をもとに戻す
			_control.ResetViewUpPosition();
		}

		/// <summary>
		/// 指定レベルのTilemapが存在する場合取得する
		/// </summary>
		public Tilemap FindTilemap(int level) =>
			MapControl.FindTilemap(level);

		public Map.GroundTilemap FindGroundTilemap(int level) =>
			MapControl.FindGroundTilemap(level);

		/// <summary>
		/// Map情報を更新する
		/// </summary>
		public void UpdateMapData()
		{
			// 部屋情報
			UpdateRoomDataMap();
		}

		#endregion


		#region private 関数

		/// <summary>
		/// 非同期マップ読み込み
		/// </summary>
		private async UniTask LoadAsync(int[] mapLevels)
		{
			foreach (var mapLevel in mapLevels)
			{
				var builderData = new Map.Builder.BuilderData();

				var builder = _builderFactory.Create(Map.Builder.BuilderConst.BuilderType.Split);
				builder.Initialize(30, 30, mapLevel);

				_builderManager.SetData(builderData);
				_builderManager.SetBuilder(builder);

				// 一つ下のマップ
				var prevTileDataMap = _mapModel.FindTileDataMap(mapLevel - 1);

				// マップの作成
				await builder.Execute(prevTileDataMap);

				// 追加でマップ作成する必要があるとき
				await builder.ExecuteFurher(prevTileDataMap);

				var mapData = new Map.MapData();
				mapData.Setup(builder, builder.TileDataMap);

				_control.SetMapData(mapLevel, mapData);
			}
		}

		/// <summary>
		/// 各マップの部屋情報を更新する
		/// </summary>
		private void UpdateRoomDataMap()
		{
			foreach (var mapData in _mapModel.MapData.Values)
			{
				mapData.TileDataMap.UpdateRoomData();
			}
		}

		#endregion


		#region MonoBegaviour


		protected override void Awake()
		{
			base.Awake();

			_mapModel = new Map.MapModel();

			MapControl.SetModel(_mapModel);
		}

		#endregion
	}
}