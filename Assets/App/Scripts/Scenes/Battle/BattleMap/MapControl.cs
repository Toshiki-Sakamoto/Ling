//
// MapControl.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.03
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.BattleMap
{
	/// <summary>
	/// ダンジョンマップコントロール
	/// </summary>
	public class MapControl : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private MapView _view;

		private MapModel _model;
		private Common.Tile.MapTile _mapTile;

		#endregion


		#region プロパティ

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
			_view.OnStartItem = _view.OnUpdateItem = 
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

		/// <summary>
		/// 次マップの作成と移動を行う
		/// </summary>
		/// <param name="nextMapIndex"></param>
		/// <param name="createMapIndex"></param>
		/// <returns></returns>
		public IObservable<Unit> CreateAndMoveNextMap(int nextMapIndex, int createMapIndex)
		{
			_model.ChangeMapByIndex(nextMapIndex);

			return _view.CreateAndMoveNextMap(nextMapIndex, createMapIndex);
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
		public Tilemap FindTilemap(int mapIndex) =>
			_view.FindTilemap(mapIndex);

		/// <summary>
		/// 次のフロアに移動させる
		/// </summary>
		/// <remarks>
		/// MapView全体を上げる
		/// </remarks>
		public IEnumerator MoveUp()
		{
			var moveValue = 20.0f;

			var startTime = Time.timeSinceLevelLoad;

			bool isEnd = false;
			while (!isEnd)
			{
				yield return null;

				var diff = Time.timeSinceLevelLoad - startTime;
				if (diff > 0.2f/*manager.CellMoveTime*/)
				{
					diff = 1.0f;
					isEnd = true;
				}
				else
				{
					diff /= 0.2f/*manager.CellMoveTime*/;
				}

				var value = Mathf.Lerp(0.0f, moveValue, diff);

				_view.transform.localPosition = new Vector3(0.0f, value);
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
