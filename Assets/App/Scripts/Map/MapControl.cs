//
// MapControl.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.03
//

using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;
using Ling.Chara;
using Ling.Map.TileDataMapExtensions;
using Ling.Map.TileDataMapExtensionss.Chara;
using System.Linq;
using System.Collections.Generic;
using Utility.Extensions;

using Zenject;

namespace Ling.Map
{
	public enum OrderType : int
	{
		None,
			
		Map,
		Item,
		Chara
	}


	/// <summary>
	/// マップにオブジェクトの配置をする物
	/// </summary>
	public interface IMapObjectInstaller
	{
		/// <summary>
		/// マップにオブジェクトを配置する
		/// </summary>
		void PlaceObject(GameObject gameObject, int level, in Vector2Int cellPos, OrderType orderType = OrderType.Chara);
	}


	/// <summary>
	/// ダンジョンマップコントロール
	/// </summary>
	/// 
	[System.Serializable]
	public class MapControl : MonoBehaviour, IMapObjectInstaller
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private MapView _view = default;

		[Inject] private MasterData.IMasterHolder _masterManager = null;
		[Inject] private Chara.CharaManager _charaManager = null;

		private MapModel _model;
		private Tile.MapTile _mapTile;

		#endregion


		#region プロパティ

		public MapView View => _view;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void SetModel(MapModel model) =>
			_model = model;

		public void Startup(int curretMapIndex)
		{
			_model.ChangeMapByIndex(curretMapIndex);

			// 見た目の更新
			_view.OnStartGroundmapData = _view.OnUpdateItem =
				(groundMap_, mapIndex_) =>
				{
					var mapData = _model.FindMapData(mapIndex_);
					if (mapData == null)
					{
						Utility.Log.Error($"マップデータが見つからない {mapIndex_}");
						return;
					}

					groundMap_.BuildMap(mapIndex_, mapData.Width, mapData.Height, mapData.MapTileRenderData);
				};

			_view.Startup(_model, curretMapIndex, 40);
		}

		public void Resume()
		{
			foreach (var pair in _model.MapData)
			{
				var mapData = pair.Value;
				mapData.Resume();
			}
		}

		public void SetChara(Chara.ICharaController chara) =>
			SetChara(chara, _model.CurrentMapIndex);

		public void SetChara(Chara.ICharaController chara, int level)
		{
			var tilemap = FindTilemap(level);
			var mapData = _model.FindMapData(level);

			var charaModel = chara.Model;
			var charaView = chara.View;

			charaModel.SetMapLevel(level, mapData);
			charaView.gameObject.SetActive(true);

			switch (charaModel.CharaType)
			{
				case Chara.CharaType.Player:
					charaView.transform.SetParent(_view.PlayerRoot, worldPositionStays: false);
					break;

				case Chara.CharaType.Enemy:
					_view.SetEnemy(charaView, level);
					break;

				default:
					Utility.Log.Error($"キャラタイプが設定されていません");
					break;
			}

			chara.SetTilemap(tilemap, level);
		}

		public void SetCharaViewInCurrentMap(Chara.ICharaController chara) =>
			SetChara(chara, _model.CurrentMapIndex);

		/// <summary>
		/// 次マップの作成と移動を行う
		/// </summary>
		/// <param name="nextMapIndex"></param>
		/// <param name="createMapIndex"></param>
		/// <returns></returns>
		public void CreateMapView(int createMapIndex) =>
			_view.CreateMapView(createMapIndex);

		/// <summary>
		/// 現在のマップを一つ進め、変更する
		/// </summary>
		public void ChangeMap(int nextMapIndex)
		{
			// 現在のMapIndexを変更する
			_model.ChangeMapByIndex(nextMapIndex);

			_view.SetMapIndex(nextMapIndex);

			// もう見えないMapを削除する
			_view.RemoveExtraTilemap(_model.ShowMapIndexes);

			// 変化先のマップを中心とする
			_view.ForceTransformAdjustment(nextMapIndex);

			// プレイヤーの座標も初期化する
		}

		/// <summary>
		/// 指定したセルの中心ワールド座標を取得する
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public Vector3 GetCellCenterWorld(int mapIndex, int x, int y)
		{
			var tilemap = FindTilemap(mapIndex);

			return tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0));
		}

		/// <summary>
		/// 指定階層のTilemapを取得する
		/// </summary>
		/// <param name="mapIndex"></param>
		/// <returns></returns>
		public Tilemap FindTilemap(int level) =>
			_view.GetTilemap(level);

		/// <summary>
		/// 指定階層のGroundTilemapを検索する
		/// </summary>
		public Map.GroundTilemap FindGroundTilemap(int level) =>
			_view.FindGroundTilemap(level);

		/// <summary>
		/// 指定階層の指定座標のタイルデータを取得する
		/// </summary>
		public Map.TileData GetTileData(int level, int x, int y)
		{
			var tileDatamap = FindTileDataMap(level);
			if (tileDatamap == null)
			{
				Utility.Log.Error($"指定したレベルのマップが見つからない {level}");
				return null;
			}

			return tileDatamap.GetTile(x, y);
		}

		/// <summary>
		/// 指定階層のTile情報が詰まっているTileDataMapを検索
		/// </summary>
		public Map.TileDataMap FindTileDataMap(int level) =>
			_model.FindTileDataMap(level);

		public Map.TileDataMap FindTileDataMap(IMapObject mapObject) =>
			FindTileDataMap(mapObject.Level);

		/// <summary>
		/// 次のフロアに移動させる
		/// </summary>
		/// <remarks>
		/// MapView全体を上げる
		/// </remarks>
		public async UniTask MoveUpAsync()
		{
			var constMaster = _masterManager.Const;

			await _view.transform.DOLocalMoveY(constMaster.MapDiffHeight, constMaster.MapLevelMoveTime);
		}

		/// <summary>
		/// 動いたViewのY座標をもとに戻す
		/// </summary>
		public void ResetViewUpPosition()
		{
			var localPos = _view.transform.localPosition;
			_view.transform.localPosition = new Vector3(localPos.x, 0f, localPos.z);
		}

		/// <summary>
		/// ランダムな部屋の座標を取得する
		/// 他のキャラと被っていた場合は再抽選
		/// </summary>
		public Vector2Int GetRandomPosInRoom(int level)
		{
			var mapData = _model.FindMapData(level);
			Vector2Int pos = Vector2Int.zero;

			while (true)
			{
				pos = mapData.GetRandomPosInRoom();

				// キャラと被っていたら再抽選
				if (!_charaManager.ExistsCharaInPos(level, pos))
				{
					break;
				}
			}

			return pos;
		}

		/// <summary>
		/// 新しく生成されたMapDataを設定する
		/// </summary>
		public void SetMapData(int level, MapData mapData)
		{
			_model.SetMapData(level, mapData);

			// 新しく生成する
		}

		/// <summary>
		/// 引数のObjectをマップに配置する
		/// </summary>
		public void PlaceObject(GameObject gameObject, int level, in Vector2Int cellPos, OrderType orderType = OrderType.Chara)
		{
			var tilemap = FindTilemap(level);

			var worldPos = tilemap.GetCellCenterWorld(cellPos.ToVector3Int());
			gameObject.transform.position = worldPos;
			gameObject.transform.localRotation = Quaternion.identity;

			// SortingLayerとOrderを設定する
			_view.SetSortingLayerAndOrder(gameObject, level, orderType);
		}

		public DropItemController FindDropItemController(int level) =>
			_view.FindDropItemController(level);

		/// <summary>
		/// 指定したマップにアイテムをばらまく
		/// </summary>
		public void CreateItemObjectToMap(int level)
		{
			DeployItemObjectToMapInternal(level, isResume: false);
		}

		public void ResumeItemObjectToMap(int level)
		{
			DeployItemObjectToMapInternal(level, isResume: true);
		}


		#endregion


		#region private 関数

		private void DeployItemObjectToMapInternal(int level, bool isResume)
		{
			var mapData = _model.FindMapData(level);

			// 落とし物の生成
			// アイテムの配置を行う
			var mapMaster = _model.StageMaster.GetMapMasterByLevel(level);

			var dropItemController = FindDropItemController(level);
			dropItemController.Setup(this);
			dropItemController.SetMapInfo(level, mapMaster, mapData, FindTilemap(level));

			if (isResume)
			{
				dropItemController.Resume();
			}
			else
			{
				dropItemController.CreateAtBuild();
			}
		}
		
		private void Awake()
		{
			// TileFlagの更新
			this.AddEventListener<EventTileFlagUpdate>(ev_ =>
				{
					var tileData = GetTileData(ev_.level, ev_.x, ev_.y);

					switch (ev_.type)
					{
						case EventTileFlagUpdate.Type.Add:
							tileData.AddFlag(ev_.tileFlag);
							break;

						case EventTileFlagUpdate.Type.Remove:
							tileData.RemoveFlag(ev_.tileFlag);
							break;
					}
				});

			/// <summary>
			/// キャラを削除する
			/// </summary>
			this.AddEventListener<Chara.EventRemove>(ev_ =>
				{
					var chara = ev_.chara;
					var model = chara.Model;

					var tileFlag = model.CharaType.ToTileFlag();

					var tileData = GetTileData(model.MapLevel, model.CellPosition.Value.x, model.CellPosition.Value.y);
					tileData.RemoveFlag(tileFlag);
				});

			// キャラクタが移動した
			this.AddEventListener<Chara.EventPosUpdate>(ev_ =>
				{
					var tileFlag = ev_.charaType.ToTileFlag();

					// 以前の座標からフラグを削除する
					if (ev_.prevPos != null)
					{
						var tileData = GetTileData(ev_.mapLevel, ev_.prevPos.Value.x, ev_.prevPos.Value.y);
						tileData.RemoveFlag(tileFlag);
					}

					// 新しい座標にTileFlagを設定する
					var newTileData = GetTileData(ev_.mapLevel, ev_.newPos.x, ev_.newPos.y);
					newTileData.AddFlag(tileFlag);
				});

			// Mapが破棄された
			// todo: このイベント、Viewからきてるが、そもそも階層管理はControl側に任せるべきで改修しなければ
			this.AddEventListener<EventRemoveMap>(ev_ => 
				{
//					var elmenet = FindElement(ev_.level);
//					elmenet.Reset();
				});
		}

		/*
		private Element FindElement(int mapIndex)
		{
//			return System.Array.Find(_elements, element => element.MapIndex == mapIndex);
		}
*/

		#endregion
	}
}
