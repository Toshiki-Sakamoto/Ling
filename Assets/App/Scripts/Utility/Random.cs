//
// Random.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.30
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Utility
{
	/// <summary>
	/// 
	/// </summary>
	public static class Random
    {
		/// <summary>
		/// Maxも範囲に含める
		/// </summary>
		public static int Range(int min, int max) => UnityEngine.Random.Range(min, max + 1);
		public static float Range(float min, float max) => UnityEngine.Random.Range(min, max + 1);

		/// <summary>
		/// 0からMaxまでのランダム値を取る
		/// </summary>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int Range(int max) => Range(0, max);
		public static float Range(float max) => Range(0, max);
	}
}
