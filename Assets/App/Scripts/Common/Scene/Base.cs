//
// Base.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.15
//

using Cysharp.Threading.Tasks;
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
		[Inject] protected MasterData.MasterManager _masterManager = null;

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
		/// 正規手順でシーンが実行されたのではなく
		/// 直接起動された場合StartSceneよりも前に呼び出される
		/// </summary>
		public virtual void QuickStartScene() { }

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


		/// <summary>
		/// 指定したシーンを直接起動する場合、必要な手続きを踏んでからシーン開始させる
		/// </summary>
		protected void QuickStart()
		{
			QuickStartInternalAsync().Forget();
		}

		protected virtual async UniTask QuickStartInternalAsync()
		{
			// マスタデータの読み込みが終わっていない場合読み込みを行う
			if (!_masterManager.IsLoaded)
			{
				await _masterManager.LoadAllAsync();
			}

			QuickStartScene();

			_sceneManager.QuickStart(this);
		}

		#endregion


		#region private 関数

		private void Awake()
		{
			_processManager.SetupScene(this);
		}

		#endregion
	}
}
