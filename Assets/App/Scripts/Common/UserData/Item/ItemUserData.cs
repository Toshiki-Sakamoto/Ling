//
// ItemUserData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using Ling.MasterData.Item;
using Ling.Common.Item;
using Zenject;
using UnityEngine;

namespace Ling.UserData.Item
{
	/// <summary>
	/// ユーザーが持っているアイテムデータ
	/// </summary>
	public class ItemUserData : Utility.GameData.IGameDataBasic
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private ItemEntity _entity = default;

		#endregion


		#region プロパティ

		public ItemEntity Entity => _entity;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数


		#endregion


		#region private 関数

		#endregion
	}
}
