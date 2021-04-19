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
using Utility.Extensions;
using Ling.Common.Scene.Extensions;

namespace Ling.Common.Scene
{
	public interface IExSceneManager
	{
		Base Current { get; }

		void StartScene(SceneID sceneID);

		void ChangeScene(SceneID sceneID, Argument argument = null, System.Action<DiContainer> bindAction = null);

		void AddScene(SceneID sceneID, Argument argument = null, bool isStopCurrentScene = true, System.Action<DiContainer> bindAction = null);
		void AddScene(Base parent, SceneID sceneID, Argument argument = null, bool isStopCurrentScene = true, System.Action<DiContainer> bindAction = null);

		void AddSceneAsync(Base parent, SceneID sceneID, Argument argument = null, bool isStopCurrentScene = true, System.Action<DiContainer> bindAction = null);
		UniTask<TScene> AddSceneAsync<TScene>(Base parent, SceneID scene, Argument argument = null, bool isStopCurrentScene = true, System.Action<DiContainer> bindAction = null) where TScene : Base;

		void CloseScene(Base scene, SceneResult result = null);
		UniTask CloseSceneAsync(Base scene, SceneResult result = null);

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

		private static UnityEngine.SceneManagement.Scene loadedScene;

		[SerializeField] private Transform _sceneRoot;  // シーンインスタンスが配置されるルート

		[Inject] private ZenjectSceneLoader _zenjectSceneLoader = default;

		private Base _currentScene = null;
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
			SceneChangeInternalAsync(_currentScene, sceneID, argument, LoadSceneMode.Single, isStopCurrentScene: true, bindAction).Forget();
		}

		/// <summary>
		/// 指定したシーンを閉じる
		/// </summary>
		public void CloseScene(Base scene, SceneResult result = null) =>
			CloseSceneAsync(scene, result).Forget();

		public async UniTask CloseSceneAsync(Base scene, SceneResult result = null)
		{
			if (_currentScene == scene)
			{
				// CurrentSceneの場合、一つ前のシーンに戻る
				var lastSceneData = _sceneData.Last();

				// 結果を登録しておく
				lastSceneData.Result = result;

				await BackToSceneAsync(lastSceneData);
			}
			else
			{
				// 親をアクティブ状態にする
				var parent = scene.Parent;
				if (parent != null)
				{
					// 結果を登録しておく
					parent.SceneData.Result = result;

					// 開始前にどのシーンから戻ってきたかを通知する
					parent.CamebackScene(scene);

					parent.RemoveChild(scene);

					parent.IsStartScene = true;
					parent.StartScene();
				}

				// 自分を削除
				await DestroySceneAsyncInternal(scene);
			}
		}

		/// <summary>
		/// 指定したSceneDataを持つところまで戻る
		/// </summary>
		public async UniTask<Base> BackToSceneAsync(SceneID sceneID)
		{
			var sceneData = FindSceneDataBySceneID(sceneID);
			if (sceneData == null)
			{
				Utility.Log.Error($"指定したSceneIDが見つからない {sceneID}");
				return null;
			}

			return await BackToSceneAsync(sceneData);
		}
		public async UniTask<Base> BackToSceneAsync(SceneData sceneData)
		{
			while (_sceneData.Count > 0)
			{
				var elm = _sceneData.Pop();
				if (elm == sceneData) break;
			}

			// 空の場合見つからなかったのでデフォルトに戻す
			return await SceneChangeInternalAsync(_currentScene, sceneData, LoadSceneMode.Single, isStopCurrentScene: true, backScene: _currentScene);
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
		public void AddScene(SceneID sceneID, Argument argument = null, bool isStopCurrentScene = true, System.Action<DiContainer> bindAction = null)
		{
			AddScene(_currentScene, sceneID, argument, isStopCurrentScene, bindAction);
		}
		public void AddScene(Base parent, SceneID sceneID, Argument argument = null, bool isStopCurrentScene = true, System.Action<DiContainer> bindAction = null)
		{
			SceneChangeInternalAsync(parent, sceneID, argument, LoadSceneMode.Additive, isStopCurrentScene, bindAction).Forget();
		}

		public async void AddSceneAsync(Base parent, SceneID sceneID, Argument argument = null, bool isStopCurrentScene = true, System.Action<DiContainer> bindAction = null)
		{
			await SceneChangeInternalAsync(parent, sceneID, argument, LoadSceneMode.Additive, isStopCurrentScene, bindAction);
		}
		public async UniTask<TScene> AddSceneAsync<TScene>(Base parent, SceneID sceneID, Argument argument = null, bool isStopCurrentScene = true, System.Action<DiContainer> bindAction = null) where TScene : Base
		{
			var result = await SceneChangeInternalAsync(parent, sceneID, argument, LoadSceneMode.Additive, isStopCurrentScene, bindAction);
			return result as TScene;
		}

		/// <summary>
		/// 正規の手順ではなく、指定したシーンからゲームを始める
		/// </summary>
		public async UniTask QuickStartAsync(Base scene)
		{
			_currentScene = scene;

			_currentScene.SceneData = new SceneData { SceneID = SceneID.Main, Argument = new Argument() };

			// 依存しているシーンを呼び出す
			// tood: 今めっちゃ仮で適当に起動してる
			await SceneLoadProcessAsync(null, _currentScene.SceneData, LoadSceneMode.Single, scene);

			// すべてのシーンを読み込み終わったらStartSceneを呼び出す
			scene.IsStartScene = true;
			scene.StartScene();
		}

		#endregion


		#region private 関数


		private async UniTask<Base> SceneChangeInternalAsync(Base parent, SceneID sceneID, Argument argument, LoadSceneMode mode, bool isStopCurrentScene, System.Action<DiContainer> bindAction = null)
		{
			// デフォルト生成
			if (argument == null)
			{
				argument = Argument.Create();
			}

			// 1シーン、１SceneData
			var sceneData = new SceneData() { SceneID = sceneID, Argument = argument, BindAction = bindAction };

			return await SceneChangeInternalAsync(parent, sceneData, mode, isStopCurrentScene, backScene: null);
		}

		private async UniTask<Base> SceneChangeInternalAsync(Base parent, SceneData sceneData, LoadSceneMode mode, bool isStopCurrentScene, Base backScene)
		{
			var prevScene = _currentScene;

			// 現在のシーンを止める
			// 止めるかどうかは引数で判断する
			if (isStopCurrentScene)
			{
				if (prevScene != null)
				{
					await StopSceneAsyncInternal(prevScene, needsChildren: true);
				}
			}

			if (mode == LoadSceneMode.Single)
			{
				// 遷移前処理

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

			// シーン読み込み
			var loadedScene = await SceneLoadProcessAsync(parent, sceneData, mode);

			// あるシーンから戻った場合、メソッドを呼び出す
			if (backScene != null)
			{
				loadedScene.CamebackScene(backScene);
			}

			// すべてのシーンを読み込み終わったらStartSceneを呼び出す
			loadedScene.IsStartScene = true;
			loadedScene.StartScene();

			// 前回のシーンを削除する
			if (mode == LoadSceneMode.Single)
			{
				if (prevScene != null)
				{
					await DestroySceneAsyncInternal(prevScene);

					prevScene.Parent?.RemoveChild(prevScene);
				}
			}

			return loadedScene;
		}

		/// <summary>
		/// シーン読み込みの一連の処理を行う
		/// </summary>
		private async UniTask<Base> SceneLoadProcessAsync(Base parent, SceneData sceneData, LoadSceneMode mode, Base loadedScene = null)
		{
			// シーン遷移処理
			if (loadedScene == null)
			{
				loadedScene = await LoadSceneAsync(parent, sceneData, mode);
			}

			// 事前準備が終わったのでここで始める
			loadedScene.gameObject.SetActive(true);

			// 読み込み後ほかシーンに依存する設定がある場合
			if (!loadedScene.Dependences.IsNullOrEmpty())
			{
				foreach (var dependenceData in loadedScene.Dependences.Where(data => data.Timing.IsLoaded()))
				{
					await SceneLoadProcessAsync(loadedScene, dependenceData.Data, LoadSceneMode.Additive);
				}
			}

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
		private IObservable<Base> LoadSceneAsync(Base parent, SceneData sceneData, LoadSceneMode mode = LoadSceneMode.Single, System.Action onLoaded = null)
		{
			var sceneName = sceneData.SceneID.GetName();

			return Observable.FromCoroutine<Unit>(observer_ =>
				LoadSceneOperationAsync(_zenjectSceneLoader.LoadSceneAsync(sceneName, mode, sceneData.BindAction), observer_))
				.Select(scene_ =>
				{
					// LoadSceneOperationAsync内のOnNextが呼び出されたときに来る
					var activeSceneInstance = loadedScene;
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

					// シーンにUnityシーン情報をもたせる
					scene.Scene = activeSceneInstance;

					/////	scene.transform.SetParent(_sceneRoot);
					scene.SceneData = sceneData;
					scene.Argument = sceneData.Argument;

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
									// 親に関連付ける
									parent.AddChild(scene_);
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
		/// シーンが一時中断/停止される時
		/// </summary>
		private async UniTask StopSceneAsyncInternal(Base scene, bool needsChildren)
		{
			if (scene.IsStartScene)
			{
				scene.IsStartScene = false;

				// 同期処理の終了処理
				scene.StopScene();

				// 非同期処理の終了処理
				await scene.StopSceneAsync();
			}

			if (needsChildren)
			{
				foreach (var child in scene.Children)
				{
					await StopSceneAsyncInternal(child, needsChildren);
				}
			}
		}

		/// <summary>
		/// シーンの終了処理
		/// </summary>
		private async UniTask DestroySceneAsyncInternal(Base scene)
		{
			// まずは自分の上に乗っているシーンを削除する
			foreach (var addScene in scene.Children)
			{
				await DestroySceneAsyncInternal(addScene);
			}

			scene.Children.Clear();

			await StopSceneAsyncInternal(scene, needsChildren: true);

			// 現在のAddSceneに乗っている情報を持ち変える
			_currentScene?.SceneData.MoveToCacheByChildData();

			// 削除直前
			scene.DestroyScene();

			// シーン破棄
			await SceneManager.UnloadSceneAsync(scene.Scene);
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

			// シーン読み込み完了時
			SceneManager.sceneLoaded += (scene, loadMode) => 
				{
					loadedScene = scene;
				};
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
			UpdateInternal(_currentScene);
		}

		private void UpdateInternal(Base scene)
		{
			if (scene == null) return;

			if (scene.IsStartScene)
			{
				scene.UpdateScene();
			}

			foreach (var child in scene.Children)
			{
				UpdateInternal(child);
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