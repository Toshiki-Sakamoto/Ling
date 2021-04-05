//
// BuilderFactory.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.19
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Ling.Map.Builder
{
	public class BuilderFactory : PlaceholderFactory<BuilderConst.BuilderType, IBuilder>
	{
	}

	/// <summary>
	/// 
	/// </summary>
	public class CustomBuilderFactory : IFactory<BuilderConst.BuilderType, IBuilder>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Dictionary<BuilderConst.BuilderType, IFactory<IBuilder>> _builderFactories = new Dictionary<BuilderConst.BuilderType, IFactory<IBuilder>>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		public CustomBuilderFactory(Split.Builder.Factory splitFactory)
		{
			_builderFactories[BuilderConst.BuilderType.Split] = splitFactory;
		}

		#endregion


		#region public, protected 関数

		public IBuilder Create(BuilderConst.BuilderType type)
		{
			if (_builderFactories.TryGetValue(type, out IFactory<IBuilder> builderFactory))
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
