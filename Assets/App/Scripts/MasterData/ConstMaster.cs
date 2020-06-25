//
// ConstMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.06.24
//

using Cysharp.Threading.Tasks;
using Ling.Common.Attribute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.MasterData
{
	/// <summary>
	/// ゲーム全体の定数
	/// </summary>
	[CreateAssetMenu(menuName = "MasterData/ConstMaster", fileName = "ConstMaster")]
	public class ConstMaster : MasterBase<ConstMaster>
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Header("Map関連")]
		[SerializeField, FieldName("Map間隔")]
		private float _mapDiffHeight = 20f;
		
		[SerializeField, FieldName("プレイヤー階層移動時間")] 
		private float _playerLevelMoveTime = 1.0f;
		
		[SerializeField, FieldName("マップ階層移動時間")] 
		private float _mapLevelMoveTime = 1.2f;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public float MapDiffHeight => _mapDiffHeight;
		public float PlayerLevelMoveTime => _playerLevelMoveTime;
		public float MapLevelMoveTime => _mapLevelMoveTime;


		#endregion


		#region private 関数

		#endregion
	}
}
