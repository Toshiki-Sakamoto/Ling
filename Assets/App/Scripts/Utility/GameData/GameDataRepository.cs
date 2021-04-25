//
// IGameDataRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using System.Collections.Generic;
using Zenject;

namespace Utility.GameData
{
#if DEBUG
	public abstract class RepositoryDebugMenu : Utility.DebugConfig.DebugMenuItem.Data
	{
		public RepositoryDebugMenu(string title)
			: base(title)
		{}
	}

#endif

	public interface IGameDataRepository
	{
		void Initialize();

		void Clear();
	}

	/// <summary>
	/// User/Master データ管理リポジトリベース
	/// </summary>
	public abstract class GameDataRepository<T> : IGameDataRepository, 
		Utility.Repository.IRepository<T>
		where T : GameDataBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public List<T> Entities { get; } = new List<T>();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Add(IEnumerable<T> entities)
		{
			Entities.AddRange(entities);

			AddFinished();
		}

		public abstract void Initialize();

		public void Clear() =>
			Entities.Clear();

		/// <summary>
		/// IDから検索
		/// </summary>
		public T Find(int id) =>
			Entities.Find(entity => entity.ID == id);


		protected virtual void AddFinished()
		{
			// デバッグ処理
		}

		#endregion


		#region private 関数

		#endregion
	}
}
