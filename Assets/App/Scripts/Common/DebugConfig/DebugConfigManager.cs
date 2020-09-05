// 
// DebugConfigManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.23
// 
using Ling.Utility.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

using Zenject;
using UniRx;

#if DEBUG
namespace Ling.Common.DebugConfig
{
	public interface IDebugItemData
	{
		/// <summary>
		/// データの更新処理が入った
		/// </summary>
		/// <param name="obj"></param>
		void DataUpdate(GameObject obj);

		Const.MenuType GetMenuType();
	}

	/// <summary>
	/// 全てのItemのDataベースクラス
	/// </summary>
	public abstract class DebugItemDataBase<T> : IDebugItemData
	{
		/// <summary>
		/// 表示されるアイテムのタイトル
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// 保存するか
		/// </summary>
		public bool IsSave { get; set; }


		public DebugItemDataBase(string title) =>
			Title = title;

		public void DataUpdate(GameObject obj)
		{
			DataUpdateInternal(obj.GetComponent<T>());
		}

		public abstract Const.MenuType GetMenuType();


		protected abstract void DataUpdateInternal(T obj);
	}



	/// <summary>
	/// デバッグ管理者
	/// </summary>
	public class DebugConfigManager : 
		Utility.MonoSingleton<DebugConfigManager>, 
		Utility.UI.RecyclableScrollView.IContentDataProvider 
    {
		#region 定数, class, enum

		[Serializable]
		private class MenuObject
		{
			[SerializeField] private Const.MenuType _type = Const.MenuType.None;
			[SerializeField] private GameObject _object = null;

			public Const.MenuType Type => _type;
			public GameObject Obj => _object;
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private RecyclableScrollView _scrollContent = null;
		[SerializeField] private MenuObject[] _menuObjects = null;
		[SerializeField] private Button _btnClose = null;
		[SerializeField] private Text _txtTitle = null; // タイトルテキスト

		private DebugMenuItem.Data _currMenu = null;  // 現在表示しているMenu
		private Dictionary<Const.MenuType, MenuObject> _menuObjectCaches = new Dictionary<Const.MenuType, MenuObject>();

		#endregion


		#region プロパティ

		/// <summary>
		/// 現在データの個数
		/// </summary>
		public int DataCount => _currMenu.Count;

		public DebugMenuItem.Data RootMenu { get; } = new DebugMenuItem.Data("Root");

		public bool IsOpened => gameObject.activeSelf;

		#endregion


		#region public, protected 関数

		/// <summary>
		/// デバッグ画面を開く
		/// </summary>
		public void Open()
		{
			if (gameObject.activeSelf) return;

			_currMenu = RootMenu;

			_menuObjectCaches.Clear();
			_menuObjectCaches = _menuObjects.ToDictionary(_menuObject => _menuObject.Type);
			
			gameObject.SetActive(true);

			_scrollContent.Refresh(needRemoveCache: false);
		}

		public void Close()
		{
			if (gameObject.activeSelf)
			{
				gameObject.SetActive(false);
			}
		}

		public void AddItemDataByRootMenu(IDebugItemData itemData)
		{
			RootMenu.Add(itemData);
		}

		public void RemoveItemDataByRootMenu(IDebugItemData itemData)
		{
			//RootMenu.Remove(itemData);
		}


		public float GetItemSize(int index)
		{
			var obj = GetItemObj(index);
			if (obj == null) return 0f;

			var rectTrans = obj.GetComponent<RectTransform>();

			return rectTrans.sizeDelta.y;
		}

		public GameObject GetItemObj(int index)
		{
			var item = _currMenu[index];

			if (_menuObjectCaches.TryGetValue(item.GetMenuType(), out MenuObject value))
			{
				return value.Obj;
			}

			return null;
		}

		/// <summary>
		/// スクロール更新
		/// </summary>
		public void ScrollItemUpdate(int index, GameObject obj)
		{
			var item = _currMenu[index];
			item.DataUpdate(obj);
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			_currMenu = RootMenu;

			_scrollContent.Initialize(this);
		}

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
			// 非アクティブで閉じる
			_btnClose.onClick.AsObservable().Subscribe(_ =>
				{
					gameObject.SetActive(false);
				});
		}

		#endregion
	}
}
#endif