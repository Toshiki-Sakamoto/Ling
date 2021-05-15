//
// ItemControl.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.15
//

using UnityEngine;
using Ling.MasterData.Item;

namespace Ling.Map.Item
{
	/// <summary>
	/// アイテムPrefabをコントロールする
	/// </summary>
	public class ItemControl : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private ItemView _view = default;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(ItemMaster itemMaster)
		{
			_view.Setup();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
