// 
// MenuTitleItem.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.10
// 

using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Ling.Scenes.Menu
{
	/// <summary>
	/// タイトル 一つのアイテム
	/// </summary>
	[RequireComponent(typeof(Toggle))]
	public class MenuTitleItem : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private Toggle _toggle = default;

		#endregion


		#region プロパティ

		public TitleData TitleData { get; private set; }

		/// <summary>
		/// 押された時
		/// </summary>
		public System.Action<TitleData> OnValueChanged { get; set; }

		#endregion


		#region public, protected 関数

		public void SetTitleData(TitleData titleData)
		{
			TitleData = titleData;
			_toggle.isOn = titleData.IsOn;
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
			_toggle = GetComponent<Toggle>();
			_toggle.OnValueChangedAsObservable()
				.Subscribe(isOn => 
				{
					if (TitleData == null) return;
					
					TitleData.IsOn = isOn;

					OnValueChanged?.Invoke(TitleData);

				}).AddTo(this);
		}

		#endregion
	}
}