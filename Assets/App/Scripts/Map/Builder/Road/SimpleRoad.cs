//
// SimpleRoad.cs
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
using Zenject;

namespace Ling.Map.Builder.Road
{
	/// <summary>
	/// 
	/// </summary>
	public class SimpleRoad : IRoadBuilder
	{
		#region 定数, class, enum

		public class Factory : PlaceholderFactory<SimpleRoad> { }

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public IEnumerator<float> Build(BuilderData builderData)
		{
			yield return 0.5f;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
