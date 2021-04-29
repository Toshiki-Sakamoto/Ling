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
			[SerializeField, FieldName("Lv")]
			private int _lv = 0;

			[SerializeField, FieldName("経験値")]
			private int _exp = 0;

			[SerializeField, FieldName("ちから")]
			private int _power = 0;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("キャラクタID")]
		private int _id = default;

		[SerializeField, FieldName("テーブル")]
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
