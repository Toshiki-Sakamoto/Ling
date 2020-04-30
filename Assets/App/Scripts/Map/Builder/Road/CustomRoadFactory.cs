//
// CustomRoadFactory.cs
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
	public class RoadBuilderFactory : PlaceholderFactory<BuilderConst.RoadType, IRoadBuilder>
	{
	}

	/// <summary>
	/// 
	/// </summary>
	public class CustomRoadFactory : IFactory<BuilderConst.RoadType, IRoadBuilder>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Dictionary<BuilderConst.RoadType, IFactory<IRoadBuilder>> _builderFactories = new Dictionary<BuilderConst.RoadType, IFactory<IRoadBuilder>>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ
		public CustomRoadFactory(SimpleRoad.Factory simpleRoadFactory)
		{
			_builderFactories[BuilderConst.RoadType.Normal] = simpleRoadFactory;
		}

		#endregion


		#region public, protected 関数

		public IRoadBuilder Create(BuilderConst.RoadType type)
		{
			if (_builderFactories.TryGetValue(type, out IFactory<IRoadBuilder> builderFactory))
			{
				return builderFactory.Create();
			}

			return null;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
