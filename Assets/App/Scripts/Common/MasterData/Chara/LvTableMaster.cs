//
// LvTableMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.29
//

using UnityEngine;
using Utility.MasterData;
using Utility.Attribute;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Ling.MasterData.Chara
{
	/// <summary>
	/// レベルテーブル
	/// </summary>
	[CreateAssetMenu(menuName = "MasterData/LvTable", fileName = "LvTableMaster")]
	public class LvTableMaster : MasterDataBase
	{
		#region 定数, class, enum

		[System.Serializable]
		public class Data
		{
			[TableColumnWidth(20)]
			public int Lv;

			[TableColumnWidth(30)]
			public int Exp;

			[TableColumnWidth(20)]
			public int Power;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, TableList]
		private List<Data> _data = default;

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
