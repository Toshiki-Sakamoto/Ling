//
// BattlePhasePlayerAction.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
//

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// プレイヤーの攻撃
	/// </summary>
	public class BattlePhasePlayerAttack : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Chara.CharaManager _charaManager;
		private Map.MapManager _mapManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		protected override void AwakeInternal()
		{
			_charaManager = Resolve<Chara.CharaManager>();
			_mapManager = Resolve<Map.MapManager>();
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


		#endregion
	}
}
