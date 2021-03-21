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

namespace Ling.Common.Scene
{
	public interface IExSceneManager
	{
		Base Current { get; }

		void StartScene(SceneID sceneID);

		void ChangeScene(SceneID sceneID, Argument argument = null, System.Action<DiContainer> bindAction = null);

		void AddScene(SceneID sceneID, Argument argument = null, System.Action<DiContainer> bindAction = null);

		void QuickStart(Base scene);
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

		private SceneID _nextSceneID = SceneID.None;
		private Base _currentScene = null;
		private List<Base> _addScenes = new List<Base>();	// AddSceneインスタンス
		private Stack<SceneData> _sceneData = new Stack<SceneData>();
		private Stack<SceneData> _addSceneData = new Stack<SceneData>();


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
		/// 現在のシーンの上に追加する
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="argument"></param>
		public void AddScene(SceneID sceneID, Argument argument = null, System.Action<DiContainer> bindAction = null)
		{
			SceneChangeInternalAsync(sceneID, argument, LoadSceneMode.Additive, bindAction).Forget();
		}

		/// <summary>
		/// 正規の手順ではなく、指定したシーンからゲームを始める
		/// </summary>
		public void QuickStart(Base scene)
		{
			_currentScene = scene;

			scene.IsStartScene = true;
			scene.StartScene();
		}

		#endregion


		#region private 関数

		private async UniTask SceneChangeInternalAsync(SceneID sceneID, Argument argument, LoadSceneMode mode, System.Action<DiContainer> bindAction = null)
		{
			_nextSceneID = sceneID;

			// デフォルト生成
			if (argument == null)
			{
				argument = Argument.Create();
			}

			var sceneData = new SceneData() { SceneID = sceneID, Argument = argument };

			if (mode == LoadSceneMode.Additive)
			{
				_addSceneData.Push(sceneData);
			}
			else
			{
				// 遷移前処理
				
				// AddSceneすべて削除
				foreach (var scene in _addScenes)
				{
					scene.IsStartScene = false;
					scene.StopScene();

					GameObject.Destroy(scene.gameObject);
				}

				_addScenes.Clear();
				_addSceneData.Clear();

				if (_currentScene != null)
				{
					_currentScene.IsStartScene = false;
					_currentScene.StopScene();

					await _currentScene.SceneStopAsync(argument);

					GameObject.Destroy(_currentScene.gameObject);
				}

				for (int i = 1; i < SceneManager.sceneCount; ++i)
				{
					var scene = SceneManager.GetSceneAt(i);

					await SceneManager.UnloadSceneAsync(scene);
				}
				// StackClear
				if (argument.IsStackClear)
				{
					_sceneData.Clear();
				}

				_sceneData.Push(sceneData);
			}

			await SceneLoadProcessAsync(sceneID, argument, mode, bindAction);
		}

		/// <summary>
		/// シーン読み込みの一連の処理を行う
		/// </summary>
		private async UniTask<Base> SceneLoadProcessAsync(SceneID sceneID, Argument argument, LoadSceneMode mode, System.Action<DiContainer> bindAction)
		{
			// シーン遷移処理
			var loadedScene = await LoadSceneAsync(sceneID.GetName(), argument, mode, bindAction: bindAction);

			// 事前準備が終わったのでここで始める
			loadedScene.gameObject.SetActive(true);

			// 読み込み後ほかシーンに依存する設定がある場合
			foreach (var dependenceData in loadedScene.Dependences.Where(data => data.Timing.IsLoaded()))
			{
				await SceneLoadProcessAsync(dependenceData.SceneID, dependenceData.Argument, LoadSceneMode.Additive, bindAction /* todo */);
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

					scene.transform.SetParent(_sceneRoot);
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