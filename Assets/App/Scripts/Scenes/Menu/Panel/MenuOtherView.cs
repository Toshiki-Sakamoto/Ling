//
// MenuOtherView.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.20
//

using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

namespace Ling.Scenes.Menu.Panel
{
	/// <summary>
	/// その他
	/// </summary>
	public class MenuOtherView : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private Button _saveButton = default;

		#endregion


		#region プロパティ

		public Button SaveButton => _saveButton;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup()
		{
		}

		#endregion


		#region private 関数

		#endregion
	}
}
