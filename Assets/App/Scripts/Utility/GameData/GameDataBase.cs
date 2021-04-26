//
// TGameDataBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.22
//

using UnityEngine;

namespace Utility.GameData
{
	/// <summary>
	/// User/Master Dataのベースクラス
	/// </summary>
	public class GameDataBase : ScriptableObject
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private int _id = default; // 大体存在する一意なID

		#endregion


		#region プロパティ

		public int ID => _id;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public virtual void Setup() { }

		#endregion


		#region private 関数

		#endregion
	}
}
