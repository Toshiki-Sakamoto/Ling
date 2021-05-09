//
// DropItemTableMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.09
//

using UnityEngine;
using Utility.MasterData;
using Utility.Attribute;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Ling.MasterData.Stage
{
	/// <summary>
	/// マップ上の落とし物テーブルマスタ
	/// </summary>
	[CreateAssetMenu(menuName = "MasterData/DropItemTable", fileName = "DropItemTableMaster")]
	public class DropItemTableMaster : MasterDataBase
	{
		#region 定数, class, enum

		[System.Serializable]
		public class Data
		{
			/// <summary>
			/// アイテムマスタ
			/// </summary>
			[TableColumnWidth(10)]
			public Item.ItemMaster ItemMaster;

			/// <summary>
			/// 確率
			/// </summary>
			[TableColumnWidth(10)]
			public int Rate;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("最小ドロップ数")]
		private int _min = default;

		[SerializeField, FieldName("最大ドロップ数")]
		private int _max = default;

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
