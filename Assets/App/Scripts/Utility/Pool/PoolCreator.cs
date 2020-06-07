// 
// PoolItem.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.01
// 
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

using Zenject;


namespace Ling.Utility.Pool
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class PoolCreator : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Transform _poolRoot = null;	// プール保管先
		[SerializeField] private GameObject _poolObject = null;
		[SerializeField] private int _initCreateNum = 0;        // 初期生成数
		[SerializeField] private bool _isAutoCreate = true;     // プールがなくなったら自動で生成する
		[SerializeField] private int _onceCreatingNum = 20;		// 一度の処理で作成する数

		private List<PoolItem> _poolItems = new List<PoolItem>();		// 保存プール
		private List<PoolItem> _usedPoolItems = new List<PoolItem>();	// 使用しているプールアイテム
		private Stack<PoolItem> _unusedPoolItems = new Stack<PoolItem>();	// 使用していないプールアイテム

		#endregion


		#region プロパティ

		public Transform PoolRoot { get { return _poolRoot; } set { _poolRoot = value; } }
		public GameObject PoolObject { get { return _poolObject; } set { _poolObject = value; } }
		public int InitCreateNum { get { return _initCreateNum; } set { _initCreateNum = value; } }

		#endregion


		#region public, protected 関数

		public IObservable<Unit> CreatePoolAsync()
		{
			return Observable.FromCoroutine(() => 
				{
					return CreatePool();
				});
		}

		public IEnumerator CreatePool()
		{
			_poolObject.SetActive(false);

			int count = 0;
			for (int i = 0; i < _initCreateNum; ++i)
			{
				CreatePoolAdditional();

				if (++count >= _onceCreatingNum)
				{
					yield return null;
					count = 0;
				}
			}
		}

		/// <summary>
		/// プールから取り出す
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public PoolItem Pop()
		{
			PoolItem result = null;

			if (_unusedPoolItems.Count <= 0)
			{
				// 自動生成できない場合はnullを返す
				if (!_isAutoCreate)
				{
					return null;
				}

				// 自動生成
				result = CreatePoolAdditional();
			}
			else
			{
				// 未使用スタックから取り出す
				result = _unusedPoolItems.Pop();
			}

			
			// 使用リストに追加する
			_usedPoolItems.Add(result);

			result.gameObject.SetActive(true);
			result.Used();

			return result;
		}

		/// <summary>
		/// プールに戻す
		/// </summary>
		/// <param name="poolItem"></param>
		public void Push(PoolItem poolItem)
		{
			if (!_usedPoolItems.Exists(poolItem_ => poolItem_ == poolItem))
			{
				Utility.Log.Error("使用リスト内に存在しない");
				return;
			}

			poolItem.gameObject.SetActive(false);
			poolItem.transform.SetParent(_poolRoot);

			poolItem.Unused();

			_usedPoolItems.Remove(poolItem);
			_unusedPoolItems.Push(poolItem);
		}

		#endregion

 
		#region private 関数

		/// <summary>
		/// 追加で生成する
		/// </summary>
		/// <returns></returns>
		private PoolItem CreatePoolAdditional()
		{
			var poolObject = Instantiate(_poolObject, _poolRoot);
			var poolItem = poolObject.AddComponent<PoolItem>();
			poolItem.Setup(this);

			_unusedPoolItems.Push(poolItem);
			_poolItems.Add(poolItem);

			return poolItem;
		}

		#endregion


		#region MonoBegaviour


		#endregion
	}
}