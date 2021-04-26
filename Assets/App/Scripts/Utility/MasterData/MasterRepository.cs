//
// MasterRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.11
//

using System.Collections.Generic;
using Zenject;

namespace Utility.MasterData
{
#if DEBUG
	public class MasterDataRepositoryDebugMenu<TMasterData> : Utility.GameData.RepositoryDebugMenu
		where TMasterData : MasterDataBase
	{
		public MasterDataRepositoryDebugMenu()
			: base($"{typeof(TMasterData).Name}")
		{
		}

		public override void RemoveFile()
		{

		}
	}

#endif


	/// <summary>
	/// 指定したMasterを配列で保持する
	/// </summary>
	public class MasterRepository<T> : Utility.GameData.GameDataRepository<T>,
		IInitializable
		where T : MasterDataBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

#if DEBUG
		[Inject] protected MasterDataDebugMenu _masterDataDebugMenu;

		protected MasterDataRepositoryDebugMenu<T> _debugMenu;
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
			_debugMenu = _masterDataDebugMenu.AddRepository<MasterDataRepositoryDebugMenu<T>>();
#endif
		}

		#endregion


		#region private 関数

		#endregion
	}
}
