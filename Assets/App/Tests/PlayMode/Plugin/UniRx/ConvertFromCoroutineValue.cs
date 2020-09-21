// 
// ConvertFromCoroutineValue.cs  
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
	/// 
	/// </summary>
	public class ConvertFromCoroutineValue : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public int Count { get; private set; }
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
			Observable.FromCoroutineValue<int>(IncValueCotrouine)
				.Subscribe(count => Count += count, () => Finished = true);
		}

		private IEnumerator IncValueCotrouine()
		{
			for (int i = 0; i < 3; ++i)
			{
				yield return i;
			}
		}

		#endregion
	}
}