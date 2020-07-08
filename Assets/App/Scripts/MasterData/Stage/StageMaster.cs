//
// StageMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.05
//

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
	public class StageMaster : MasterBase<StageMaster>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private MapMaster[] _mapMasters = default; // Stage内にある１Mapの情報を持つ

		#endregion


		#region プロパティ

		/// <summary>
		/// Map数(最大階層数)
		/// </summary>
		public int MapCount => _mapMasters.Length;

		/// <summary>
		/// MapMaster配列
		/// </summary>
		public MapMaster[] MapMasters => _mapMasters;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 指定したIndexのMapMasterを取得する
		/// </summary>
		public MapMaster GetMapMaster(int index)
		{
			if (index >= MapCount)
			{
				Utility.Log.Error($"マップの最大数を超えたIndexを指定している {index}");
				return null;
			}

			return MapMasters[index];
		}

		#endregion


		#region private 関数

		#endregion
	}
}
