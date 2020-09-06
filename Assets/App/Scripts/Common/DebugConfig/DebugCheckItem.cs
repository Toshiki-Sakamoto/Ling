// 
// ItemCheck.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.23
// 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

using Zenject;

#if DEBUG
namespace Ling.Common.DebugConfig
{
	/// <summary>
	/// チェックボックス
	/// </summary>
	public class DebugCheckItem : MonoBehaviour 
    {
		#region 定数, class, enum

		public class Data : DebugItemDataBase<DebugCheckItem>
		{
			public bool IsOn { get; private set; }

			public Data(string title)
				: base(title)
			{ }

			public void SetValue(bool isOn)
			{
				IsOn = isOn;
			}

			public override Const.MenuType GetMenuType() =>
				Const.MenuType.Check;

			protected override void DataUpdateInternal(DebugCheckItem obj)
			{
				obj.SetData(this);
			}
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Text _txtTitle = null;
		[SerializeField] private Toggle _toggle = null;

		private Data _data;

		#endregion


		#region プロパティ


//		public Action<bool> onUpdate { get; set; }

		#endregion


		#region public, protected 関数

		public static Data Create(bool isOn, string title)
		{
			var data = new Data(title);
			data.SetValue(isOn);

			return data;
		}


		public void SetData(Data data)
		{
			_txtTitle.text = data.Title;
			_toggle.isOn = data.IsOn;

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
			_toggle
				.OnValueChangedAsObservable()
				.Subscribe((Action<bool>)(isOn_ => 
				{
					if (this._data == null) return;

					this._data.SetValue(isOn_);
				//	onUpdate?.Invoke(Data.IsOn.Value);
				}));
		}

		#endregion
	}
}
#endif