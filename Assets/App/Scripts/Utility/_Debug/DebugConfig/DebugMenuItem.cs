//
// DebugMenu.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.30
//

using UnityEngine;
using System.Collections.Generic;
using Zenject;
using UnityEngine.UI;
using UniRx;

#if DEBUG
namespace Utility.DebugConfig
{
	public class DebugMenuItem : MonoBehaviour
	{
		/// <summary>
		/// Menuクラスを子供として保持する
		/// 親のMenuに子供のMenuを登録したい場合はこれに包んでからMenuクラスのAddItemに指定する
		/// </summary>
		public class Data : DebugItemDataBase<DebugMenuItem>
		{
			[Inject] DiContainer _diContainer;

			public List<IDebugItemData> children = new List<IDebugItemData>();

			public int Count => children.Count;

			/// <summary>
			/// リストの中身
			/// </summary>
			public IDebugItemData this[int index] => children[index];

			public Data(string title)
				: base(title)
			{
			}

			protected override void DataUpdateInternal(DebugMenuItem obj)
			{
				obj.SetData(this);
			}

			public override Const.MenuType GetMenuType() =>
				Const.MenuType.MenuItem;

			/// <summary>
			/// 子供のItemDataを作成する
			/// </summary>
			public T CreateAndAddItem<T>() where T : IDebugItemData, new()
			{
				_diContainer
					.BindInterfacesAndSelfTo<T>()
					.AsSingle();

				var instance = _diContainer.Resolve<T>();

				Add(instance);

				return instance;
			}

			public void Add(IDebugItemData itemData)
			{
				children.Add(itemData);
			}

			public void Remove(IDebugItemData itemData)
			{
				children.Remove(itemData);
			}
		}


		[SerializeField] private Button _button = default;
		[SerializeField] private Text _buttonText = default;

		[Inject] private DebugConfigManager _manager = default;

		private Data _data;
		private List<IDebugItemData> _dataList = new List<IDebugItemData>();
		public int DataCount => _dataList.Count;



		public void SetData(Data data)
		{
			_buttonText.text = data.Title;

			_data = data;
		}

		public void Add(IDebugItemData item)
		{
			if (item == null)
			{
				Utility.Log.Error("追加しようとしたItemがNull");
				return;
			}

			_dataList.Add(item);
		}

		public void Remove(IDebugItemData item)
		{
			if (item == null)
			{
				Utility.Log.Error("削除しようとしたItemがNull");
				return;
			}

			_dataList.Remove(item);
		}

		public void Awake()
		{
			_button
				.OnClickAsObservable()
				.Subscribe(_ =>
					{
						// 自分が持つリストを表示させる
						_manager.UpdateMenuItemData(_data);
					});
		}
	}
}
#endif