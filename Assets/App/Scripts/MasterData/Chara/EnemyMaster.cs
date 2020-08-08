//
// EnemyMaster.cs
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

namespace Ling.MasterData.Chara
{
	/// <summary>
	/// 敵情報を持つマスタデータ
	/// </summary>

	[CreateAssetMenu(menuName = "MasterData/EnemyMaster", fileName = "EnemyMaster")]
	public class EnemyMaster : MasterBase<EnemyMaster>
	{
		#region 定数, class, enum


		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("敵の種類")]
		private Const.EnemyType _enemyType = default;

		[SerializeField, FieldName("攻撃AIの種類")]
		private Const.AttackAIType _attackAIType = default;

		[SerializeField, FieldName("移動AIの種類")]
		private Const.MoveAIType _moveAIType = default;

		[SerializeField]
		private StatusData _status = default;

		#endregion


		#region プロパティ

		public Const.EnemyType EnemyType => _enemyType;

		public Const.AttackAIType AttackAIType => _attackAIType;

		public Const.MoveAIType MoveAIType => _moveAIType;

		public StatusData Status => _status;


		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		//		public Entity Find(Define.EnemyType enemyType, int id = 0) =>
		//			_entities.FirstOrDefault(entity => entity.enemyType == enemyType && entity.id == id);

		#endregion


		#region private 関数

		#endregion
	}
}
