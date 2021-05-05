//
// MenuEquipmentListView.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.05
//

using UnityEngine;
using Utility.UI;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Ling.Scenes.Menu.Panel
{
	/// <summary>
	/// 装備
	/// </summary>
	public class MenuEquipmentListView : MonoBehaviour,
		RecyclableScrollView.IContentDataProvider
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private RecyclableScrollView _scrollView = default;
		[SerializeField] private GameObject _itemObject = default;

		private List<UserData.Equipment.EquipmentUserData> _entities;
		private float _itemSize;

		#endregion


		#region プロパティ

		int RecyclableScrollView.IContentDataProvider.DataCount => _entities.Count;

		public System.Action<UserData.Equipment.EquipmentUserData> OnClick { get; set; }

		#endregion


		#region public, protected 関数

		public void Setup(IEnumerable<UserData.Equipment.EquipmentUserData> entities)
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
			var entity = _entities[index];

			var item = obj.GetComponent<MenuEquipmentView>();
			item.Setup(entity);

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
