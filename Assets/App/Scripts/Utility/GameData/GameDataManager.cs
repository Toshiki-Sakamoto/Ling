// 
// GameDataManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.22
// 

using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Utility.GameData
{
#if DEBUG
	
	/// <summary>
	/// 基本的なデバッグ機能をもたせる
	/// </summary>
	public class GameDataDebugMenu: Utility.DebugConfig.DebugMenuItem.Data
	{
		// 全データ削除
		public Utility.DebugConfig.DebugButtonItem.Data ClearAllDataButton;

		// リポジトリはかならずある
		private List<RepositoryDebugMenu> _repositoryDebugs = new List<RepositoryDebugMenu>();

		public GameDataDebugMenu(string title)
			: base(title)
		{
			ClearAllDataButton = new Utility.DebugConfig.DebugButtonItem.Data("全データクリア");

			Add(ClearAllDataButton);
		}

		public void AddRepository<TRepository>() where TRepository : RepositoryDebugMenu, new()
		{
			_repositoryDebugs.Add(CreateAndAddItem<TRepository>());
		}
	}

#endif

	/// <summary>
	/// Master/User データ管理の基礎クラス
	/// </summary>
	public abstract class GameDataManager : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

#if DEBUG
		[Inject] private DiContainer _diContainer;
		[Inject] protected Utility.DebugConfig.DebugRootMenuData _rootMenuData;
#endif

		private Dictionary<Type, IGameDataRepository> _repositoryDict = new Dictionary<Type, IGameDataRepository>();
		private Dictionary<Type, GameDataBase> _dataDict = new Dictionary<Type, GameDataBase>();
		private IGameDataLoader _loader;

		protected List<UniTask> _loadTasks = new List<UniTask>();

		#endregion


		#region プロパティ

		/// <summary>
		/// 読み込み済みの場合true
		/// </summary>
		public bool IsLoaded { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public abstract UniTask LoadAll();

		public void SetLoader(IGameDataLoader loader) =>
			_loader = loader;

		public TGameData GetData<TGameData>()
			where TGameData : GameDataBase
		{
			if (!_dataDict.TryGetValue(typeof(TGameData), out var result))
			{
				// todo: エラー処理
			}

			return (TGameData)result;
		}

		/// <summary>
		/// 読み込み済みのリポジトリを取得する
		/// </summary>
		public TRepository GetRepository<TRepository>()
		{
			if (!_repositoryDict.TryGetValue(typeof(TRepository), out var result))
			{
				// todo: エラー処理
			}

			return (TRepository)result;
		}


		#endregion


		#region private 関数

		/// <summary>
		/// ロード処理リストに突っ込む
		/// </summary>
		protected void AddLoadTask<TGameData>(string key) where TGameData : GameDataBase
		{
			_loadTasks.Add(LoadAsync<TGameData>(key, master =>
				{
					// todo: 以前のデータが存在する場合、削除するかClearするだけにするか決めること
					_dataDict.Add(typeof(TGameData), master);
				}));
		}

		protected void AddLoadRepositoryTask<TGameData, TRepository>(string key)
			where TGameData : GameDataBase
			where TRepository : GameDataRepository<TGameData>, new()
		{
			// todo: 以前のデータが存在する場合、削除するかClearするだけにするか決めること
			var repository = _diContainer.Instantiate<TRepository>();
			repository.Initialize();
			
			_repositoryDict.Add(typeof(TRepository), repository);

			_loadTasks.Add(LoadRepositoryAsync<TGameData>(key, repository));
		}

		/// <summary>
		/// 実際の非同期読み込み処理
		/// </summary>
		protected async UniTask LoadAsync<T>(string key, System.Action<T> onSuccess) where T : GameDataBase
		{
			var master = await _loader.LoadAssetAsync<T>(key);

			onSuccess?.Invoke(master);
		}

		/// <summary>
		/// 指定フォルダ以下のMasterを検索し、Repositoryにすべて格納する
		/// </summary>
		protected async UniTask LoadRepositoryAsync<T>(string key, GameDataRepository<T> repository) where T : GameDataBase
		{
			var masters = await _loader.LoadAssetsAsync<T>(key);
			repository.Add(masters);

			#if false
			// 指定マスタデータをすべて読み込む
			foreach (var guid in AssetDatabase.FindAssets($"t:{typeof(T).Name}"))
			{
				var filePath = AssetDatabase.GUIDToAssetPath(guid);
				if (string.IsNullOrEmpty(filePath)) continue;

				// Resourcesファイルパス以下にする
				filePath = Regex.Replace(filePath, ".*/Resources/(.*).asset", "$1");

				var master = await LoadAsyncAtPath<T>(filePath);

				repository.Add(master);
			}
			#endif
		}

		/// <summary>
		/// すべての読み込みが終了したときに呼び出す
		/// </summary>
		protected void LoadFinished<TEvent>() where TEvent : class, new()
		{
			Utility.EventManager.SafeTrigger(new TEvent { });
		}

		#endregion
	}
}