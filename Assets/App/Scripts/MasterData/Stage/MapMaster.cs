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
using Ling.Common.Attribute;
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
	public class MapMaster : MasterBase<MapMaster>
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, MinMax(1, 50, fieldName: "敵の初期生成数")]
		private Common.MinMaxInt _initCreateNum = default;

		[SerializeField, FieldName("敵の最大生成数")]
		private int _enemyMaxCreateNum = default;

		[SerializeField]
		private MapEnemyData[] _mapEnemyData = default;

		#endregion


		#region プロパティ

		/// <summary>
		/// 敵の初期生成数
		/// </summary>
		public Common.MinMaxInt InitCreateNum => _initCreateNum;

		/// <summary>
		/// 敵の最大生成数
		/// </summary>
		public int EnemyMaxCreateNum => _enemyMaxCreateNum;

		/// <summary>
		/// 出現する敵のデータ
		/// </summary>
		public MapEnemyData[] MapEnemyData => _mapEnemyData;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
