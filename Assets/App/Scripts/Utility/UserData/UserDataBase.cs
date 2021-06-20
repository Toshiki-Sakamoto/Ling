//
// UserDataBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.25
//

using UnityEngine;
using Utility.Attribute;
using Utility.GameData;

namespace Utility.UserData
{
	/// <summary>
	/// ユーザーデータ基礎クラス
	/// </summary>
	[System.Serializable]
	public abstract class UserDataBase : IGameDataBasic
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数


		[SerializeField] protected Utility.UniqKey _uniq = default;

		#endregion


		#region プロパティ

		public Utility.UniqKey Uniq  { get => _uniq; set => _uniq = value; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
