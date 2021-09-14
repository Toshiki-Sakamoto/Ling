// 
// PoolItem.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.02
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

using Zenject;


namespace Utility.Pool
{
	/// <summary>
	/// 
	/// </summary>
	public class PoolItem : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private PoolCreator _poolCreator;   // 自分を生成したPoolCreator
		private PoolCallbacker _callbacker;

		#endregion


		#region プロパティ

		/// <summary>
		/// 使用中
		/// </summary>
		public bool IsUsed { get; private set; }

		/// <summary>
		/// プールに戻した時に呼び出される
		/// </summary>
		public Utility.IFunction OnRelease { get; set; }

		#endregion


		#region public, protected 関数

		public void Setup(PoolCreator poolCreator)
		{
			_poolCreator = poolCreator;
		}

		/// <summary>
		/// プールに戻す
		/// </summary>
		public void Detach()
		{
			_poolCreator.Push(this);

			_callbacker?.CallDetach(this);
		}

		public void Used() => IsUsed = true;

		public void Unused() => IsUsed = false;

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		private void Awake()
		{
			_callbacker = gameObject.GetOrAddComponent<PoolCallbacker>();
		}

		#endregion
	}
}