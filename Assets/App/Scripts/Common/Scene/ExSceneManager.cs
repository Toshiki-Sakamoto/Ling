﻿// 
// Manager.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.04.30.
// 
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Ling.Common.Scene
{
	public interface IExSceneManager
	{
		Base Current { get; }

		void StartScene(SceneID sceneID);

		void ChangeScene(SceneID sceneID, Argument argument = null);

		void AddScene(SceneID sceneID, Argument argument = null);

		void QuickStart(Base scene);
	}


	/// <summary>
	/// 
	/// </summary>
	[DefaultExecutionOrder(Common.ExcutionOrders.SceneManager)]
	public class ExSceneManager : MonoBehaviour, IExSceneManager
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		public static ExSceneManager Instance { get; private set; }

		#endregion


		#region private 変数

		[SerializeField] private Transform _sceneRoot;  // シーンインスタンスが配置されるルート

		[Inject] private MasterData.MasterManager _masterManager = null;

		private SceneID _nextSceneID = SceneID.None;
		private Base _sceneInstance = null;
		private Stack<SceneData> _sceneData = new Stack<SceneData>();
		private Stack<SceneData> _addSceneData = new Stack<SceneData>();


		#endregion


		#region プロパティ

		public Base Current => _sceneInstance;

		#endregion


		#region public, protected 関数

		/// <summary>
		/// まず初回に起動するシーンはここを呼び出す
		/// </summary>
		/// <param name="sceneID"></param>
		public void StartScene(SceneID sceneID)
		{
			ChangeScene(sceneID, Argument.Create());
		}

		/// <summary>
		/// シーンを完全に入れ替える
		/// </summary>
		/// <param name="sceneID"></param>
		/// <param name="arg"></param>
		public void ChangeScene(SceneID sceneID, Argument argument = null)
		{
			SceneChangeInternalAsync(sceneID, argument, LoadSceneMode.Single).Forget();
		}

		/// <summary>
		/// 現在のシーンの上に追加する
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="argument"></param>
		public void AddScene(SceneID sceneID, Argument argument = null)
		{
			SceneChangeInternalAsync(sceneID, argument, LoadSceneMode.Additive).Forget();
		}

		/// <summary>
		/// 正規の手順ではなく、指定したシーンからゲームを始める
		/// </summary>
		public void QuickStart(Base scene)
		{
			_sceneInstance = scene;

			scene.IsStartScene = true;
			scene.StartScene();
		}

		#endregion


		#region private 関数

		private async UniTask SceneChangeInternalAsync(SceneID sceneID, Argument argument, LoadSceneMode mode)
		{
			_nextSceneID = sceneID;

			// デフォルト生成
			if (argument == null)
			{
				argument = Argument.Create();
			}

			// 遷移前処理
			if (_sceneInstance != null)
			{
				_sceneInstance.IsStartScene = false;
				_sceneInstance.StopScene();

				await _sceneInstance.SceneStopAsync(argument);

				GameObject.Destroy(_sceneInstance.gameObject);
			}

			var sceneData = new SceneData() { SceneID = sceneID, Argument = argument };

			if (mode == LoadSceneMode.Additive)
			{
				_addSceneData.Push(sceneData);
			}
			else
			{
				for (int i = 1; i < SceneManager.sceneCount; ++i)
				{
					var scene = SceneManager.GetSceneAt(i);

					await SceneManager.UnloadSceneAsync(scene);
				}

				// AddSceneすべて削除
				_addSceneData.Clear();

				// StackClear
				if (argument.IsStackClear)
				{
					_sceneData.Clear();
				}

				_sceneData.Push(sceneData);
			}

			// シーン遷移処理
			await LoadSceneAsync(sceneID.GetName(), argument, LoadSceneMode.Additive);
		}


		private IObservable<Base>InitLoadPrepareAsync(Base scene)
		{
			return Observable.Return(scene);
		}

		private IObservable<Unit> LoadSceneAsync(string sceneName, Argument argument, LoadSceneMode mode = LoadSceneMode.Single)
		{
			return Observable.FromCoroutine<Unit>(observer_ =>
				LoadSceneOperationAsync(SceneManager.LoadSceneAsync(sceneName, mode), observer_))
				.Select(_ =>
				{
					var scene = GameObject.FindObjectOfType<Base>();
					if (scene == null)
					{
						Utility.Log.Error($"Scene.Base クラスが見つかりません {typeof(Base).ToString()}");

						return null;
					}

					scene.transform.SetParent(_sceneRoot);
					scene.Argument = argument;

					// 準備が整うまで非アクティブ
					scene.gameObject.SetActive(false);

					// 必要なデータが読み込まれていない場合、読み込みを行う
					//Observable.FromCoroutine<Base>(observer_ => LoadScenePrepareAsync(observer_));

					return scene;
				})
				.ContinueWith(scene_ => InitLoadPrepareAsync(scene_))
				.SelectMany(scene_ =>
				{
					return scene_.ScenePrepareAsync().Do(unit_ =>
						{
							_sceneInstance = scene_;

							// 事前準備が終わったのでここで始める
							scene_.gameObject.SetActive(true);

							scene_.IsStartScene = true;
							scene_.StartScene();
						});
				});
		}

		private IEnumerator LoadSceneOperationAsync(AsyncOperation operation, IObserver<Unit> observer)
		{
			if (!operation.isDone) yield return operation;

			observer.OnNext(Unit.Default);
			observer.OnCompleted();
		}

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			if (Instance != null)
			{
				Destroy(Instance);
			}

			Instance = this;

			// 起動時rootがNullの場合は自分につく
			if (_sceneRoot == null)
			{
				_sceneRoot = transform;
			}
		}

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update()
		{
			if (_sceneInstance == null) return;
			if (!_sceneInstance.IsStartScene) return;

			_sceneInstance.UpdateScene();
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		void OnDestoroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}

		#endregion
	}
}