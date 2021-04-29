//
// ICharaPostProcesser.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.29
//

using Cysharp.Threading.Tasks;

namespace Ling.Chara
{
	/// <summary>
	/// キャラクタの行動の最後に処理を行う
	/// </summary>
	public interface ICharaPostProcesser
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		/// <summary>
		/// 優先度(値が低いもの順に実行される)
		/// </summary>
		int Order { get; }

		/// <summary>
		/// 処理する必要があるか
		/// </summary>
		bool ShouldExecute { get; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 非同期処理を行う
		/// </summary>
		UniTask ExecuteAsync();

		#endregion


		#region private 関数

		#endregion
	}
}
