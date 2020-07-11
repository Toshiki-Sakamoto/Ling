//
// MinMax.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.03
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Common
{
	/// <summary>
	/// MinとMax両方保持できる
	/// </summary>
	public class MinMax<T>
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private T _min = default(T);
		[SerializeField] private T _max = default(T);

		#endregion


		#region プロパティ

		public T Min => _min;
		public T Max => _max;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}


	[System.Serializable]
	public class MinMaxFloat : MinMax<float>
	{ 
	}

	[System.Serializable]
	public class MinMaxInt : MinMax<int>
	{
		public int GetRandomValue() =>
			Utility.Random.Range(Min, Max);
	}
}
