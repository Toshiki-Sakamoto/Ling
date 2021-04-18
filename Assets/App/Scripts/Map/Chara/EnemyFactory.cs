//
// EnemyFactory.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.12
//

using Ling.Chara;
using Ling.MasterData;
using Ling.MasterData.Stage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Chara
{
	/// <summary>
	/// 
	/// </summary>
	public class EnemyFactory
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

		public static void Create(EnemyControlGroup controlGroup, EnemyControl control, MasterData.Chara.EnemyMaster enemyMaster)
		{
			var model = control.Model;
			var param = new CharaModel.Param();
			param.charaType = CharaType.Enemy;

			model.Setup(param);
			model.SetStatus(enemyMaster.Status);

			control.Setup();

			// AIの設定
			var attackAIFactory = new AI.Attack.AttackAIFactory(enemyMaster.AttackAIData);
			var moveAIFactory = new AI.Move.MoveAIFactory(enemyMaster.MoveAIData);

			attackAIFactory.Attach(control);
			moveAIFactory.Attach(control);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
