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

using Zenject;

namespace Ling.Define
{
	/// <summary>
	/// 敵の種類
	/// </summary>
	public enum EnemyType
	{
		None,
		Slime,	// スライム
	}

	/// <summary>
	/// ステージの種類
	/// </summary>
	public enum StageType
	{
		None,
		First,	// 最初
	}
}
