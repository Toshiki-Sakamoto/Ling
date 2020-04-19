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
using Zenject;

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

		[Inject] protected Common.Scene.IExSceneManager _sceneManager = null;
		[Inject] protected Utility.IEventManager _eventManager = null;

		#endregion


		#region プロパティ

		/// <summary>
		/// シーン遷移時に渡される引数
		/// </summary>
		public Argument Argument { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 遷移後まずは呼び出される
		/// </summary>
		/// <returns></returns>
		public virtual IObservable<Unit> ScenePrepareAsync() =>
			Observable.Return(Unit.Default);

		/// <summary>
		/// シーンが開始される時
		/// </summary>
		public virtual void StartScene() { }

		/// <summary>
		/// シーン終了時
		/// </summary>
		public virtual void StopScene() { }

		/// <summary>
		/// シーン遷移前に呼び出される
		/// </summary>
		/// <returns></returns>
		public virtual IObservable<Unit> SceneStopAsync(Argument nextArgument) =>
			Observable.Return(Unit.Default);

		#endregion


		#region private 関数

		#endregion
	}
}
