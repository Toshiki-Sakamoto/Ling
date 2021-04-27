//
// MasterRepositoryContainer.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.27
//

using System.Collections.Generic;

namespace Utility.MasterData
{
	/// <summary>
	/// RepositoryをまとめるContainer
	/// </summary>
	public abstract class MasterRepositoryContainer<TCategory, TBaseEntity>
		where TBaseEntity : MasterDataBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Dictionary<TCategory, MasterRepository<TBaseEntity>> _repositoryDict = new Dictionary<TCategory, MasterRepository<TBaseEntity>>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void AddRepository<TEntity>(TCategory category, InheritanceMasterRepository<TBaseEntity, TEntity> repository) where TEntity : TBaseEntity =>
			_repositoryDict.Add(category, repository);

		public MasterRepository<TBaseEntity> FindRepository(TCategory category) =>
			_repositoryDict.TryGetValue(category, out var value) ? value : default(MasterRepository<TBaseEntity>);

		public TRepository FindRepository<TRepository>(TCategory category) where TRepository : MasterRepository<TBaseEntity> =>
			_repositoryDict.TryGetValue(category, out var value) ? (TRepository)value : default(TRepository);

		public TBaseEntity Find(TCategory category, int id)
		{
			var repository = FindRepository(category);
			var masterData = repository.Find(id);

			if (masterData == null)
			{
				Utility.Log.Error($"検索で引っかからなかった Category:{category} ID:{id}");
			}

			return masterData;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
