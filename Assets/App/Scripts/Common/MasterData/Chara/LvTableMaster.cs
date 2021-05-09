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
			[TableColumnWidth(10)]
			public int Lv;

			[TableColumnWidth(10)]
			public int Exp;

			[TableColumnWidth(10)]
			public long Power;

			[TableColumnWidth(10)]
			public long Defence;

			[TableColumnWidth(10)]
			public long Hp;
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

		/// <summary>
		/// 渡された経験値から現在のLvTableデータを返す
		/// </summary>
		public Data GetDataByExp(int exp)
		{
			// 次のLv-1
			var index = GetNextDataIndexByExp(exp);
			return _data[index - 1];
		}

		public Data GetNextDataByExp(int exp)
		{
			var index = GetNextDataIndexByExp(exp);
			return _data[index];
		}

		#endregion


		#region private 関数

		private int GetNextDataIndexByExp(int exp)
		{
			// あるLvテーブルの経験値よりも低い場所が出てきた時終了
			return _data.FindIndex(data => exp < data.Exp);
		}

		#endregion
	}
}
