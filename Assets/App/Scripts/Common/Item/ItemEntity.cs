//
// ItemEntity.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.26
//

using System;
using UnityEngine;
using Zenject;
using Ling.MasterData.Item;

namespace Ling.Common.Item
{
	/// <summary>
	/// 基本的なアイテムデータ
	/// </summary>
	[System.Serializable]
	public class ItemEntity
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private int _id;
		[SerializeField] private Const.Item.Category _category;

		private ItemMaster _master;

		#endregion


		#region プロパティ

		public int ID { get => _id; set => _id = value; }

		public Const.Item.Category Category { get => _category; set => _category = value; }

		public string Name => Master?.Name;

		public ItemMaster Master
		{
			get
			{
				if (_master != null) return _master;

				var holder = GameManager.Instance.MasterHolder;
				_master = holder.ItemRespositoryContainer.Find(Category, _id);

				return _master;
			}
		}

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
