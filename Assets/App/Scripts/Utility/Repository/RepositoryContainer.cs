//
// RepositoryContainer.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.22
//

using System.Collections.Generic;

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

		private Dictionary<TCategory, IRepository> _repositoryDict = new Dictionary<TCategory, IRepository>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Update(TCategory category, IRepository repository) =>
			_repositoryDict.Add(category, repository);

		public IRepository FindRepository(TCategory category) =>
			_repositoryDict.TryGetValue(category, out var value) ? value : null;

		#endregion


		#region private 関数

		#endregion
	}
}
