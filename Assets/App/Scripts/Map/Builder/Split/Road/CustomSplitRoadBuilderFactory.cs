//
// CustomSplitRoadBuilderFactory.cs
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

namespace Ling.Map.Builder.Split.Road
{
	public class SplitRoadBuilderFactory : PlaceholderFactory<SplitConst.RoadBuilderType, ISplitRoadBuilder>
	{
	}

	/// <summary>
	/// 
	/// </summary>
	public class CustomSplitRoadBuilderFactory : IFactory<SplitConst.RoadBuilderType, ISplitRoadBuilder>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Dictionary<SplitConst.RoadBuilderType, IFactory<ISplitRoadBuilder>> _builderFactories = new Dictionary<SplitConst.RoadBuilderType, IFactory<ISplitRoadBuilder>>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ
		public CustomSplitRoadBuilderFactory(SimpleRoadBuilder.Factory simpleRoadFactory)
		{
			_builderFactories[SplitConst.RoadBuilderType.Simple] = simpleRoadFactory;
		}

		#endregion


		#region public, protected 関数

		public ISplitRoadBuilder Create(SplitConst.RoadBuilderType type)
		{
			if (_builderFactories.TryGetValue(type, out IFactory<ISplitRoadBuilder> builderFactory))
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
