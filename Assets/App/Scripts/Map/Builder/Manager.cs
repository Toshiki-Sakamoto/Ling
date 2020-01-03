//
// Manager.cs
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


namespace Ling.Map.Builder
{
	/// <summary>
	/// ビルダーを管理する
	/// </summary>
	public class Manager : Utility.Singleton<Manager>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Data _data = null;		// データ
		private Base _builder = null;   // 現在のビルダー

		#endregion


		#region プロパティ

		/// <summary>
		/// ビルダー本体を返す
		/// </summary>
		public Base Builder => _builder;

		/// <summary>
		/// ビルダー情報を返す
		/// </summary>
		public Data Data => _data;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void SetData(Data data)
		{
			_data = data;
			_builder?.SetData(data);
		}

		public void SetBuilder(Base builder)
		{
			_builder = builder;
			_builder?.SetData(_data);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
