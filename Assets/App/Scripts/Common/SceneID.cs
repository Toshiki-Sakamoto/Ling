//
// SceneID.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.13
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Common
{
	/// <summary>
	/// Secne識別子
	/// </summary>
	public static class Scene
	{
		public enum ID
		{
			None,
			StartUp, Main, Battle
		}

		public static readonly Dictionary<ID, string> _sceneIDs = 
			new Dictionary<ID, string> 
				{
					[ID.StartUp] = "StartUp",
					[ID.Main] = "Main",
					[ID.Battle] = "Battle",
				};


		public static string GetNameByID(ID id) => _sceneIDs[id];
	}
}
