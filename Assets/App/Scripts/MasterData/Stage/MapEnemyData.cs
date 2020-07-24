//
// MapEnemyGroup.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.06
//

using Ling.Utility.Attribute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.MasterData.Stage
{
	/// <summary>
	/// １マップ上に出現する敵や確立データ
	/// </summary>
	[System.Serializable]
	public class MapEnemyData
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("敵の種類")] 
		private Const.EnemyType _enemyType = default;
		
		[SerializeField, FieldName("出現率")] 
		private int _popRate = default;

		#endregion


		#region プロパティ

		/// <summary>
		/// 出現する敵の種類
		/// </summary>
		public Const.EnemyType EnemyType => _enemyType;

		/// <summary>
		/// 出現率
		/// </summary>
		public int PopRate => _popRate;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
