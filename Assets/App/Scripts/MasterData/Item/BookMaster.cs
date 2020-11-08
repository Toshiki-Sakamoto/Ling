//
// BoolRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.09
//

using UnityEngine;
using Ling.Utility.Attribute;

namespace Ling.MasterData.Item
{
	/// <summary>
	/// スキル本 Master
	/// </summary>
	[CreateAssetMenu(menuName = "MasterData/BookMaster", fileName = "BookMaster")]
	public class BookMaster : MasterBase<BookMaster>
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("種類")]
		private Const.Item.Book _type = default;

		#endregion


		#region プロパティ

		public Const.Item.Book Type => _type;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
