// 
// PoolDetachCallbacker.cs  
// ProductName Ling
//  
// Created by  on 2021.09.14
// 

using UnityEngine;
using UniRx;
using System;

namespace Utility.Pool
{
	/// <summary>
	/// PoolItemのDeatchが呼び出されてプールに戻るときにメソッドを呼び出す
	/// </summary>
	public class PoolCallbacker : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private Subject<PoolItem> _onDetachSubject = new Subject<PoolItem>();

		#endregion


		#region プロパティ

		public IObservable<PoolItem> OnDetach => _onDetachSubject;

		#endregion


		#region public, protected 関数

		public void CallDetach(PoolItem item)
		{
			_onDetachSubject.OnNext(item);
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}