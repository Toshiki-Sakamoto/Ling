//
// MasterManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.06.24
//

using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using Zenject;
using Utility.GameData;

namespace Utility.MasterData
{
#if DEBUG
	public class MasterDataDebugMenu : Utility.GameData.GameDataDebugMenu
	{
		public MasterDataDebugMenu()
			: base("MasterData")
		{
		}
	} 
#endif

	public interface IMasterManager
	{
		/// <summary>
		/// マスタデータ読み込み済みの場合true
		/// </summary>
		bool IsLoaded { get; }

		/// <summary>
		/// 全マスタデータの読み込みを行う
		/// </summary>
		UniTask LoadAll();


		TGameData GetData<TGameData>() where TGameData : class;

		TRepository GetRepository<TRepository>();
	}

	/// <summary>
	/// MasterDataはアセットバンドルとして読み込む
	/// </summary>
	public class AssetBundleLoader : IGameDataLoader
	{
		[Inject] Utility.AssetBundle.AssetBundleManager _assetBundleManager = default;

		async UniTask<T> IGameDataLoader.LoadAssetAsync<T>(string key)
		{
			var result = await _assetBundleManager.LoadAssetAsync<T>(key);

			// ここでリリースしても参照は残る？
			_assetBundleManager.Release(result);

			return result;
		}

		async UniTask<IList<T>> IGameDataLoader.LoadAssetsAsync<T>(string key)
		{
			var result = await _assetBundleManager.LoadAssetsAsync<T>(key);

			// ここでリリースしても参照は残る？
			_assetBundleManager.Release(result);

			return result;
		}
	}

	/// <summary>
	/// マスタデータ管理者
	/// </summary>
	public abstract class MasterManager : Utility.GameData.GameDataManager, IMasterManager
	{
		#region 定数, class, enum


		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] DiContainer _diContainer;

#if DEBUG
		protected MasterDataDebugMenu _debugMenu;
#endif

		#endregion


		#region プロパティ


		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		protected void LoadFinished()
		{
			LoadFinished<MasterLoadedEvent>();
		}

		protected void AddLoadRepositoryTask<TGameData, TRepository>(string key)
			where TGameData : MasterDataBase
			where TRepository : MasterRepository<TGameData>, new()
		{
			var repository = _diContainer.Instantiate<TRepository>();
			repository.Initialize();

			_repositoryDict.Add(typeof(TRepository), repository);

			_loadTasks.Add(LoadRepositoryAsync<TGameData>(key, repository));
		}

		protected void AddLoadRepositoryTask<TGameData, TBaseData, TRepository>(string key)
			where TGameData : TBaseData
			where TBaseData : MasterDataBase
			where TRepository : InheritanceMasterRepository<TBaseData, TGameData>, new()
		{
			// todo: 以前のデータが存在する場合、削除するかClearするだけにするか決めること
			var repository = _diContainer.Instantiate<TRepository>();
			repository.Initialize();

			_repositoryDict.Add(typeof(TRepository), repository);

			_loadTasks.Add(LoadRepositoryAsync<TBaseData, TGameData>(key, repository));
		}

		protected async UniTask LoadRepositoryAsync<T, U>(string key, InheritanceMasterRepository<T, U> repository) 
			where T : MasterDataBase
			where U : T
		{
			var masters = await _loader.LoadAssetsAsync<U>(key);
			repository.Add(masters);
			repository.AddFinished();
		}

		#endregion


		#region private 関数

		private void Awake()
		{
			// Loaderを生成する
			var loader = _diContainer.Instantiate<AssetBundleLoader>();
			SetLoader(loader);

#if DEBUG
			_debugMenu = _rootMenuData.CreateAndAddItem<MasterDataDebugMenu>();
#endif
		}

		#endregion
	}
}
