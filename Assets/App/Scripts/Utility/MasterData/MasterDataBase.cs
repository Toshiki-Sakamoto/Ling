//
// MasterBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.26
//

using UnityEngine;
using Utility.Attribute;
using Utility.GameData;

namespace Utility.MasterData
{
	/// <summary>
	/// 基本的なデータを持つ
	/// </summary>
	public class MasterDataBase : IGameDataBasic
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数


		[SerializeField, FieldName("ID")]
		private int _id = default;

		#endregion


		#region プロパティ

		public int ID => _id;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
