//
// EquipmentUserData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.05
//

using UnityEngine;

namespace Ling.UserData.Equipment
{
	/// <summary>
	/// 装備データ
	/// </summary>
	public class EquipmentUserData : Utility.GameData.IGameDataBasic
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private int _id;
		[SerializeField] private Const.Equipment.Category _category;

		#endregion


		#region プロパティ

		public int ID { get => _id; set => _id = value; }

		public Const.Equipment.Category Category { get => _category; set => _category = value; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
