//
// EquipmentMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.05
//

using UnityEngine;
using Utility.Attribute;
using Utility.MasterData;

namespace Ling.MasterData.Equipment
{
	/// <summary>
	/// 装備マスタの基本
	/// </summary>
	public abstract class EquipmentMaster : MasterDataBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("名前")]
		private string _name = default;

		[SerializeField, FieldName("説明")]
		private string _desc = default;

		#endregion


		#region プロパティ

		public abstract Const.Equipment.Type Type { get; }

		/// <summary>
		/// アイテム名
		/// </summary>
		public string Name => _name;

		/// <summary>
		/// 詳細
		/// </summary>
		public string Desc => _desc;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
