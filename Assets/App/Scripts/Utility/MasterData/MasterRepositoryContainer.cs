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

		private Dictionary<TCategory, IInheritenceMasterRepository<TBaseEntity>> _repositoryDict = new Dictionary<TCategory, IInheritenceMasterRepository<TBaseEntity>>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void AddRepository(TCategory category, IInheritenceMasterRepository<TBaseEntity> repository) =>
			_repositoryDict.Add(category, repository);

		public IInheritenceMasterRepository<TBaseEntity> FindRepository(TCategory category) =>
			_repositoryDict.TryGetValue(category, out var value) ? value : default(IInheritenceMasterRepository<TBaseEntity>);

		public TRepository FindRepository<TRepository>(TCategory category) where TRepository : IInheritenceMasterRepository<TBaseEntity> =>
			_repositoryDict.TryGetValue(category, out var value) ? (TRepository)value : default(TRepository);

		public TBaseEntity Find(TCategory category, int id)
		{
			var repository = FindRepository(category);
			var data = repository.FindAtBase(id);

			if (data == null)
			{
				Utility.Log.Error($"検索で引っかからなかった Category:{category} ID:{id}");
			}

			return data;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
