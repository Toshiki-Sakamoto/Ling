//
// UserDataRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using Zenject;
using UnityEngine;
using Utility.GameData;

namespace Utility.UserData
{
#if DEBUG
	public class UserDataRepositoryDebugMenu<TUserData> : Utility.GameData.RepositoryDebugMenu
		where TUserData : Utility.GameData.IGameDataBasic
	{
		public UserDataRepositoryDebugMenu()
			: base($"{typeof(TUserData).Name}")
		{

		}

		public override void RemoveFile()
		{
			
		}
	}

#endif

	/// <summary>
	/// UserData Repository
	/// </summary>
	public abstract class UserDataRepository<T, TUserData> : Utility.GameData.GameDataRepository<TUserData>
		, IGameDataSavable
		, IUserDataInitializable
#if DEBUG
		, IUserDataDebuggable
#endif
		where T : Utility.GameData.GameDataRepository<TUserData>
		where TUserData : Utility.UserData.UserDataBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] protected bool _isInitialized = false;	// 初期化済み()

#if DEBUG
		protected UserDataDebugMenu _userDataDebugMenu;
		protected UserDataRepositoryDebugMenu<TUserData> _debugMenu;
#endif

		#endregion


		#region プロパティ

#if DEBUG
		protected override bool EnableDebugMode => _debugMenu.EnableDebugMode.IsOn;
#endif

		/// <summary>
		/// セーブデータの保存読み込みに必要なKey
		/// </summary>
		string IGameDataSavable.SaveDataKey { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

#if DEBUG
		public void SetDebugMenu(UserDataDebugMenu userDataDebugMenu)
		{
			_userDataDebugMenu = userDataDebugMenu;
		}
#endif

		public override void Initialize() 
		{
#if DEBUG
			// 自分を登録
			_debugMenu = _userDataDebugMenu.AddRepository<UserDataRepositoryDebugMenu<TUserData>>();
#endif
		}

		/// <summary>
		/// ユニークKeyで検索をする
		/// </summary>
		public TUserData FindByUniq(Utility.UniqKey uniq)
		{
			return Entities.Find(userData => userData.Uniq == uniq);
		}

		/// <summary>
		/// 最初の読み込み時に一度だけ呼び出される
		/// </summary>
		public virtual void OnFirstLoad()
		{
		}

		/// <summary>
		/// 自分自身を保存する
		/// </summary>
		bool IGameDataSavable.Save(Utility.GameData.IGameDataSaver saver)
		{
			var savable = (IGameDataSavable)this;
			saver.Save(savable.SaveDataKey, this as T);

			return true;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
