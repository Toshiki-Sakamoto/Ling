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
		where TMasterData : Utility.GameData.IGameDataBasic
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


		/// <summary>
		/// IDから検索
		/// </summary>
		public T Find(int id)
		{
			var entity = Entities.Find(entity => entity.ID == id);
			if (entity == null)
			{
				Utility.Log.Error($"IDから見つけられない {id}");
			}

			return entity;
		}


		#endregion


		#region private 関数

		#endregion
	}

	public interface IInheritenceMasterRepository<TEntity>
		where TEntity : MasterDataBase
	{
		TEntity FindAtBase(int id);
	}

	public abstract class InheritanceMasterRepository<TEntity, UEntity> : MasterRepository<UEntity>,
		IInheritenceMasterRepository<TEntity>
		where TEntity : MasterDataBase
		where UEntity : TEntity
	{
		TEntity IInheritenceMasterRepository<TEntity>.FindAtBase(int id) =>
			Find(id) as TEntity;
	}
}
