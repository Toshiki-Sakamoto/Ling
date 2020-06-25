// 
// MessageSelect.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.09
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

using Zenject;
using System.Linq;

namespace Ling.Scenes.Battle.Message
{
	/// <summary>
	/// 
	/// </summary>
	public class MessageSelect : MonoBehaviour 
    {
		#region 定数, class, enum

		[System.Serializable]
		public class SelectData
		{
			[SerializeField] private Transform _root = null;
			[SerializeField] private Button[] _buttons = null;

			private List<Text> _texts = new List<Text>();

			public System.Action<int> onSelected;


			public void Setup()
			{
				_texts = _buttons.Select(button_ => button_.GetComponentInChildren<Text>()).ToList();

				for (int i = 0; i < _buttons.Length; ++i)
				{
					var selectedIndex = i;

					_buttons[i].onClick
						.AsObservable()
						.Subscribe(_ =>
							{
								Hide();

								onSelected?.Invoke(selectedIndex);
							});
				}

				Hide();
			}

			public void Show() =>
				_root.gameObject.SetActive(true);

			public void Hide() =>
				_root.gameObject.SetActive(false);

			public void SetText(string[] texts)
			{
				Utility.Log.Assert(texts.Length == _buttons.Length, $"数が一致しない {texts.Length} {_buttons.Length}");

				for (int i = 0; i < texts.Length; ++i)
				{
					_texts[i].text = texts[i];
				}
			}
		}

		#endregion


		#region public 変数

		public System.Action<int> onSelected;

		#endregion


		#region private 変数

		[SerializeField] private SelectData[] _selectData = null;
		[SerializeField] private Image _wall = null;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void Show(string[] selectTexts)
		{
			if (selectTexts.Length > _selectData.Length)
			{
				Utility.Log.Error($"選択肢の数が多い {selectTexts.Length} {_selectData.Length}");
				return;
			}

			_wall.gameObject.SetActive(true);

			var index = selectTexts.Length - 1;

			var selectData = _selectData[index];
			selectData.onSelected = selected_ =>
				{
					Hide();

					onSelected?.Invoke(selected_);
				};

			selectData.SetText(selectTexts);
			selectData.Show();
		}

		public void Hide()
		{
			_wall.gameObject.SetActive(false);

			foreach(var item in _selectData)
			{
				item.Hide();
			}
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		public void Awake()
		{
			foreach(var data in _selectData)
			{
				data.Setup();
			}
		}

		#endregion
	}
}