//
// MapMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.06.29
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Ling;
using Ling.Utility.Attribute;
using Zenject;

namespace Ling.MasterData.Stage
{
	/// <summary>
	/// １階層のデータ
	/// 　- 敵の出現率
	/// 　- 初期生成数
	/// 　...
	/// </summary>
	[CreateAssetMenu(menuName = "MasterData/MapMaster", fileName = "MapMaster")]
	public class MapMaster : Common.MasterData.MasterDataBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, MinMax(1, 50, fieldName: "敵の初期生成数")]
		private Common.MinMaxInt _initCreateNum = default;

		[SerializeField]
		private MapEnemyData[] _mapEnemyData = default;


		private int _popRateMaxParameter;   // 最大出現率の母数

		#endregion


		#region プロパティ

		/// <summary>
		/// 敵の初期生成数
		/// </summary>
		public Common.MinMaxInt InitCreateNum => _initCreateNum;

		/// <summary>
		/// 出現する敵のデータ
		/// </summary>
		public MapEnemyData[] MapEnemyData => _mapEnemyData;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void Setup()
		{
			// 出現する敵をあらかじめ出現率でマッピングしておく
			_popRateMaxParameter = _mapEnemyData.Sum(enemyData => enemyData.PopRate);
		}

		/// <summary>
		/// 出現率によりランダムで敵のデータを取得する
		/// </summary>
		public MapEnemyData GetRandomEnemyDataFromPopRate()
		{
			var targetValue = Utility.Random.IncludedMax(_popRateMaxParameter);
			var value = 0;

			foreach (var data in _mapEnemyData)
			{
				value += data.PopRate;

				if (value <= targetValue)
				{
					return data;
				}
			}

			return _mapEnemyData[_mapEnemyData.Count() - 1];
		}

		#endregion


		#region private 関数

		#endregion
	}
}
