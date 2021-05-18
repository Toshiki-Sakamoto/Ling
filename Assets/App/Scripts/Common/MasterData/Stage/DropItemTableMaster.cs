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
using System.Linq;

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

		private bool _isInitialized;
		private int _totalRate;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 落とし物の数を取得する
		/// </summary>
		public int GetRandomDropNum() =>
			Utility.Random.MaxIncludedRange(_min, _max);

		/// <summary>
		/// ランダムに落とし物を取得する
		/// </summary>
		/// <returns></returns>
		public Item.ItemMaster GetRandomItem()
		{
			Initialize();

			var index = Utility.Random.Range(_totalRate);
			var count = 0;

			foreach (var data in _data)
			{
				count += data.Rate;
				if (index < count)
				{
					return data.ItemMaster;
				}
			}

			Utility.Log.Error("確率が狂ってる");
			return null;
		}

		#endregion


		#region private 関数

		private void Initialize()
		{
			if (_isInitialized) return;

			_totalRate = _data.Sum(data => data.Rate);

			_isInitialized = true;
		}

		#endregion
	}
}
