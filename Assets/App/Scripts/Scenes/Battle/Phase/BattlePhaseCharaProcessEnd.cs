//
// BattlePhaseCharaProcessEnd.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.23
//

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// キャラのプロセス終了処理
	/// </summary>
	public class BattlePhaseCharaProcessEnd : BattlePhaseBase
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
			// 今の所何もすることないのでプレイヤー行動開始時に戻す
			// 足元確認とか次に入れるほうが良さそう
			Change(BattleScene.Phase.PlayerAction);
		}


		#endregion


		#region private 関数

		#endregion
	}
}
