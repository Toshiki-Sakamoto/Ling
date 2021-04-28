//
// StatusView.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.11.02
//

using UnityEngine;
using UnityEngine.UI;

namespace Ling.Scenes.Status
{
	/// <summary>
	/// Status View
	/// </summary>
	public class StatusView : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private HPView _hpView = default;
		[SerializeField] private StaminaView _staminaView = default;

		#endregion


		#region プロパティ

		public HPView HP => _hpView;
		public StaminaView Stamina => _staminaView;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
