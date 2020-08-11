//
// CharaStatus.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.08
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Chara
{
	/// <summary>
	/// <see cref="ViewBase"/>キャラクターのHP等を管理するステイタス
	/// </summary>
	[System.Serializable]
	public class CharaStatus
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private LongReactiveProperty _hp = default;

		#endregion


		#region プロパティ

		/// <summary>
		/// 現在のHP
		/// </summary>
		public ReactiveProperty<long> HP => _hp;

		/// <summary>
		/// 死んだとき(HPが0)に通知を受ける
		/// </summary>
		public ReadOnlyReactiveProperty<bool> IsDead { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		public CharaStatus(long hp)
		{
			_hp = new LongReactiveProperty(hp);
			IsDead = HP.Select(hp_ => hp_ <= 0).ToReadOnlyReactiveProperty();
		}

		public CharaStatus(MasterData.Chara.StatusData statusData)
			: this(statusData.HP)
		{
		}

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
