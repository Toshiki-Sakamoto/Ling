//
// AIBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.26
//

namespace Ling.AI.Move
{
	/// <summary>
	/// 移動AIのベースクラス
	/// </summary>
	public abstract class AIBase
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		/// <summary>
		/// AIの種類
		/// </summary>
		public abstract Const.MoveAIType AIType { get; }

		/// <summary>
		/// 汎用パラメータ
		/// </summary>
		public int Param1 { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
