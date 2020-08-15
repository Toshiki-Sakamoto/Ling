//
// AIBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.10
//

using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Ling.AI.Attack
{
	using CharaMaster = Ling.MasterData.Chara;

	/// <summary>
	/// 攻撃AIのベースクラス
	/// </summary>
	public abstract class AIBase : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private CharaMaster.AttackAIData _masterAIData;

		#endregion


		#region プロパティ

		/// <summary>
		/// 行動できるか
		/// </summary>
		public bool IsActable { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(CharaMaster.AttackAIData attackAIData)
		{
			_masterAIData = attackAIData;
		}

		/// <summary>
		/// 思考処理
		/// 非同期にしているのは、逐次処理を戻すことで１フレーム内の思考時間最大数超えていた場合次フレームに回すため
		/// </summary>
		public abstract UniTask ThinkAsync(Chara.ICharaController chara);

		public void Reset()
		{
			IsActable = false;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
