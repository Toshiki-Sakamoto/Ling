//
// PoolManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Ling;

using Zenject;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Ling.Utility.Pool
{
	/// <summary>
	/// 特定のゲームオブジェクトをプールで保管する。引き出し、預け入れができる。
	/// <typeparamref name="TEnum"/>で指定した列挙型でプールを判別する。
	/// </summary>
	public abstract class PoolManager<TEnum, TPoolManager> : PoolManager<TEnum, DefaultPoolCreator, TPoolManager>
		where TEnum : Enum
		where TPoolManager : MonoBehaviour
	{
	}


	/// <summary>
	/// 実際のPoolManager。
	/// 生成方法を自分で渡す。
	/// </summary>
	public abstract class PoolManager<TEnum, TPoolCreator, TPoolManager> : MonoBehaviour
		where TEnum : Enum
		where TPoolCreator : PoolCreator
		where TPoolManager : MonoBehaviour
	{
		#region 定数, class, enum

		[System.Serializable]
		public class PoolCreateItem
		{
			public TEnum key;
			public string id;
			public bool isUsedCreator;	// 直接PoolCreatorを使用する場合はtrue
			public TPoolCreator creator;
			public PoolCreateInfo createInfo;

			public bool Equal(TEnum key, string id) =>
				EqualityComparer<TEnum>.Default.Equals(this.key, key) && this.id == id;
		}
		

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private Transform _defaultPoolRoot = null; // PoolItemがRootを持っていないときに設定されるデフォルトプールルート
		[SerializeField] private List<PoolCreateItem> _createItems = null;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 手動でPool情報を追加する
		/// </summary>
		public PoolCreator AddPoolItem(TEnum key, GameObject poolObject, int initCreateNum = 0) =>
			AddPoolItem(key, null, poolObject, initCreateNum);

		public PoolCreator AddPoolItem(TEnum key, string id, GameObject poolObject, int initCreateNum = 0)
		{
			if (_createItems.Exists(item_ => item_.Equal(key, id)))
			{
				Utility.Log.Error($"すでに存在するKeyです。二重登録禁止 {key.ToString()}");
				return null;
			}

			var creator = gameObject.AddComponent<TPoolCreator>();
			creator.Setup(_defaultPoolRoot, poolObject, initCreateNum);

			_createItems.Add(new PoolCreateItem { key = key, id = id, creator = creator });

			return creator;
		}

		/// <summary>
		/// 内部で保持しているPoolItem情報からPoolを作成する
		/// </summary>
		public async UniTask CreateObjectsAsync()
		{
			var list = new List<UniTask>();

			foreach(var poolItemData in _createItems)
			{
				list.Add(poolItemData.creator.CreateObjectAsync());
			}

			await UniTask.WhenAll(list);
		}

		/// <summary>
		/// プールスタックから取り出す
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public T Pop<T>(TEnum key) => Pop<T>(key, string.Empty);

		public T Pop<T>(TEnum key, string id)
		{
			var poolItemData = _createItems.Find(itemData_ => itemData_.Equal(key, id));
			if (poolItemData == null)
			{
				Utility.Log.Error($"プール内に存在しません Type:{key.ToString()} ID:{id}");

				return default(T);
			}

			var popItem = poolItemData.creator.Pop();

			return popItem.GetComponent<T>();
		}

		/// <summary>
		/// すべてのアイテムをプールに戻す
		/// 初期値には戻さない
		/// </summary>
		public void ReturnAllItems()
		{
			foreach (var item in _createItems)
			{
				item.creator.ReturnAllItems();
			}
		}

		#endregion


		#region private 関数

		/// <summary>
		/// Creatorのセットアップを行う
		/// </summary>
		private void SetupCreatorItems()
		{
			foreach (var createItem in _createItems)
			{
				TPoolCreator creator;

				if (createItem.isUsedCreator)
				{
					// クリエイターを直接使用する
					creator = createItem.creator;
					if (creator == null)
					{
						Utility.Log.Warning("PoolCreatorがNull");
						return;
					}
				}
				else
				{
					// クリエイターはこちらで作成する
					creator = gameObject.AddComponent<TPoolCreator>();
					creator.SetInfo(createItem.createInfo);

					createItem.creator = creator;
				}

				// プール先がない場合は指定する
				if (creator.PoolRoot == null)
				{
					creator.Setup(_defaultPoolRoot);
				}
				else
				{
					creator.Setup();
				}
			}
		}


		protected virtual void Awake()
		{
			// デフォルトのプール先がNullの場合は自分を指定する
			if (_defaultPoolRoot == null)
			{
				Utility.Log.Print("デフォルトのプール先が無いため自分を指定します");

				_defaultPoolRoot = transform;
			}

			// 設定されているCreatorのセットアップを行う
			SetupCreatorItems();
		}
		
		#endregion
	}
}
