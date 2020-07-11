//
// StageRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.11
//

using Ling.MasterData.Stage;
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
	/// 
	/// </summary>
	public class StageRepository : MasterRepository<StageMaster>
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

		/// <summary>
		/// StageMasterを検索
		/// </summary>
		public StageMaster FindByStageType(Define.StageType stageType) =>
			Entities.Find(stageMaster => stageMaster.StageType == stageType);

		#endregion


		#region private 関数


		#endregion
	}
}
