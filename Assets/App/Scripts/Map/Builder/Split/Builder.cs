//
// Builder.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.22
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Zenject;


namespace Ling.Map.Builder.Split
{
	/// <summary>
	/// 
	/// </summary>
	public class Builder : BuilderBase 
	{
		#region 定数, class, enum

		private static readonly int[,] Dir = new int[4, 2] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

		public class Factory : PlaceholderFactory<Builder> {}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private SplitBuilderFactory _splitFactory = null;		// 部屋の分割担当
		[Inject] private Road.SplitRoadBuilderFactory _roadFactory = null;	// 道を作る担当

		private ISplitter _splitter = null;
		private Road.ISplitRoadBuilder _roadBuilder = null;

		#endregion


		#region プロパティ

		public MapRect MapRect { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ


		#endregion


		#region public, protected 関数

		/// <summary>
		/// 処理を実行する
		/// </summary>
		protected override IEnumerator<float> ExecuteInternal(TileDataMap tildeDataMap)
		{
			_splitter = _splitFactory.Create();

			MapRect = new MapRect();

			// 全体を一つの区画にする
			// Width&Heightが20の場合
			// (0, 0, 20, 20)になる。
			// 区画が (0, 0, 10, 20), (10, 0, 20, 20)
			// となった場合、引き算するだけで
			// 10 - 0 = 10
			// 20 - 10 = 10 
			// とマスの幅が求まり
			// 　　for (int x = 0; x < w; ++x)
			// とすると区画のループ処理がかける
			MapRect.CreateRect(0, 0, Width, Height);

			// 区画を作る
			var splitEnumerator = _splitter?.SplitRect(_data, MapRect);
			while (splitEnumerator.MoveNext())
			{
				yield return splitEnumerator.Current;
			}

			// 部屋を作る
			var roomEnumerator = CreateRoom();
			while (roomEnumerator.MoveNext())
			{
				yield return roomEnumerator.Current;
			}

			// 道を作る
			_roadBuilder = _roadFactory.Create(SplitConst.RoadBuilderType.Simple);

			var roadEnumerator = _roadBuilder.Build(TileDataMap, MapRect);
			while (roadEnumerator.MoveNext())
			{
				yield return roadEnumerator.Current;
			}

			// 下階段を作る
			var pos = GetRandomRoomCellPos();
			TileDataMap.SetStepDownFlag(pos.x, pos.y);
		}

		protected override IEnumerator<float> ExecuteFurtherInternal(TileDataMap prevTildeDataMap)
		{
			if (prevTildeDataMap == null)
			{
				yield break;
			}

			// 前マップの下階段の位置に部屋を作成する
			// 下り階段の場所が部屋なら何もしない
			var stepDownPos = prevTildeDataMap.StepDownPos;

			var tileData = TileDataMap.GetTile(stepDownPos.x, stepDownPos.y);
			if (tileData.HasFlag(TileFlag.Floor))
			{
				yield break;
			}

			// 3 ～ 5 マスの範囲で部屋を作成
			var w = Utility.Random.Range(3, 5);
			var h = Utility.Random.Range(3, 5);

			// どこかの部屋か道と連結したら終わり
			var xMin = stepDownPos.x - w / 2;
			var xMax = stepDownPos.x + w / 2;
			var yMin = stepDownPos.y - h / 2;
			var yMax = stepDownPos.y + h / 2;

			bool shouldCreateRoad = true;

			TileDataMap.FillRect(xMin, yMin, xMax, yMax, TileFlag.Floor, 
				tileData_ =>
				{
					// 自分自身が道か道路にあたったら道作るのをスキップ
					if (tileData_.HasFlag(TileFlag.Floor | TileFlag.Road))
					{
						shouldCreateRoad = false;
						return false;
					}

					// 上下左右、隣接も確認
					if (shouldCreateRoad)
					{
						for (int i = 0; i < 4; ++i)
						{
							var x = tileData_.X + Dir[i, 0];
							var y = tileData_.Y + Dir[i, 1];

							var tileFlag = TileDataMap.GetTileFlag(x, y);
							if (tileFlag.HasFlag(TileFlag.Floor | TileFlag.Road))
							{
								shouldCreateRoad = false;

								// 道は作る
								return true;
							}
						}
					}

					return true; 
				});

			// 部屋のマップを作成する
			TileDataMap.BuildRoomMap();

			// 道を作る必要がある場合作成
			if (shouldCreateRoad)
			{
				// 部屋マップの値を取得
				var roomValue = TileDataMap.RoomMap[stepDownPos.y * Width + stepDownPos.x];

				// 一番近い部屋か道とつなげる
				var route = new Route.Tracking();
				route.Setup(TileDataMap);

				// 自分の部屋は1とする。
				// 自分以外の部屋か道が見つかったら終わり
				route.Execute(stepDownPos.x, stepDownPos.y, 
					(tracking_, cellPos_, newValue_, oldValue_) =>
					{
						bool existMyRoom = false;
						bool existRoad = false;

						// 自分の部屋か
						if (TileDataMap.GetRoomMapValue(cellPos_.x, cellPos_.y) == roomValue)
						{
							// すでに書き込まれていたらスキップ
							if (oldValue_ == 1) return -1;

							// 自分の部屋は 1 として書き込む
							return 1;
						}

						// 自分が２(部屋の外周)じゃないのに部屋の隣りにいるならばだめ
						if (newValue_ > 2)
						{
							for (int i = 0; i < 4; ++i)
							{
								var x = cellPos_.x + Dir[i, 0];
								var y = cellPos_.y + Dir[i, 1];

								// 自分の部屋があればおわり
								if (TileDataMap.GetRoomMapValue(x, y) == roomValue)
								{
									return -1;
								}
							}
						}

						// 前後左右が部屋か道ならおわり
						for (int i = 0; i < 4; ++i)
						{
							var x = cellPos_.x + Dir[i, 0];
							var y = cellPos_.y + Dir[i, 1];

							if (!TileDataMap.InRange(x, y))
							{
								continue;
							}

							// 部屋か道を見つける
							var tileFlag = TileDataMap.GetTileFlag(x, y);
							if (tileFlag.HasFlag(TileFlag.Floor | TileFlag.Road))
							{
								tracking_.ProcessFinish();
								break;
							}
						}

						return 0; 
					});

				// ルートを道にする
				var posList = route.GetRoute(route.ForceFinishPos, useDiagonal: false);

				foreach(var pos in posList)
				{
					// 部屋なら何もしない
					if (TileDataMap.GetTileFlag(pos.x, pos.y).HasFlag(TileFlag.Floor))
					{
						continue;
					}

					TileDataMap.SetTileFlag(pos, TileFlag.Road);
				}
			}

			yield break;
		}

		/// <summary>
		/// プレイヤーの初期座標をランダムに取得する
		/// </summary>
		/// <returns></returns>
		public override Vector3Int GetPlayerInitPosition()
		{
			// 数ある部屋の中から一つ選び、ランダムに配置する
			var pos = GetRandomRoomCellPos();
			return new Vector3Int(pos.x, pos.y, 0);
		}

		#endregion


		#region private 関数

		private (int x, int y) GetRandomRoomCellPos()
		{
			var mapData = MapRect.GetRandomData();
			var room = mapData.room;

			var x = Utility.Random.Range(room.xMin, room.xMax - 1);
			var y = Utility.Random.Range(room.yMin, room.yMax - 1);

			return (x, y);
		}

		/// <summary>
		/// 部屋を作成する
		/// </summary>
		/// <returns></returns>
		private IEnumerator<float> CreateRoom()
        {
            for (int i = 0; i < MapRect.RectCount; ++i)
            {
				var rectData = MapRect[i];
				var rect = rectData.rect;

				// 矩形の大きさを計算
				var w = rect.width - 3;
				var h = rect.height - 3;

				// 区画に入る最小部屋の余裕を求める
				var cw = w - _data.RoomMinSize;
				var ch = h - _data.RoomMinSize;

				// 部屋の大きさを決定する
				var sw = UnityEngine.Random.Range(0, cw + 1);
				var sh = UnityEngine.Random.Range(0, ch + 1);
				var rw = w - sw;
				var rh = h - sh;

				// 部屋の位置を決定する
				var rx = UnityEngine.Random.Range(0, sw + 1) + 2;
				var ry = UnityEngine.Random.Range(0, sh + 1) + 2;

				// 求めた結果から部屋の情報を設定
				rectData.room.xMin = rect.xMin + rx;
				rectData.room.yMin = rect.yMin + ry;
				rectData.room.xMax = rect.xMin + rx + rw;
				rectData.room.yMax = rect.yMin + ry + rh;

				var room = rectData.room;

                // 部屋を作る
				TileDataMap.FillRect(room.xMin, room.yMin, room.xMax, room.yMax, TileFlag.Floor);

				yield return 0.5f;
			}
        }

		#endregion
	}
}
