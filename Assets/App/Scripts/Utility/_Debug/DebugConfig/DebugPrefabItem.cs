//
// DebugMenu.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.08.30
//

using UnityEngine;
using System.Collections.Generic;
using Zenject;
using UnityEngine.UI;
using UniRx;

#if DEBUG
namespace Utility.DebugConfig
{
	/// <summary>
	/// 指定したPrefabを表示するアイテム
	/// </summary>
	public class DebugPrefabData : IDebugItemData
	{
		public GameObject Prefab { get; private set; }
		public System.Func<GameObject, Transform, GameObject> Creator { get; private set; }

		public DebugPrefabData(GameObject prefab)
		{
			Prefab = prefab;
		}

		void IDebugItemData.DataUpdate(GameObject obj)
		{
		}

		Const.MenuType IDebugItemData.GetMenuType() =>
			Const.MenuType.Prefab;


		public static DebugPrefabData Create(GameObject prefabObj, System.Func<GameObject, Transform, GameObject> creator)
        {
			var instance = new DebugPrefabData(prefabObj);
			instance.Creator = creator;

			return instance;
        }

		public static DebugPrefabData CreateAtPath(string path, System.Func<GameObject, Transform, GameObject> creator)
		{
			return Create(Resources.Load<GameObject>(path), creator);
		}
	}
}
#endif