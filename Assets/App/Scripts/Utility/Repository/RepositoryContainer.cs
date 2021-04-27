//
// RepositoryContainer.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.22
//

using System.Collections.Generic;
using Utility.Repository;
using Utility.GameData;

namespace Utility.Repository
{
	/// <summary>
	/// RepositoryをまとめるContainer
	/// </summary>
	public abstract class RepositoryContainer<TCategory, TBaseEntity>
		where TBaseEntity : Utility.GameData.IGameDataBasic
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Dictionary<TCategory, List<TBaseEntity>> _entityDict = new Dictionary<TCategory, List<TBaseEntity>>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Add<TEntity>(TCategory category, GameDataRepository<TEntity> repository) where TEntity : TBaseEntity
		{
//////			_entityDict.Add(category, repository.Entities.ConvertAll(entity => entity as TBaseEntity));
		}

		public List<TBaseEntity> FindEntities(TCategory category) =>
			_entityDict.TryGetValue(category, out var value) ? value : default(List<TBaseEntity>);

		#endregion


		#region private 関数

		#endregion
	}
}
