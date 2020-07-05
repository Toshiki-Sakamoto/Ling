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

namespace Ling.MasterData
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

		[SerializeField, MinMax(1, 100, fieldName: "初期生成数")]
		private Common.MinMaxInt _initCreateNum = default;

		[SerializeField, Utage.MinMax(1, 100)]
		private Utage.MinMaxInt aaa;

		[SerializeField]
		private int _a = 0;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
