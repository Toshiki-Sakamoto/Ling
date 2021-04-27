// 
// MenuItemListView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.26
// 

using UnityEngine;
using Utility.UI;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Ling.Scenes.Menu.Panel
{
	/// <summary>
	/// アイテムリストView
	/// </summary>
	public class MenuItemListView : MonoBehaviour,
		RecyclableScrollView.IContentDataProvider
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private RecyclableScrollView _scrollView = default;
		[SerializeField] private GameObject _itemObject = default;

		private List<Common.Item.ItemEntity> _entities;
		private float _itemSize;

		#endregion


		#region プロパティ

		int RecyclableScrollView.IContentDataProvider.DataCount => _entities.Count;

		public System.Action<Common.Item.ItemEntity> OnClick { get; set; }

		#endregion


		#region public, protected 関数

		public void Setup(IEnumerable<Common.Item.ItemEntity> entities)
		{
			_entities = entities.ToList();
			_itemSize = _itemObject.GetComponent<RectTransform>().sizeDelta.y;

			_scrollView.Initialize(this);
		}

		float RecyclableScrollView.IContentDataProvider.GetItemSize(int index) =>
			_itemSize;

		GameObject RecyclableScrollView.IContentDataProvider.GetItemObj(int index) =>
			_itemObject;

		void RecyclableScrollView.IContentDataProvider.ScrollItemUpdate(int index, GameObject obj, bool init)
		{
			var itemEntity = _entities[index];

			var item = obj.GetComponent<MenuItemView>();
			item.Setup(itemEntity);

			if (init)
			{
				item.OnClick
					.Subscribe(_ => OnClick?.Invoke(item.Entity));
			}
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour


		#endregion
	}
}