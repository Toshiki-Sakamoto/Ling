//
// EventHolder.cs
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
	/// 
	/// </summary>
	public class EventHolder
    {
		public EventPlayerMove PlayerMove { get; } = new EventPlayerMove(); 
	}
}
