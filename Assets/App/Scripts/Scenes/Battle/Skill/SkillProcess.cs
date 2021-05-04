//
// SkillProcess.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.03
//

namespace Ling.Scenes.Battle.Skill
{
	/// <summary>
	/// スキル効果プロセス
	/// 一つの効果のみを表す。もし複数に効果がある場合はこのプロセスがその回数分生成されるようにする
	/// 複数のプロセスに対する制御は別の担当
	/// </summary>
	public class SkillProcess : Utility.ProcessBase
	{
		#region 定数, class, enum

		public class Data
		{
			public string EffectName;	// 演出名

			// 回復系
			public HealData HealData;

			// ダメージ系
		}

		public class HealData
		{
			public int HP;	// HP回復値
		}

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

		public void Setup(string effectName)
		{

		}

		#endregion


		#region private 関数

		#endregion
	}
}
