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
    /// スキルの範囲
    /// </summary>
    public enum RangeType
    {
		[LabelText("足元")]
		Foot,

        [LabelText("直線")]
        Line,

        [LabelText("直線:貫通")]
        LinePnt,
    }
}
