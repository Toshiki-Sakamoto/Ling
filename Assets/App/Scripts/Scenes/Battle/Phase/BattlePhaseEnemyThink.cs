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
		private MapManager _mapManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void Init()
		{
			_charaManager = Resolve<Chara.CharaManager>();
			_mapManager = Resolve<MapManager>();

#if false
			// 敵は生成された順番から思考する
			foreach (var pair in _charaManager.EnemyControlGroups)
			{
				var enemyModelGroup = pair.Value;

				foreach (var enemyModel in enemyModelGroup)
				{
					// 敵が持つAIによって行動を自由に変更する
				}
			}
			#endif
		}

		#endregion


		#region private 関数

		#endregion
	}
}
