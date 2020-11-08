// 
// ItemMaster.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.11.08
// 

using UnityEngine;
using Ling.Utility.Attribute;

namespace Ling.MasterData.Item
{
	/// <summary>
	/// Item Master
	/// </summary>
	[CreateAssetMenu(menuName = "MasterData/ItemMaster", fileName = "ItemMaster")]
	public class ItemMaster : MasterBase<ItemMaster>
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("アイテムのカテゴリ")]
		private Const.Item.Category _category;

		#endregion


		#region プロパティ

		public Const.Item.Category Category => _category;

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour


		#endregion
	}
}