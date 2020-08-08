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

		public static CharaModel Create(MapEnemyData mapEnemyData)
		{
			var enemyMaster = MasterManager.Instance.EnemyRepository.Find(mapEnemyData.EnemyType);
			var charaModel = new CharaModel();
			charaModel.Setup(enemyMaster.Status);
			charaModel.SetAIType(enemyMaster.AttackAIType, enemyMaster.MoveAIType);

			return charaModel;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
