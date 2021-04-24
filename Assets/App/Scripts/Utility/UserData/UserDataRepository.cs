//
// UserDataRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using Zenject;

namespace Utility.UserData
{
#if DEBUG
	public class UserDataRepositoryDebugMenu<TUserData> : Utility.GameData.RepositoryDebugMenu
		where TUserData : UserDataBase
	{
		public UserDataRepositoryDebugMenu()
			: base($"{nameof(TUserData)}")
		{

		}
	}

#endif

	/// <summary>
	/// UserData Repository
	/// </summary>
	public class UserDataRepository<T> : Utility.GameData.GameDataRepository<T>,
		IInitializable
		where T : UserDataBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

#if DEBUG
		[Inject] protected UserDataDebugMenu _userDataDebugMenu;
#endif

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数


		void IInitializable.Initialize() 
		{
#if DEBUG
			// 自分を登録
			_userDataDebugMenu.AddRepository<UserDataRepositoryDebugMenu<T>>();
#endif
		}

		#endregion


		#region private 関数

		#endregion
	}
}
