//
// BattleManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle
{
	/// <summary>
	/// BattleScene全体を管理する
	/// </summary>
	public class BattleManager : Utility.MonoSingleton<BattleManager>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private BattleModel _model;

		#endregion


		#region プロパティ

		public EventHolder EventHolder { get; } = new EventHolder();

		public BattleModel Model => _model;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
