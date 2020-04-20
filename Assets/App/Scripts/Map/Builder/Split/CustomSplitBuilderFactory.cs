//
// CustomSplitBuilderFactory.cs
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


namespace Ling.Map.Builder.Split
{
	public class SplitBuilderFactory : PlaceholderFactory<ISplitter>
	{
	}

	/// <summary>
	/// 
	/// </summary>
	public class CustomSplitBuilderFactory : IFactory<ISplitter>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Half.Splitter.Factory _factory;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		public CustomSplitBuilderFactory(Half.Splitter.Factory halfFactory)
		{
			_factory = halfFactory;
		}

		#endregion


		#region public, protected 関数

		public ISplitter Create()
		{
			return _factory.Create();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
