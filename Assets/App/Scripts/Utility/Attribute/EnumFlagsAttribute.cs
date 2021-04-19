// 
// EnumFlagsAttribute.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.06.07
// 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;


namespace Utility.Attribute
{
	/// <summary>
	/// Attributeを作成
	/// </summary>
	/// <remarks>
	/// sealed class は以降どんなクラスも継承できなくなる
	/// </remarks>
	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
	public sealed class EnumFlagsAttribute : PropertyAttribute { }

}