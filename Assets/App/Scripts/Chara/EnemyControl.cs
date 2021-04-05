// 
// EnemyControl.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.10
// 

using UnityEngine;
using UniRx;
using System;

namespace Ling.Chara
{
	/// <summary>
	/// Enemy Control
	/// </summary>
	public class EnemyControl : CharaControl<EnemyModel, EnemyView>
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		/// <summary>
		/// 削除時に呼び出される
		/// </summary>
		public IObserver<EnemyControl> OnDestroyed;

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		protected override void DestroyProcessInternal()
		{
			OnDestroyed?.OnNext(this);
			OnDestroyed?.OnCompleted();
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}