﻿//
// BattlePhaseCharaCreate.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.30
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// 
	/// </summary>
	public class BattlePhaseCharaCreate : Utility.PhaseScene<BattleScene.Phase, BattleScene>.Base
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void Awake()
		{
			// プレイヤーの作成
			CharaManager.Instance.CreatePlayer();
		}

		public override void Init() 
		{
		}

		public override void Proc() 
		{
		}

		public override void Term() 
		{ 
		}

		#endregion


		#region private 関数

		#endregion
	}
}
