//
// Events.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.03
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle
{
	/// <summary>
	/// Playerの移動距離
	/// </summary>
	public class EventPlayerMove
	{
		public Vector3Int moveDistance;	// 移動距離
	}

	public class EventMessageText
	{
		public string text;
	}

	/// <summary>
	/// 選択肢を出す
	/// </summary>
	public class EventMessageTextSelect
	{
		public string text;
		public string[] selectTexts;
		public System.Action<int> onSelected;
	}

	/// <summary>
	/// Phaseを変更させる
	/// </summary>
	public class EventChangePhase
	{
		public BattleScene.Phase phase;
	}
}
