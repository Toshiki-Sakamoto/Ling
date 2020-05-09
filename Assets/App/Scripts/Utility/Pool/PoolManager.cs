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

namespace Ling.Utility.Pool
{
	/// <summary>
	/// 特定のゲームオブジェクトをプールで保管する
	/// </summary>
	public abstract class PoolManager<TEnum, TPoolManager> : PoolManager<TEnum, DefaultPoolCreator, TPoolManager>
		where TEnum : Enum
		where TPoolManager : MonoBehaviour
	{
	}

	public abstract class PoolManager<TEnum, TPoolItem, TPoolManager> : Utility.MonoSingleton<TPoolManager>
		where TEnum : Enum
		where TPoolItem : PoolCreator
		where TPoolManager : MonoBehaviour
	{
		#region 定数, class, enum

		[System.Serializable]
		public class PoolItemData
		{
			public TEnum key;
			public string id;
			public PoolCreator item = null;

			public bool Equal(TEnum key, string id)
			{
				return EqualityComparer<TEnum>.Default.Equals(this.key, key) && this.id == id;
			}
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private Transform _defaultPoolRoot = null; // PoolItemがRootを持っていないときに設定されるデフォルトプールルート
		[SerializeField] private List<PoolItemData> _poolItemData = new List<PoolItemData>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 手動でPool情報を追加する
		/// </summary>
		/// <param name="key"></param>
		/// <param name="poolItem"></param>
		public PoolCreator AddPoolItem(TEnum key) =>
			AddPoolItem(key, null);

		public PoolCreator AddPoolItem(TEnum key, string id)
		{
			if (_poolItemData.Exists(item_ => item_.Equal(key, id)))
			{
				Utility.Log.Error($"すでに存在するKeyです。二重登録禁止 {key.ToString()}");
				return null;
			}

			var item = gameObject.AddComponent<DefaultPoolCreator>();
			item.PoolRoot = _defaultPoolRoot;

			_poolItemData.Add(new PoolItemData { key = key, id = id, item = item });

			return item;
		}

		/// <summary>
		/// 内部で保持しているPoolItem情報からPoolを作成する
		/// </summary>
		public IObservable<Unit> CreatePoolItemsAsync()
		{
			var list = new List<IObservable<Unit>>();

			foreach(var poolItemData in _poolItemData)
			{
				list.Add(poolItemData.item.CreatePoolAsync());
			}

			return Observable.WhenAll(list);
		}

		/// <summary>
		/// プールスタックから取り出す
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public T Pop<T>(TEnum key) => Pop<T>(key, null);

		public T Pop<T>(TEnum key, string id)
		{
			var poolItemData = _poolItemData.Find(itemData_ => itemData_.Equal(key, id));
			if (poolItemData == null)
			{
				Utility.Log.Error($"プール内に存在しません Type:{key.ToString()} ID:{id}");

				return default(T);
			}

			var popItem = poolItemData.item.Pop();

			return popItem.GetComponent<T>();
		}

		#endregion


		#region private 関数


		#endregion
	}
}
