﻿//
// UserDataManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using System;
using Cysharp.Threading.Tasks;
using Zenject;

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

	public interface IUserDataManager
	{
		bool IsLoaded { get; }

		UniTask LoadAll();

		TGameData GetData<TGameData>() where TGameData : Utility.GameData.GameDataBase;

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


#if DEBUG
		private UserDataDebugMenu _debugMenu;
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
		}

		#endregion
	}
}