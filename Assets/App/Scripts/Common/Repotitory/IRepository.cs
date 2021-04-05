//
// IRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.22
//

namespace Ling.Common.Repotitory
{
	/// <summary>
	/// Repository管理
	/// </summary>
	public interface IRepository<T> where T : class
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// IDから検索
		/// </summary>
		T Find(int id);

		#endregion


		#region private 関数

		#endregion
	}
}
