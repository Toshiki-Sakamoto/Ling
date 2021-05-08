//
// MenuEquipmentView.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.05
//

using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;

namespace Ling.Scenes.Menu.Panel
{
	/// <summary>
	/// 装備
	/// </summary>
	public class MenuEquipmentView : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Text _titleText = default;
		[SerializeField] private Button _button = default;
		[SerializeField] private Text _buttonText = default;
		[SerializeField] private GameObject _activeMark = default;

		private UserData.Equipment.EquipmentUserData _entity;

		#endregion


		#region プロパティ

		public UserData.Equipment.EquipmentUserData Entity => _entity;

		public IObservable<Unit> OnClick => _button.OnClickAsObservable();

		#endregion


		#region public, protected 関数

		public void Setup(UserData.Equipment.EquipmentUserData entity)
		{
			_entity = entity;

			// 名前の設定
			_titleText.text = _entity.Name;

			if (entity.Equipped)
			{
				// 現在装備中のものには「装備」ではなく、「外す」にする
				_buttonText.text = "外す";
			}
			else
			{
				// 「装備」に名前を変える
				_buttonText.text = "装着";
			}

			_activeMark.SetActive(entity.Equipped);
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}
