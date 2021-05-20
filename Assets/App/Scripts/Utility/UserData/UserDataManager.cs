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
	public class LocalFileLoader : IGameDataLoader
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
				instance = Save<T>(key);
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


		/// <summary>
		/// ユーザーデータが存在するか
		/// </summary>
		protected bool Exists<T>(string key)
		{
			return _saveDataHelper.Exists("UserData.save", $"{key}");
		}

		public T Save<T>(string key)
			where T : class
		{
			var instance = ScriptableObject.CreateInstance(typeof(T)) as T;
			if (instance is IUserDataInitializable initializable)
			{
				initializable.OnFirstLoad();
			}

			_saveDataHelper.Save("UserData.save", $"{key}", instance);

			return instance as T;
		}
	}


	public interface IUserDataManager
	{
		bool IsLoaded { get; }

		UniTask LoadAll();

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

#if DEBUG
		protected UserDataDebugMenu _debugMenu;
#endif

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数
		
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
			SetLoader(loader);
		}

		#endregion
	}
}
