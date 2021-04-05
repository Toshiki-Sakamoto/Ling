// 
// ItemSlider.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.23
// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

#if DEBUG
namespace Ling.Common.DebugConfig
{
	/// <summary>
	/// Slider
	/// </summary>
	public class DebugSliderItem : MonoBehaviour
	{
		#region 定数, class, enum

		public class Data : DebugItemDataBase<DebugSliderItem>
		{
			public float Value { get; private set; }
			public float MinValue { get; private set; }
			public float MaxValue { get; private set; }

			public string Format { get; private set; }


			public Data(string title, float minValue, float maxValue, string format)
				: base(title)
			{
				MinValue = minValue;
				MaxValue = maxValue;
				Format = format;
			}

			public void SetValue(float? value)
			{
				var strValue = string.Format(Format, value);

				float result = 0f;
				if (float.TryParse(strValue, out result))
				{
					Value = result;
				}
			}

			public override Const.MenuType GetMenuType() =>
				Const.MenuType.Slider;

			protected override void DataUpdateInternal(DebugSliderItem obj)
			{
				obj.SetData(this);
			}
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Text _txtTitle = null;
		[SerializeField] private Text _txtValue = null;
		[SerializeField] private Slider _slider = null;

		private Data _data = null;  // 現在のデータ

		#endregion


		#region プロパティ

		public Action<float> OnUpdate { get; set; }

		#endregion


		#region public, protected 関数

		public static Data Create(string title, float min, float max, string format = "{0:f2}")
		{
			var data = new Data(title, min, max, format);
			data.SetValue(min);

			return data;
		}

		public void SetData(Data data)
		{
			_txtTitle.text = data.Title;
			_txtValue.text = data.Value.ToString();
			_slider.value = data.Value;
			_slider.minValue = data.MinValue;
			_slider.maxValue = data.MaxValue;

			_data = data;
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			_slider.onValueChanged.AddListener(value_ =>
				{
					if (_data == null) return;

					_data.SetValue(value_);
					_txtValue.text = _data.Value.ToString();

					OnUpdate?.Invoke(_data.Value);
				});
		}

		#endregion
	}
}
#endif