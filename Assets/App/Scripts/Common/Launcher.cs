// 
// Boot.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.30
// 

using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using UniRx;
using System;
using Cysharp.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Ling.Common
{
#if UNITY_EDITOR
	[InitializeOnLoad]
	public class InitLauncher
	{
		static InitLauncher()
		{
			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private static void OnPlayModeStateChanged(PlayModeStateChange state)
		{
			//EditorSceneManager.OpenScene("Assets/App/Scenes/Manager.scene", OpenSceneMode.Additive);
			//UnityEngine.SceneManagement.SceneManager.LoadScene("Manager");
		}
	}
#endif

	/// <summary>
	/// 必ず一番初めに起動される
	/// </summary>
	public class Launcher : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[Inject] protected Common.MasterData.IMasterManager _masterManager = null;
		[Inject] protected Common.Scene.IExSceneManager _sceneManager = null;

#if DEBUG
		[Inject] protected Common.DebugConfig.DebugConfigManager _debugManager = default;
#endif

		#endregion


		#region プロパティ

		/// <summary>
		/// 現在のシーンが存在する場合、起動済みとする
		/// </summary>
		public bool IsSceneBooted => _sceneManager.Current != null;

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 指定したシーンを直接実行させる
		/// </summary>
		public void QuickStart(Common.Scene.Base scene)
		{
			QuickStartInternalAsync(scene).Forget();
		}

		#endregion


		#region private 関数

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void OnBeforeSceneLoadRuntimeMethod()
		{
			// Editor再生時にも同様の処理をするほうがいいか
			//Debug.Log("Before scene loaded " + UnityEngine.SceneManagement.SceneManager.GetAllScenes().ToString());
		}

		protected virtual async UniTask QuickStartInternalAsync(Common.Scene.Base scene)
		{
			// マスタデータの読み込みが終わっていない場合読み込みを行う
			if (!_masterManager.IsLoaded)
			{
				await _masterManager.LoadAll();
			}

			// シーン固有の処理を呼び出す
			await scene.QuickStartSceneAsync();

			await _sceneManager.QuickStartAsync(scene);
		}

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		protected virtual void Awake()
		{
#if false//DEBUG
			Observable.EveryUpdate()
				.Where(_ => Keyboard.current.leftShiftKey.isPressed && Keyboard.current.dKey.isPressed)
				.ThrottleFirst(TimeSpan.FromSeconds(0.5))
				.Subscribe(_ => 
				{
					if (_debugManager.IsOpened)
					{
						_debugManager.Close();
					}
					else
					{
						_debugManager.Open();
					}
				});
#endif
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
		void OnDestroy()
		{
		}

		#endregion
	}
}