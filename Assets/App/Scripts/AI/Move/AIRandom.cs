//
// AIRandom.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.10
//

namespace Ling.AI.Move
{
	/// <summary>
	/// 同じ部屋にプレイヤーがいても追っかけたりしない
	/// </summary>
	public class AIRandom : AIBase
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
		/// AIの種類
		/// </summary>
		public override Const.MoveAIType AIType => Const.MoveAIType.Random;

		#endregion


		#region private 関数

		#endregion
	}
}
