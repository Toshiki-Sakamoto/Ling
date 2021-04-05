// 
// ConvertToCortouine.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.09.21
// 

using System.Collections;
using UniRx;
using UnityEngine;
using System;
using UniRx.Triggers;

namespace Ling.Tests.PlayMode.Plugin.UniRx
{
	/// <summary>
	/// 
	/// </summary>
	public class ConvertToCortouine : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

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
			StartCoroutine(WaitCoroutine());
		}

		private IEnumerator WaitCoroutine()
		{
			// Subscribeの代わりにToYieldInstruction()を利用することで
			// コルーチンとしてストリームを扱えるようになる

			yield return Observable.Timer(TimeSpan.FromSeconds(0.1f)).ToYieldInstruction();

			// ToYieldInstruction()はOnompletedが発行されてコルーチンを終了する
			// そのためOnCompletedが必ず発行されるストリームでのみ利用できる
			// 無限に続くストリームの場合はFirstやFirstOrDefaultを使うと良いかも

			bool isFinished = true;
			yield return this.UpdateAsObservable()
				.FirstOrDefault(_ => isFinished)
				.ToYieldInstruction();

			// FirstOrDefaultは条件を満たすとOnNextとOnCompletedを両方発行する
			Finished = true;
		}

		#endregion
	}
}