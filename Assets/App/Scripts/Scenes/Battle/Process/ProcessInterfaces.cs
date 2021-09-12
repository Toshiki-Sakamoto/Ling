//
// ProcessUseFoodItem.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.03
//

using System.Collections.Generic;

namespace Ling.Scenes.Battle.Process
{
	/// <summary>
	/// 行動結果のターゲットを取得する
	/// </summary>
	public interface IProcessTargetGetter
	{
		List<Chara.ICharaController> Targets { get; }
	}
}
