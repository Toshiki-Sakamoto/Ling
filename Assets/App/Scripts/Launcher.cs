// 
// Boot.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.30
// 

using UnityEngine;
using Zenject;
using UniRx;
using System;
using Cysharp.Threading.Tasks;

namespace Ling
{
	/// <summary>
	/// 必ず一番初めに起動される
	/// </summary>
	[DefaultExecutionOrder(Common.ExcutionOrders.Launcher)]
	public class Launcher : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[Inject] protected MasterData.MasterManager _masterManager = null;
		[Inject] protected Common.Scene.IExSceneManager _sceneManager = null;

#if DEBUG
		[Inject] private Common.DebugConfig.DebugConfigManager _debugManager = default;
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

		protected virtual async UniTask QuickStartInternalAsync(Common.Scene.Base scene)
		{
			// マスタデータの読み込みが終わっていない場合読み込みを行う
			if (!_masterManager.IsLoaded)
			{
				await _masterManager.LoadAllAsync();
			}

			// シーン固有の処理を呼び出す
			await scene.QuickStartSceneAsync();

			_sceneManager.QuickStart(scene);
		}

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
#if DEBUG
			//var saveMenuItem = new Common.DebugConfig.DebugMenuItem.Data("Save");
			//_debugManager.RootMenu.Add(saveMenuItem);

			// 最初のデバッグの設定
			{
				var menuItem = new Common.DebugConfig.DebugMenuItem.Data("Battle");
				var aStarScoreShow = new Common.DebugConfig.DebugButtonItem.Data("AStarのスコアを表示する");
				menuItem.Add(aStarScoreShow);
				_debugManager.RootMenu.Add(menuItem);
			}

			Observable.EveryUpdate()
				.Where(_ => Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
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
		void OnDestoroy()
		{
		}

		#endregion
	}
}