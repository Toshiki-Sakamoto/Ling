//
// GameManager.cs
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
	public class GameManager : Utility.MonoSingleton<GameManager>
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private DiContainer _diContainer;

		#endregion


		#region プロパティ

		public EventHolder EventHolder { get; } = new EventHolder();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// DIContainerから指定した参照の解決
		/// </summary>
		public TContract Resolve<TContract>() => _diContainer.Resolve<TContract>();

		#endregion


		#region private 関数

		#endregion
	}
}
