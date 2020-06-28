// 
// PoolManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.01
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

using Zenject;


namespace Ling.Scenes.Battle
{
	public enum PoolType
	{
	}

	/// <summary>
	/// バトル中に使用するプール管理者
	/// エフェクトなど管理して使い回す
	/// </summary>
	public class PoolManager : Utility.Pool.PoolManager<PoolType, PoolManager> 
    {
		#region 定数, class, enum

		[System.Serializable]
		public class PoolInfo
		{
			public PoolType type;
			public GameObject poolObject = null;
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private List<PoolInfo> _poolInfos = null;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void SetupPoolItem(PoolType poolType, int initCreateNum)
		{
			var poolInfo = _poolInfos.Find(info_ => info_.type == poolType);
			if (poolInfo == null)
			{
				Utility.Log.Warning($"存在しないPoolType {poolType.ToString()}");
				return;
			}

			var item = AddPoolItem(poolType, poolInfo.poolObject, initCreateNum);
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour


		#endregion
	}
}