//
// RepositoryContainer.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.22
//

using System.Collections.Generic;

namespace Ling.Common.Repotitory
{
	/// <summary>
	/// RepositoryをまとめるContainer
	/// </summary>
	public abstract class RepositoryContainer<TCategory, TBaseEntity>
		where TBaseEntity : class
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Dictionary<TCategory, IRepository<TBaseEntity>> _repositoryDict = new Dictionary<TCategory, IRepository<TBaseEntity>>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Update<TEntity>(TCategory category, IRepository<TEntity> repository) where TEntity : class, TBaseEntity =>
			_repositoryDict.Add(category, (IRepository<TBaseEntity>)repository);

		public IRepository<TBaseEntity> FindRepository(TCategory category) =>
			_repositoryDict.TryGetValue(category, out var value) ? value : null;

		public TBaseEntity Find(TCategory category, int id)
		{
			var repository = FindRepository(category);
			if (repository == null) return null;

			return repository.Find(id);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
