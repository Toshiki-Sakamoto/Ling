//
// BattlePhaseEnemyThink.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.02
//

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// 敵の思考
	/// </summary>
	public class BattlePhaseEnemyThink : BattlePhaseBase
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

		public override void Init()
		{
			_charaManager = Resolve<Chara.CharaManager>();
			_mapManager = Resolve<Map.MapManager>();

			// 敵は生成された順番から思考する
			// 階層の浅い順から思考を開始する
			foreach (var pair in _charaManager.EnemyControlDict)
			{
				var enemyGroup = pair.Value;

				foreach (var enemy in enemyGroup)
				{
					// 敵が持つAIによって行動を自由に変更する
				}
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
