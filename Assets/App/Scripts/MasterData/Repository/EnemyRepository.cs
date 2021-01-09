//
// EnemyRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.11
//

using Ling.MasterData.Chara;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.MasterData.Repository
{
	/// <summary>
	/// <see cref="EnemyMaster"/>
	/// </summary>
	public class EnemyRepository : Common.MasterData.MasterRepository<EnemyMaster>
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

		public EnemyMaster Find(Const.EnemyType enemyType) =>
			Entities.Find(entity => entity.EnemyType == enemyType);

		#endregion


		#region private 関数

		#endregion
	}
}
