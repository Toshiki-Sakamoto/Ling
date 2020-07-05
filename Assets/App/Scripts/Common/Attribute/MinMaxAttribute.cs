//
// MinMaxRangeAttribute.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.06.29
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Common.Attribute
{
	/// <summary>
	/// 最小値と最大値を設定できる。Attribute
	/// </summary>
	public class MinMaxAttribute : PropertyAttribute
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public float Min { get; private set; }
		public float Max { get; private set; }

		/// <summary>
		/// 表示名 : Nullの場合変数名
		/// </summary>
		public string FieldName { get; private set; }

		public string MinPropertyName { get; } = "_min";
		public string MaxPropertyName { get; } = "_max";

		#endregion


		#region コンストラクタ, デストラクタ

		public MinMaxAttribute(float min, float max, string fieldName = null)
		{
			Min = min;
			Max = max;
			FieldName = fieldName;
		}

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
