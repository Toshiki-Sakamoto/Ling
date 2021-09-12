//
// EnemyType.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.05
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

using Zenject;

namespace Ling.Const
{
	/// <summary>
	/// 敵の種類
	/// </summary>
	public enum EnemyType
	{
		None,
		Slime,  // スライム
	}

	/// <summary>
	/// ステージの種類
	/// </summary>
	public enum StageType
	{
		None,
		First,  // 最初
	}

	/// <summary>
	/// 移動AIの種類
	/// </summary>
	public enum MoveAIType
	{
		None,

		Manual,             // 手動操作
		Random,             // ランダム移動
		NormalTracking,     // 通常の追跡型
	}

	/// <summary>
	/// 攻撃AIの種類
	/// </summary>
	public enum AttackAIType
	{
		None,

		Normal, // 通常の攻撃しかしない
	}

	/// <summary>
	/// 対象
	/// </summary>
	public enum TargetType
	{
		None,

		[LabelText("自分")]
		Self,

		[LabelText("味方")]
		Ally,

		[LabelText("敵")]
		Enemy,

		[LabelText("全員")]
		All,
	}
}
