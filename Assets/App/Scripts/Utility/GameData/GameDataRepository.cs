//
// IGameDataRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using System.Collections.Generic;

namespace Utility.GameData
{
	public interface IGameDataRepository
	{
		void Clear();
	}

	/// <summary>
	/// User/Master データ管理リポジトリベース
	/// </summary>
	public class GameDataRepository<T> : IGameDataRepository, Utility.Repository.IRepository<T>
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

		public void Add(T master)
		{
			Entities.Add(master);
		}

		public void Clear() =>
			Entities.Clear();

		/// <summary>
		/// IDから検索
		/// </summary>
		public T Find(int id) =>
			Entities.Find(entity => entity.ID == id);

		#endregion


		#region private 関数

		#endregion
	}
}
