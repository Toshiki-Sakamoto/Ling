//
// SkillProcess.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.03
//

using Ling.MasterData.Skill;

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

		private Chara.ICharaController _chara;
		private SkillMaster _skill;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(Chara.ICharaController chara, SkillMaster skill)
		{
			_chara = chara;
			_skill = skill;
		}

		protected override void ProcessStartInternal()
		{
			AttachProcess();

			ProcessFinish();
		}

		#endregion


		#region private 関数

		private void AttachProcess()
		{
			// スキル内容によってプロセスを後ろにつけ合わす
			if (_skill.Heal != null)
			{
				SetNext<HealSkillProcess>().Setup(_chara, _skill.Heal);
			}

			if (_skill.Damage != null)
			{

			}
		}

		#endregion
	}
}
