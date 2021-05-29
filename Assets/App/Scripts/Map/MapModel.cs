//
// MapModel.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.05
//

using Ling.MasterData.Stage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using Zenject;

namespace Ling.Map
{
	/// <summary>
	/// Mapのデータを集める構造
	/// </summary>
	public class MapModel
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private StageMaster _stageMaster;

		#endregion


		#region プロパティ

		public StageMaster StageMaster => _stageMaster;
		
		[ES3Serializable] 
		public int AddMap { get; private set; }

		/// <summary>
		/// 現在のマップID(階層)
		/// </summary>
		[ES3Serializable]
		public int CurrentMapIndex { get; private set; }

		/// <summary>
		/// 現在のマップデータ
		/// </summary>
		[ES3Serializable]
		public MapData CurrentMapData { get; private set; }

		/// <summary>
		/// 現在のタイルデータ
		/// </summary>
		[ES3Serializable]
		public Map.TileDataMap CurrentTileDataMap { get; private set; }

		/// <summary>
		/// 現在見えているマップIndexリスト
		/// </summary>
		[ES3Serializable]
		public List<int> ShowMapIndexes { get; private set;  } = new List<int>();

		/// <summary>
		/// 全体のMapData
		/// </summary>
		[ES3Serializable]
		public Dictionary<int, MapData> MapData { get; private set; } = new Dictionary<int, MapData>();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(StageMaster stageMaster, int addMap)
		{
			_stageMaster = stageMaster;
			AddMap = addMap;
		}

		public void SetMapData(int level, MapData mapData)
		{
			// 現在のMapDataを上書きする
			MapData[level] = mapData;
		}

		public void ChangeMapByIndex(int level)
		{
			if (!MapData.ContainsKey(level))
			{
				Utility.Log.Error($"存在しないMap階層です {level}");
				return;
			}

			CurrentMapIndex = level;

			CurrentMapData = MapData[level];
			CurrentTileDataMap = CurrentMapData.TileDataMap;

			BuildShowMapIndexList();
		}

		public int GetCurrentMapIndex()
		{
			if (CurrentMapIndex - AddMap <= 0) return 0;

			return CurrentMapIndex;
		}

		public MapData FindMapData(int level)
		{
			if (MapData.TryGetValue(level, out MapData value))
			{
				return value;
			}

			return null;
		}

		public Map.TileDataMap FindTileDataMap(int level) =>
			FindMapData(level)?.TileDataMap;

		#endregion


		#region private 関数


		/// <summary>
		/// 現在見えているマップのIndexリストを作成する
		/// </summary>
		private void BuildShowMapIndexList()
		{
			ShowMapIndexes.Clear();

			var startIndex = Mathf.Max(CurrentMapIndex - AddMap, 0);

			for (int i = startIndex, count = CurrentMapIndex + AddMap; i <= count; ++i)
			{
				ShowMapIndexes.Add(i);
			}
		}


		#endregion
	}
}
