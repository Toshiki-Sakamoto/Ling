//
// MapModel.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.05
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.BattleMap
{
	/// <summary>
	/// 
	/// </summary>
	public class MapModel
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Dictionary<int, MapData> _mapData = new Dictionary<int, MapData>();

		#endregion


		#region プロパティ

		/// <summary>
		/// 現在のマップID(階層)
		/// </summary>
		public int CurrentMapIndex { get; private set; }

		/// <summary>
		/// 現在のマップデータ
		/// </summary>
		public BattleMap.MapData CurrentMapData { get; private set; }

		/// <summary>
		/// 現在のタイルデータ
		/// </summary>
		public Map.TileDataMap CurrentTileDataMap { get; private set; }

		/// <summary>
		/// 現在見えているマップIndexリスト
		/// </summary>
		public List<int> ShowMapIndexes { get; } = new List<int>();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void SetMapData(int mapID, MapData mapData)
		{
			// 現在のMapDataを上書きする
			_mapData[mapID] = mapData;
		}

		public void ChangeMapByIndex(int mapIndex)
		{
			if (!_mapData.ContainsKey(mapIndex))
			{
				Utility.Log.Error($"存在しないMap階層です {mapIndex}");
				return;
			}

			CurrentMapIndex = mapIndex;

			CurrentMapData = _mapData[mapIndex];
			CurrentTileDataMap = CurrentMapData.TileDataMap;

			BuildShowMapIndexList();
		}

		public int GetCurrentMapIndex()
		{
			if (CurrentMapIndex - BattleConst.AddShowMap <= 0) return 0;

			return CurrentMapIndex;
		}

		public MapData FindMapData(int mapIndex)
		{
			if (_mapData.TryGetValue(mapIndex, out MapData value))
			{
				return value;
			}

			return null;
		}

		public Map.TileDataMap FindTileDataMap(int mapIndex)
		{
			if (_mapData.TryGetValue(mapIndex, out MapData value))
			{
				return value.TileDataMap;
			}

			return null;
		}

		#endregion


		#region private 関数


		/// <summary>
		/// 現在見えているマップのIndexリストを作成する
		/// </summary>
		private void BuildShowMapIndexList()
		{
			ShowMapIndexes.Clear();

			var startIndex = Mathf.Max(CurrentMapIndex - BattleConst.AddShowMap, 0);

			for (int i = startIndex, count = CurrentMapIndex + BattleConst.AddShowMap; i <= count; ++i)
			{
				ShowMapIndexes.Add(i);
			}
		}


		#endregion
	}
}
