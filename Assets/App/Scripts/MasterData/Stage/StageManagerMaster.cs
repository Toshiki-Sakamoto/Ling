//
// StageManagerMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.11
//

using Ling.Common.Attribute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.MasterData.Stage
{
	/// <summary>
	/// 全ての<see cref="StageMaster"/>を管理する
	/// </summary>
	[CreateAssetMenu(menuName = "MasterData/StageManagerMaster", fileName = "StageManagerMaster")]
	public class StageManagerMaster : MasterBase<StageManagerMaster>
	{
		#region 定数, class, enum

		[System.Serializable]
		public class Row
		{
			public int id;

			[FieldName("ステージの種類")]
			public Define.StageType stageType;

			[FieldName("公開するか")]
			public bool open;

			[FieldName("ステージマスタ")]
			public StageMaster stageMaster;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField]
		private Row[] _rows = default;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// StageMasterを検索
		/// </summary>
		public StageMaster FindStageMaster(Define.StageType stageType) =>
			FindRowByStageType(stageType).stageMaster;

		#endregion


		#region private 関数

		private Row FindRowByStageType(Define.StageType stageType) =>
			FindRow(row => row.stageType == stageType);

		private Row FindRow(System.Predicate<Row> predicate) =>
			System.Array.Find(_rows, predicate);

		#endregion
	}
}
