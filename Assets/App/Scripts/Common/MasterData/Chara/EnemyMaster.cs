//
// EnemyMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.05
//

using UnityEngine;
using Utility.MasterData;
using Utility.Attribute;

namespace Ling.MasterData.Chara
{
	/// <summary>
	/// 敵情報を持つマスタデータ
	/// </summary>

	[CreateAssetMenu(menuName = "MasterData/EnemyMaster", fileName = "EnemyMaster")]
	public class EnemyMaster : MasterDataBase
	{
		#region 定数, class, enum


		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("名前")]
		private string _name = default;

		[SerializeField, FieldName("敵の種類")]
		private Const.EnemyType _enemyType = default;

		[SerializeField]
		private AttackAIData _attackAIData = default;

		[SerializeField]
		private MoveAIData _moveAIData = default;

		[SerializeField]
		private StatusData _status = default;

		#endregion


		#region プロパティ

		public string Name => _name;

		public Const.EnemyType EnemyType => _enemyType;

		public AttackAIData AttackAIData => _attackAIData;

		public MoveAIData MoveAIData => _moveAIData;

		public StatusData Status => _status;


		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数


		#endregion


		#region private 関数

		#endregion
	}
}
