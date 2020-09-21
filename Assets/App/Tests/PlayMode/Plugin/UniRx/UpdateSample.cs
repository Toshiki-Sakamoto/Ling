// 
// UpdateSample.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.09.20
// 

using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Ling.Tests.PlayMode.Plugin.UniRx
{
	/// <summary>
	/// Updateテスト
	/// </summary>
	public class UpdateSample : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public bool Updated { get; private set; }

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
			// Componentに対する拡張メソッドで定義されてるので呼び出すときはthisが必要
			this.UpdateAsObservable()
				.Subscribe(_ => Updated = true);
		}

		#endregion
	}
}