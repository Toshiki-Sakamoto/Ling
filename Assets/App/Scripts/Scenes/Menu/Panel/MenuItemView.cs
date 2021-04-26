// 
// MenuItemView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.26
// 

using UnityEngine;
using UnityEngine.UI;

namespace Ling.Scenes.Menu.Panel
{
	/// <summary>
	/// アイテム１つの見た目
	/// </summary>
	public class MenuItemView : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Text _titleText = default;

		private Common.Item.ItemEntity _itemEntity;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void Setup(Common.Item.ItemEntity entity)
		{
			_itemEntity = entity;
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}