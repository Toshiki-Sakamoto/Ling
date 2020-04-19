//
// Builder.cs
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
using Zenject;


namespace Ling.Map.Builder.Split
{
	/// <summary>
	/// 
	/// </summary>
	public class Builder<TSplitter> : BuilderBase 
		where TSplitter : ISplitter, new()
	{
		#region 定数, class, enum

		public class Factory : PlaceholderFactory<IBuilder>
		{ 
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private ISplitter _splitter = null;     // 部屋の分割担当
		private MapRect _mapRect = null;        // 区画情報

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		public Builder()
			: this(new TSplitter())
		{
		}

		public Builder(ISplitter splitter)
		{
			_splitter = splitter;

			_mapRect = new MapRect();
		}


		#endregion


		#region public, protected 関数

		/// <summary>
		/// 処理を実行する
		/// </summary>
		protected override void ExecuteInternal()
		{
			// まずは区画を作る
			_splitter?.SplitRect(_mapRect);
		}


		#endregion


		#region private 関数

		#endregion
	}
}
