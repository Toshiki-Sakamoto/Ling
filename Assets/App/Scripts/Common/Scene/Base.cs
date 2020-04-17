//
// Base.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.15
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Common.Scene
{
	/// <summary>
	/// シーンベース
	/// </summary>
	public abstract class Base : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Zenject.Inject] private Common.Scene.Manager _sceneManager = null;

		#endregion


		#region プロパティ

		/// <summary>
		/// シーン遷移時に渡される引数
		/// </summary>
		public Argument Argumect { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 遷移後まずは呼び出される
		/// </summary>
		/// <returns></returns>
		public virtual IObservable<Unit> ScenePrepareAsync()
		{
			return Observable.Return(Unit.Default);
		}

		/// <summary>
		/// シーン遷移前に呼び出される
		/// </summary>
		/// <returns></returns>
		public virtual IObservable<Unit> SceneStopAsync(Argument nextArgument)
		{
			return Observable.Return(Unit.Default);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
