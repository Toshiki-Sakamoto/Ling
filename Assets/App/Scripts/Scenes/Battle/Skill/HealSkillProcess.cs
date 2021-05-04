//
// HealSkillPorcess.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.04
//

using Ling.MasterData.Skill;

namespace Ling.Scenes.Battle.Skill
{
	/// <summary>
	/// 回復スキル
	/// </summary>
	public class HealSkillProcess : Utility.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Chara.ICharaController _chara;
		private SkillHealEntity _entity;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		public void Setup(Chara.ICharaController chara, SkillHealEntity entity)
		{
			_chara = chara;
			_entity = entity;
		}

		#endregion


		#region public, protected 関数

		protected override void ProcessStartInternal()
		{
			_chara.Status.HP.AddCurrent(_entity.HP);
			ProcessFinish();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
