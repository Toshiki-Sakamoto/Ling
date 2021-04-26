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
	public class UserDataRepository<T> : Utility.GameData.GameDataRepository<T>
		where T : Utility.GameData.IGameDataBasic
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

#if DEBUG
		[Inject] protected UserDataDebugMenu _userDataDebugMenu;

		protected UserDataRepositoryDebugMenu<T> _debugMenu;
#endif

		#endregion


		#region プロパティ

#if DEBUG
		protected override bool EnableDebugMode => _debugMenu.EnableDebugMode.IsOn;
#endif

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void Initialize() 
		{
#if DEBUG
			// 自分を登録
			_debugMenu = _userDataDebugMenu.AddRepository<UserDataRepositoryDebugMenu<T>>();
#endif
		}

		#endregion


		#region private 関数

		#endregion
	}
}
