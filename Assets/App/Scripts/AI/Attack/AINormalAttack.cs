//
// AINormalAttack.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.11
//

using Cysharp.Threading.Tasks;

namespace Ling.AI.Attack
{
	/// <summary>
	/// 通常攻撃しかしない。
	/// 隣のマスにPlayerがいたら攻撃するだけ
	/// </summary>
	public class AINormalAttack : AIBase
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

		protected override async UniTask ExexuteInternalAsync(Ling.Utility.Async.WorkTimeAwaiter timeAwaiter) 
		{
			// 自分の８方向のマスにPlayerが存在するか
			// しなければもう何もしない	
		}

		#endregion


		#region private 関数

		#endregion
	}
}
