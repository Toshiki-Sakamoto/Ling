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

		[Inject] protected DiContainer _diContainer;
		[Inject] protected Common.Scene.IExSceneManager _sceneManager = null;
		[Inject] protected Utility.IEventManager _eventManager = null;
		[Inject] protected Utility.ProcessManager _processManager = null;

		#endregion


		#region プロパティ

		/// <summary>
		/// シーン遷移時に渡される引数
		/// </summary>
		public Argument Argument { get; set; }

		/// <summary>
		/// StartScene呼び出されるときにtrueになる
		/// StopSceneでfalse
		/// </summary>
		public bool IsStartScene { get; set; }

		/// <summary>
		/// DIContainerを取得する
		/// </summary>
		public DiContainer DiContainer  => _diContainer;

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
		/// StartScene後呼び出される
		/// </summary>
		public virtual void UpdateScene() { }

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

		private void Awake()
		{
			_processManager.SetupScene(this);
		}

		#endregion
	}
}
