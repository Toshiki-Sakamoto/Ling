//
// ItemMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.21
//

using UnityEngine;
using Ling.Utility.Attribute;

namespace Ling.MasterData.Item
{
	/// <summary>
	/// アイテムマスタ
	/// </summary>
	/// <remarks>
	/// アイテムに関する共通処理
	/// </remarks>
	public abstract class ItemMaster : MasterDataBase
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		/// <summary>
		/// アイテムカテゴリ
		/// </summary>
		public abstract Const.Item.Category Category { get; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
