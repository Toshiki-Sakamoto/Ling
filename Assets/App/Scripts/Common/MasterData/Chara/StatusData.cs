//
// StatusData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.11
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.MasterData.Chara
{
	/// <summary>
	/// キャラクターのHP等を管理するステイタス
	/// </summary>
	[System.Serializable]
	public class StatusData
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Header("HP")]
		[SerializeField] private long _hp = default;

		[Header("スタミナ")]
		[SerializeField] private long _stamina = default;

		[Header("ちから")]
		[SerializeField] private int _power = default;

		#endregion


		#region プロパティ

		public long HP => _hp;

		public long Stamina => _stamina;

		public int Power => _power;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
