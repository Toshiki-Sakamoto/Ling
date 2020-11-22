//
// StageMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.05
//

using Ling.Utility.Attribute;
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
	/// 1ステージを管理するマスタデータ
	/// <see cref="MapMaster"/>を内部で複数持つ
	/// </summary>
	[CreateAssetMenu(menuName = "MasterData/StageMaster", fileName = "StageMaster")]
	public class StageMaster : MasterDataBase
	{
		#region 定数, class, enum

		[System.Serializable]
		public class MapEntity
		{
			[FieldName("階層")]
			public int level;	// 指定階層までMapMasterを適用させる
			
			[FieldName("適用するMapMaster")]
			public MapMaster mapMaster; // Stage内にある１Mapの情報を持つ
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("ステージの種類")]
		private Const.StageType stageType;

		[SerializeField, FieldName("最大階層")] 
		private int _maxLevel = default;    // 最大階層(Map数)

		[SerializeField] 
		private MapEntity[] _mapEntities = default;

		#endregion


		#region プロパティ

		public Const.StageType StageType => stageType;

		/// <summary>
		/// Map数(最大階層数)
		/// </summary>
		public int MapCount => _maxLevel;

		/// <summary>
		/// MapEntity
		/// </summary>
		public MapEntity[] MapEntities => _mapEntities;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 指定レベルのMapMasterを取得する
		/// </summary>
		public MapMaster GetMapMasterByLevel(int level) =>
			// 指定したレベルより高いレベルを持つデータのMapMasterを返す
			_mapEntities.FirstOrDefault(entity => level <= entity.level).mapMaster;

		#endregion


		#region private 関数

		#endregion
	}
}
