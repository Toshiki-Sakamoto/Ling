//
// BattlePhaseCharaProcessEnd.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.23
//

using Ling.Const;
using Ling.Map.TileDataMapExtensions;
using Zenject;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// プレイヤーのプロセス終了処理
	/// → 今はプレイヤーの行動が終わったら敵思考に回してるけどその途中になにかはさみたい時に使う
	/// </summary>
	public class BattlePhasePlayerProcessEnd : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数


		[Inject] private Chara.CharaManager _charaManager;
		[Inject] private Map.MapManager _mapManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseInit()
		{
		}

		public override void PhaseStart()
		{

		}


		#endregion


		#region private 関数


		#endregion
	}
}
