//
// IRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.22
//

using System.Collections.Generic;

namespace Utility.Repository
{
	/// <summary>
	/// Repository管理
	/// </summary>
	public interface IRepository<TEntity>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		List<TEntity> Entities { get; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		TEntity Find(System.Predicate<TEntity> predicate);

		#endregion


		#region private 関数

		#endregion
	}
}
