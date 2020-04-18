// 
// Manager.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.04.30.
// 
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Ling.Common.Scene
{
	public interface IManager
	{
		void StartScene(Base instance, SceneID sceneID);

		void ChangeScene(SceneID sceneID, Argument argument);

		void AddScene(SceneID sceneID, Argument argument);
	}


	/// <summary>
	/// 
	/// </summary>
	[DefaultExecutionOrder(Common.ExcutionOrders.SceneManager)]
	public class Manager : MonoBehaviour, IManager
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		public static Manager Instance { get; private set; }

		#endregion


		#region private 変数

		[SerializeField] private Transform _sceneRoot;  // シーンインスタンスが配置されるルート

		private SceneID _nextSceneID = SceneID.None;
		private Base _sceneInstance = null;
		private Stack<SceneData> _sceneData = new Stack<SceneData>();
		private Stack<SceneData> _addSceneData = new Stack<SceneData>();


		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// まず初回に起動するシーンはここを呼び出す
		/// </summary>
		/// <param name="sceneID"></param>
		public void StartScene(Base instance, SceneID sceneID)
		{
			_sceneInstance = instance;

			ChangeScene(sceneID, Argument.Create());
		}

		/// <summary>
		/// シーンを完全に入れ替える`
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
			await _sceneInstance.SceneStopAsync(argument);

			var sceneData = new SceneData() { SceneID = sceneID, Argument = argument };

			if (mode == LoadSceneMode.Additive)
			{
				_addSceneData.Push(sceneData);
			}
			else
			{
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
			await LoadSceneAsync(sceneID.GetName(), argument, mode);
		}


		private IObservable<Unit> LoadSceneAsync(string sceneName, Argument argument, LoadSceneMode mode = LoadSceneMode.Single)
		{
			return Observable.FromCoroutine<Unit>(observer_ =>
				LoadSceneOperationAsync(SceneManager.LoadSceneAsync(sceneName, mode), observer_))
				.SelectMany(_ =>
				{
					var scene = GameObject.FindObjectOfType<Base>();
					scene.Argumect = argument;

					// 準備が整うまで非アクティブ
					scene.gameObject.SetActive(false);

					return scene.ScenePrepareAsync().Do(unit_ =>
						{
							// 事前準備が終わったのでここで始める`
							scene.gameObject.SetActive(true);
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
			DontDestroyOnLoad(gameObject);

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