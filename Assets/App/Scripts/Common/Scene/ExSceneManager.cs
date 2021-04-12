// 
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
using System.Linq;
using Ling.Utility.Extensions;

namespace Ling.Common.Scene
{
	public interface IExSceneManager
	{
		Base Current { get; }

		void StartScene(SceneID sceneID);

		void ChangeScene(SceneID sceneID, Argument argument = null, System.Action<DiContainer> bindAction = null);

		void AddSceneAsync(SceneID sceneID, Argument argument = null, System.Action<DiContainer> bindAction = null);
		UniTask<TScene> AddSceneAsync<TScene>(SceneID scene, Argument argument = null, System.Action<DiContainer> bindAction = null) where TScene : Base;

		UniTask QuickStartAsync(Base scene);
	}


	/// <summary>
	/// シーン管理者
	/// </summary>
	public class ExSceneManager : MonoBehaviour, IExSceneManager
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		public static ExSceneManager Instance { get; private set; }

		#endregion


		#region private 変数

		[SerializeField] private Transform _sceneRoot;  // シーンインスタンスが配置されるルート

		[Inject] private ZenjectSceneLoader _zenjectSceneLoader = default;

		private Base _currentScene = null;
		private List<Base> _addScenes = new List<Base>();   // AddSceneインスタンス
		private Stack<SceneData> _sceneData = new Stack<SceneData>();


		#endregion


		#region プロパティ

		public Base Current => _currentScene;

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
		public void ChangeScene(SceneID sceneID, Argument argument = null, System.Action<DiContainer> bindAction = null)
		{
			SceneChangeInternalAsync(sceneID, argument, LoadSceneMode.Single, bindAction).Forget();
		}

		/// <summary>
		/// 指定したシーンを閉じる
		/// </summary>
		public void CloseScene(Base scene) =>
			CloseSceneAsync(scene).Forget();

		public async UniTask CloseSceneAsync(Base scene)
		{
			if (_currentScene == scene)
			{
				// CurrentSceneの場合、一つ前のシーンに戻る
			}
			else
			{
				_addScenes.Remove(scene);

				// AddSceneの場合、自分を削除して終わる
				await StopSceneAsyncInternal(scene);
			}
		}

		/// <summary>
		/// 指定したSceneDataを持つところまで戻る
		/// </summary>
		public async UniTask<Base> BackToSceneAsync(SceneData sceneData)
		{
			while (_sceneData.Count > 0)
			{
				var elm = _sceneData.Pop();
				if (elm == sceneData) break;
			}

			// 空の場合見つからなかったのでデフォルトに戻す
			return await SceneChangeInternalAsync(sceneData, LoadSceneMode.Single);
		}

		/// <summary>
		/// Stack順にSceneIDからSceneDataを返す
		/// </summary>
		public SceneData FindSceneDataBySceneID(SceneID sceneID)
		{
			foreach (var sceneData in _sceneData)
			{
				if (sceneData.SceneID == sceneID) return sceneData;
			}

			return null;
		}

		/// <summary>
		/// 現在のシーンの上に追加する
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="argument"></param>
		public async void AddSceneAsync(SceneID sceneID, Argument argument = null, System.Action<DiContainer> bindAction = null)
		{
			await SceneChangeInternalAsync(sceneID, argument, LoadSceneMode.Additive, bindAction);
		}
		public async UniTask<TScene> AddSceneAsync<TScene>(SceneID sceneID, Argument argument = null, System.Action<DiContainer> bindAction = null) where TScene : Base
		{
			var result = await SceneChangeInternalAsync(sceneID, argument, LoadSceneMode.Additive, bindAction);
			return result as TScene;
		}

		/// <summary>
		/// 正規の手順ではなく、指定したシーンからゲームを始める
		/// </summary>
		public async UniTask QuickStartAsync(Base scene)
		{
			_currentScene = scene;

			// 依存しているシーンを呼び出す
			// tood: 今めっちゃ仮で適当に起動してる
			await SceneLoadProcessAsync(SceneID.Main, null, LoadSceneMode.Single, null, _currentScene);
		}

		#endregion


		#region private 関数


		private async UniTask<Base> SceneChangeInternalAsync(SceneID sceneID, Argument argument, LoadSceneMode mode, System.Action<DiContainer> bindAction = null)
		{
			// デフォルト生成
			if (argument == null)
			{
				argument = Argument.Create();
			}

			// 1シーン、１SceneData
			var sceneData = new SceneData() { SceneID = sceneID, Argument = argument, BindAction = bindAction };

			return await SceneChangeInternalAsync(sceneData, mode);
		}

		private async UniTask<Base> SceneChangeInternalAsync(SceneData sceneData, LoadSceneMode mode)
		{
			if (mode == LoadSceneMode.Additive)
			{
				// 現在のシーンに情報を乗せる
				_currentScene?.SceneData.PushAddSceneData(sceneData);
			}
			else
			{
				// 遷移前処理

				// AddSceneすべて削除
				foreach (var scene in _addScenes)
				{
					await StopSceneAsyncInternal(scene);
				}

				_addScenes.Clear();

				if (_currentScene != null)
				{
					await StopSceneAsyncInternal(_currentScene);
				}

				// todo: 差分のみUnloadするようにしたい
				for (int i = 1; i < SceneManager.sceneCount; ++i)
				{
					var scene = SceneManager.GetSceneAt(i);

					await SceneManager.UnloadSceneAsync(scene);
				}

				// StackClear
				if (sceneData.Argument.IsStackClear)
				{
					_sceneData.Clear();
				}

				_sceneData.Push(sceneData);
			}

			return await SceneLoadProcessAsync(sceneData.SceneID, sceneData.Argument, mode, sceneData.BindAction);
		}

		/// <summary>
		/// シーン読み込みの一連の処理を行う
		/// </summary>
		private async UniTask<Base> SceneLoadProcessAsync(SceneID sceneID, Argument argument, LoadSceneMode mode, System.Action<DiContainer> bindAction, Base loadedScene = null)
		{
			// シーン遷移処理
			if (loadedScene == null)
			{
				loadedScene = await LoadSceneAsync(sceneID.GetName(), argument, mode, bindAction: bindAction);
			}

			// 事前準備が終わったのでここで始める
			loadedScene.gameObject.SetActive(true);

			// 読み込み後ほかシーンに依存する設定がある場合
			if (!loadedScene.Dependences.IsNullOrEmpty())
			{
				foreach (var dependenceData in loadedScene.Dependences.Where(data => data.Timing.IsLoaded()))
				{
					await SceneLoadProcessAsync(dependenceData.SceneID, dependenceData.Argument, LoadSceneMode.Additive, bindAction /* todo */);
				}
			}

			// すべてのシーンを読み込み終わったらStartSceneを呼び出す
			loadedScene.IsStartScene = true;
			loadedScene.StartScene();

			return loadedScene;
		}

		private IObservable<Base> InitLoadPrepareAsync(Base scene)
		{
			return Observable.Return(scene);
		}

		/// <summary>
		/// シーン読み込み処理
		/// 非同期で読み込み、完了後切り替える
		/// </summary>
		private IObservable<Base> LoadSceneAsync(string sceneName, Argument argument, LoadSceneMode mode = LoadSceneMode.Single, System.Action<DiContainer> bindAction = null, System.Action onLoaded = null)
		{
			return Observable.FromCoroutine<Unit>(observer_ =>
				LoadSceneOperationAsync(_zenjectSceneLoader.LoadSceneAsync(sceneName, mode, bindAction), observer_))
				.Select(scene_ =>
				{
					// LoadSceneOperationAsync内のOnNextが呼び出されたときに来る
					var activeSceneInstance = SceneManager.GetSceneByName(sceneName);
					var scene = default(Base);

					// 読み込んだシーンからシーンクラスのインスタンスを取得する
					foreach (var rootObject in activeSceneInstance.GetRootGameObjects())
					{
						scene = rootObject.GetComponent<Base>();
						if (scene != null)
						{
							break;
						}
					}

					if (scene == null)
					{
						Utility.Log.Error($"Scene.Base クラスが見つかりません {typeof(Base).ToString()}");

						return null;
					}

					/////	scene.transform.SetParent(_sceneRoot);
					scene.Argument = argument;

					// 準備が整うまで非アクティブ
					scene.gameObject.SetActive(false);

					// 必要なデータが読み込まれていない場合、読み込みを行う
					//Observable.FromCoroutine<Base>(observer_ => LoadScenePrepareAsync(observer_));

					// Sceneに変換
					return scene;
				})
				.ContinueWith(scene_ => InitLoadPrepareAsync(scene_))
				.SelectMany(scene_ =>
				{
					// 別の処理に合成
					return scene_.ScenePrepareAsync().Select(scene_ =>
						{
							switch (mode)
							{
								case LoadSceneMode.Additive:
									_addScenes.Add(scene_);
									break;

								// AddSceneの場合は現在のシーンインスタンスとはしない　
								default:
									_currentScene = scene_;
									break;
							}

							return scene_;
						});
				});
		}

		private IEnumerator LoadSceneOperationAsync(AsyncOperation operation, IObserver<Unit> observer)
		{
			if (!operation.isDone) yield return operation;

			observer.OnNext(Unit.Default);
			observer.OnCompleted();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private async UniTask CloseSceneProcessAsyncInternal()
		{

		}

		/// <summary>
		/// シーンの終了処理
		/// </summary>
		private async UniTask StopSceneAsyncInternal(Base scene)
		{
			// 現在のAddSceneに乗っている情報を持ち変える
			_currentScene?.SceneData.MoveToCacheByAddSceceData();

			scene.IsStartScene = false;

			// 非同期処理の終了処理
			await scene.StopSceneAsync();

			// 同期処理の終了処理
			scene.StopScene();

			GameObject.Destroy(scene.gameObject);
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
			if (_currentScene == null) return;

			if (_currentScene.IsStartScene) return;
			{
				_currentScene.UpdateScene();
			}

			// AddScene
			foreach (var scene in _addScenes)
			{
				if (scene.IsStartScene)
				{
					scene.UpdateScene();
				}
			}
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}

		#endregion
	}
}