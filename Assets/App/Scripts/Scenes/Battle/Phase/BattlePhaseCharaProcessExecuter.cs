//
// BattlePhaseCharaProcess.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.23
//

using Cysharp.Threading.Tasks;

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// キャラクタの行動プロセスをすべて実行する
	/// </summary>
	public class BattlePhaseCharaProcessExecuter : BattlePhaseBase
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

		public override void Awake() 
		{ 
		}

		public override void Init() 
		{
		}

		public override void Proc() 
		{
		}

		public override void Term() 
		{ 
		}

		#endregion


		#region private 関数

		public async UniTask Execute()
		{
			// まずは移動Processをすべて叩く

			// 終わったら順番に攻撃・特技Processを叩く
		}

		#endregion
	}
}
