//
// Vector3Extension.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.11.17
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling
{
	/// <summary>
	/// 
	/// </summary>
    public static class Vector3Extensions
    {
        /// <summary>
        /// Vector3同士の掛け算
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 Multiple(this Vector3 left, Vector3 right)
        {
            return new Vector3 { x = left.x * right.x, y = left.y * right.y, z = left.z * right.z };
        }
    }
}
