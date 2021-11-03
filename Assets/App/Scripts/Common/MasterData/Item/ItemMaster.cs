//
// ItemMaster.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.21
//

using UnityEngine;
using Utility.Attribute;
using Utility.MasterData;

namespace Ling.MasterData.Item
{
	/// <summary>
	/// アイテムマスタ
	/// </summary>
	/// <remarks>
	/// アイテムに関する共通処理
	/// </remarks>
	public abstract class ItemMaster : MasterDataBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField, FieldName("名前")]
		private string _name = default;

		[SerializeField, FieldName("説明")]
		private string _desc = default;

		[SerializeField, FieldName("PrefabType")]
		private Const.Item.PrefabType _prefabType = default;

		[SerializeField, FieldName("見た目の名前")]
		private string _imageName = default;

		private int _skillId;

		[SerializeField]
		private Skill.SkillMaster _skill = default;

		#endregion


		#region プロパティ

		/// <summary>
		/// アイテム名
		/// </summary>
		public string Name => _name;

		/// <summary>
		/// アイテムカテゴリ
		/// </summary>
		public abstract Const.Item.Category Category { get; }

		/// <summary>
		/// PrefabType
		/// </summary>
		public Const.Item.PrefabType PrefabType => _prefabType;

		/// <summary>
		/// 見た目の名前
		/// </summary>
		public string ImageName => _imageName;

		/// <summary>
		/// スキル
		/// </summary>
		public Skill.SkillMaster Skill => _skill;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
