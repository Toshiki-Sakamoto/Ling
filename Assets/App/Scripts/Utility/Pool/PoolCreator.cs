// 
// PoolItem.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.01
// 
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
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
	/// プール作成時の情報
	/// </summary>
	[System.Serializable]
	public class PoolCreateInfo
	{
		public Transform poolRoot = null;    // プール保管先
		public GameObject poolObject = null;
		public int initCreateNum = 0;        // 初期生成数
		public bool isAutoCreate = true;     // プールがなくなったら自動で生成する
		public int onceCreatingNum = 20;     // 一度の処理で作成する数
	}


	/// <summary>
	/// プールの生成方法を管理＋生成を行う。
	/// 不足した場合自動で生成を行う。
	/// </summary>
	[System.Serializable]
	public abstract class PoolCreator : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private PoolCreateInfo _info = null;

		private List<PoolItem> _poolItems = new List<PoolItem>();		// 保存プール
		private List<PoolItem> _usedPoolItems = new List<PoolItem>();	// 使用しているプールアイテム
		private Stack<PoolItem> _unusedPoolItems = new Stack<PoolItem>();	// 使用していないプールアイテム

		#endregion


		#region プロパティ

		public PoolCreateInfo Info => _info;

		/// <summary>
		/// プール生成する実態
		/// </summary>
		public GameObject PoolObject => _info.poolObject;

		/// <summary>
		/// プール生成先
		/// </summary>
		public Transform PoolRoot => _info.poolRoot;

		#endregion


		#region public, protected 関数

		public void Setup(Transform poolRoot, GameObject poolObject, int initCreateNum = 0)
		{
			Info.poolObject = poolObject;

			Setup(poolRoot, initCreateNum);
		}

		public void Setup(Transform poolRoot, int initCreateNum = 0)
		{
			_info.poolRoot = poolRoot;
			_info.initCreateNum = initCreateNum;

			Setup();
		}

		public void Setup()
		{
			if (PoolObject == null)
			{
				Utility.Log.Warning("プールオブジェクトに何も指定されていない");
				return;
			}

			if (PoolRoot == null)
			{
				Utility.Log.Warning("プール生成先に何も指定されていない");
				return;
			}
		}

		/// <summary>
		/// 生成情報を外部から設定する
		/// </summary>
		public void SetInfo(PoolCreateInfo info) =>
			_info = info;

		public async UniTask CreateObjectAsync()
		{
			Info.poolObject.SetActive(false);

			int count = 0;
			for (int i = 0; i < Info.initCreateNum; ++i)
			{
				CreatePoolAdditional();

				if (++count >= Info.onceCreatingNum)
				{
					await UniTask.DelayFrame(1);

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
				if (!Info.isAutoCreate)
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
			poolItem.transform.SetParent(Info.poolRoot);

			poolItem.Unused();

			_usedPoolItems.Remove(poolItem);
			_unusedPoolItems.Push(poolItem);
		}

		/// <summary>
		/// すべてのアイテムをリストに戻す
		/// </summary>
		public void ReturnAllItems()
		{
			foreach (var item in _usedPoolItems)
			{
				item.Detach();
			}
		}

		#endregion

 
		#region private 関数

		/// <summary>
		/// 追加で生成する
		/// </summary>
		/// <returns></returns>
		private PoolItem CreatePoolAdditional()
		{
			var poolObject = Instantiate(Info.poolObject, Info.poolRoot);
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