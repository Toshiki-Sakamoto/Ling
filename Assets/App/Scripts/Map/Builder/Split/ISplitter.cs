//
// ISplittable.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.22
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Map.Builder.Split
{
	/// <summary>
	/// 部屋に分割する役目
	/// </summary>
    public interface ISplitter
	{
		/// <summary>
		/// 矩形を分割するとき呼び出される
		/// </summary>
        void SplitRect(MapRect mapRect);
    }
}
