//
// UserDataManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using System;
using Cysharp.Threading.Tasks;
using Zenject;
using Utility.GameData;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.UserData
{
#if DEBUG
	public class UserDataDebugMenu : Utility.GameData.GameDataDebugMenu
	{
		public UserDataDebugMenu()
			: base("UserData")
		{
		}
	} 
#endif


#if DEBUG
	/// <summary>
	/// UserDataデバッガ
	/// </summary>
	public interface IUserDataDebuggable
	{
		void SetDebugMenu(UserDataDebugMenu userDataDebugMenu);
	}
#endif

	public interface IUserDataInitializable
	{
		/// <summary>
		/// 初回読み込み時
		/// </summary>
		void OnFirstLoad();
	}

	/// <summary>
	/// UserDataはローカルファイルとして保存/読み込みする
	/// </summary>
	public class LocalFileLoader : IGameDataLoader, IGameDataSaver
	{
		[Inject] private UserDataDebugMenu _userDataDebugMenu;
		[Inject] private Utility.SaveData.ISaveDataHelper _saveDataHelper;


		async UniTask<T> IGameDataLoader.LoadAssetAsync<T>(string key)
			where T : class
		{
			var instance = default(T);

			// あれば読み込む。なければ作成
			if (!Exists<T>(key))
			{
				var saver = (IGameDataSaver)this;
				instance = saver.Save(key, default(T));
			}
			else
			{
				instance = _saveDataHelper.Load<T>("UserData.save", $"{key}");
			}

#if DEBUG
			// IUserDataDebuggerを継承していたら
			if (instance is IUserDataDebuggable debuggable)
			{
				debuggable.SetDebugMenu(_userDataDebugMenu);
			}
#endif

			return instance;
		}

		async UniTask<IList<T>> IGameDataLoader.LoadAssetsAsync<T>(string key)
			where T : class
		{
			return default(IList<T>);
		}


		T IGameDataSaver.Save<T>(string key, T value)
			where T : class
		{
			if (value == null)
			{
				value = ScriptableObject.CreateInstance(typeof(T)) as T;

				if (value is IUserDataInitializable initializable)
				{
					initializable.OnFirstLoad();
				}
			}

			_saveDataHelper.Save("UserData.save", $"{key}", value);

			return value as T;
		}


		/// <summary>
		/// ユーザーデータが存在するか
		/// </summary>
		protected bool Exists<T>(string key)
		{
			return _saveDataHelper.Exists("UserData.save", $"{key}");
		}
	}


	public interface IUserDataManager
	{
		bool IsLoaded { get; }

		UniTask LoadAll();
		void SaveAll();

		TGameData GetData<TGameData>() where TGameData : class;

		TRepository GetRepository<TRepository>();
	}

	/// <summary>
	/// UserData管理
	/// </summary>
	public abstract class UserDataManager : Utility.GameData.GameDataManager, IUserDataManager
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] DiContainer _diContainer;
		[Inject] Utility.SaveData.ISaveDataHelper _saveDataHelper;

		private IGameDataSaver _saver;

#if DEBUG
		protected UserDataDebugMenu _debugMenu;
#endif

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数


		void IUserDataManager.SaveAll()
		{
			// リポジトリの保存
			foreach (var pair in _repositoryDict)
			{
				if (!(pair.Value is IGameDataSavable savable)) continue;

				savable.Save(_saver);
			}
		}
		
		protected void LoadFinished()
		{
			LoadFinished<UserDataLoadedEvent>();
		}

		#endregion


		#region private 関数

		public void Awake()
		{
#if DEBUG
			_debugMenu = _rootMenuData.CreateAndAddItem<UserDataDebugMenu>();
#endif

			// Loaderを生成する
			var loader = _diContainer.Instantiate<LocalFileLoader>();
			_saver = loader;

			SetLoader(loader);
		}

		#endregion
	}
}
