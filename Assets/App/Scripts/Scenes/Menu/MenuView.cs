// 
// MenuView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.11.07
// 

using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Collections.Generic;
using System.Linq;

namespace Ling.Scenes.Menu
{
	/// <summary>
	/// Menu View
	/// </summary>
	public class MenuView : MonoBehaviour
	{
		#region 定数, class, enum

		public class Param
		{
			public List<MenuCategoryData> CategoryData;
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private MenuTitleScrollView _titleScroll = default;
		[SerializeField] private Button _closeButton = default;

		#endregion


		#region プロパティ

		public MenuTitleScrollView TitleScroll => _titleScroll;

		/// <summary>
		/// 選択されたIndex値
		/// </summary>
		public IntReactiveProperty SelectedIndex { get; } = new IntReactiveProperty();

		/// <summary>
		/// 閉じるボタン
		/// </summary>
		public Button CloseButton => _closeButton;

		#endregion


		#region public, protected 関数

		public void Setup(Param param)
		{
			_titleScroll.Setup(param.CategoryData.Select(data => data.Title), 
				index => 
				{
					// カテゴリを切り替える
					Utility.Log.Print($"Selected Category {index}");

					SelectedIndex.Value = index;
				});

			// 1番目を選択状態にする
			_titleScroll.SelectCategoryByIndex(0);
		}

		/// <summary>
		/// 表示するCategoryDataを設定
		/// </summary>
		public void SetCategoryData(MenuCategoryData categoryData)
		{

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
		}

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update()
		{
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		void OnDestroy()
		{
		}

		#endregion
	}
}