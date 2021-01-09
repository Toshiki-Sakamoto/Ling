//
// MasterBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.06.24
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Common.MasterData
{
	/// <summary>
	/// Masterデータベースクラス
	/// </summary>
	public class MasterDataBase : ScriptableObject
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private int _id = default; // 大体存在する一意なID

		#endregion


		#region プロパティ

		public int ID => _id;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public virtual void Setup() { }

		#endregion


		#region private 関数

		#endregion
	}
}
