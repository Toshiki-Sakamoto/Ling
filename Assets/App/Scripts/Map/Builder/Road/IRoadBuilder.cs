//
// IRoadCreator.cs
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


namespace Ling.Map.Builder.Road
{
	/// <summary>
	/// 
	/// </summary>
	public interface IRoadBuilder
	{
		IEnumerator<float> Build(BuilderData builderData);
	}
}
