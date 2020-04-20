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
	public class Builder : BuilderBase 
	{
		#region 定数, class, enum

		public class Factory : PlaceholderFactory<Builder>
		{ 
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private SplitBuilderFactory _splitFactory = null;     // 部屋の分割担当
		private ISplitter _splitter = null;
		private MapRect _mapRect = null;        // 区画情報

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

#if false
		public Builder()
			: this(new TSplitter())
		{
		}

		public Builder(ISplitter splitter)
		{
			_splitter = splitter;

			_mapRect = new MapRect();
		}
#endif

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 処理を実行する
		/// </summary>
		protected override void ExecuteInternal()
		{
			_splitter = _splitFactory.Create();

			_mapRect = new MapRect();

			// 全体を一つの区画にする
			_mapRect.CreateRect(0, 0, Width - 1, Height - 1);

			// 区画を作る
			_splitter?.SplitRect(_mapRect);
		}


		#endregion


		#region private 関数

		#endregion
	}
}
