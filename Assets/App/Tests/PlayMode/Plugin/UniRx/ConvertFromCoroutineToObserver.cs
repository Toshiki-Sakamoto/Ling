// 
// ConvertFromCoroutineToObserver.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.09.21
// 

using System.Collections;
using UniRx;
using UnityEngine;
using System;

namespace Ling.Tests.PlayMode.Plugin.UniRx
{
	/// <summary>
	/// 
	/// </summary>
	public class ConvertFromCoroutineToObserver : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public long Count { get; private set; }
		public bool Finished { get; private set; }

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
			Observable.FromCoroutine<long>(observer => CountCoroutine(observer))
				.Subscribe(count => Count = count, () => Finished = true)
				.AddTo(gameObject);
		}

		private IEnumerator CountCoroutine(IObserver<long> observer)
		{
			long current = 0;

			// Disposeしたらコルーチンごと止まるので while(true) でも問題なく動く
			// 気持ち悪いならCancellationTokentを受け取って利用すれば良い
			while (current <= 3)
			{
				observer.OnNext(current++);

				yield return null;
			}

			// 引数にIObserverを渡す場合、OnCompletedが自動で呼出sれない
			observer.OnCompleted();
		}

		#endregion
	}
}