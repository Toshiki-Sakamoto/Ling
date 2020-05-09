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
			[SerializeField] private Button[] _button = null;
			[SerializeField] private Text[] _text = null;

			public System.Action<int> onSelected;


			public void Setup()
			{
				for (int i = 0; i < _button.Length; ++i)
				{
					_button[i].onClick
						.AsObservable()
						.Subscribe(_ =>
							{
								Hide();

								onSelected?.Invoke(i);
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
				Utility.Log.Assert(texts.Length == _text.Length, $"数が一致しない {texts.Length} {_text.Length}");

				for (int i = 0; i < texts.Length; ++i)
				{
					_text[i].text = texts[i];
				}
			}
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private SelectData[] _selectData = null;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void Show(string[] text)
		{
			if (text.Length > _selectData.Length)
			{
				Utility.Log.Error($"選択肢の数が多い {text.Length} {_selectData.Length}");
				return;
			}

			var index = text.Length - 1;

			_selectData[index].Show();
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