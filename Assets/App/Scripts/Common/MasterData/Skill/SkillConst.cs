//
// SkillConst.cs
// ProductName Ling
//
// Created by  on 2021.08.09
//

using Sirenix.OdinInspector;

namespace Ling.MasterData.Skill
{
    /// <summary>
    /// スキルの範
    /// </summary>
    public enum RangeType
    {
        [LabelText("直線")]
        Line,

        [LabelText("直線:貫通")]
        LinePnt,
    }

	/// <summary>
	/// 対象
	/// </summary>
	public enum TargetType
	{
		None,

		[LabelText("味方")]
		Player,

		[LabelText("敵")]
		Enemy,

		[LabelText("全員")]
		All,
	}
}
