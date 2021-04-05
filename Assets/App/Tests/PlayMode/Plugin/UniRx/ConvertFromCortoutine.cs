// 
// ConvertFromCortoutine.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.09.21
// 

using System.Collections;
using UniRx;
using UnityEngine;

namespace Ling.Tests.PlayMode.Plugin.UniRx
{
	/// <summary>
	/// Cortouineテスト
	/// </summary>
	public class ConvertFromCortoutine : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public bool Finished { get; private set; }
		public bool TriggerdOnNext { get; private set; }
		public bool TriggerdOnCompleted { get; private set; }

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
			Observable.FromCoroutine(HogeCoroutine, publishEveryYield: false)
				.Subscribe(
					_ => TriggerdOnNext = true,
					() => TriggerdOnCompleted = true
				).AddTo(this);
		}

		private IEnumerator HogeCoroutine()
		{
			Debug.Log("Coroutine started");

			// 何かを処理して待ち受ける
			yield return new WaitForSeconds(0.1f);

			Debug.Log("Coroutine finished");

			Finished = true;
		}

		#endregion
	}
}