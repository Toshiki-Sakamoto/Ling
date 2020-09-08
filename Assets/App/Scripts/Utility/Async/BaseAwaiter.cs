//
// BaseAwaiter.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.29
//

using Cysharp.Threading.Tasks;

namespace Ling.Utility.Async
{
	/// <summary>
	/// Awaiterのベースクラス
	/// </summary>
	public abstract class BaseAwaiter
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
		/// 処理実行時指定時間が過ぎていた場合awaitを行う
		/// </summary>
		public abstract UniTask Wait();

		public abstract void Reset();

		#endregion


		#region private 関数

		#endregion
	}
}
