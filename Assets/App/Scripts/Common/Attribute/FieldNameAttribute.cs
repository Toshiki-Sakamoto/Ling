//
// FieldNameAttribute.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.06.24
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
	/// [FieldNameAttribute("")] private int _huga;
	/// と打つことでインスペクタに文字列を表示させる
	/// </summary>
	public class FieldNameAttribute : PropertyAttribute
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public string Name { get; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public FieldNameAttribute(string name) =>
			Name = name;

		#endregion


		#region private 関数

		#endregion
	}
}
