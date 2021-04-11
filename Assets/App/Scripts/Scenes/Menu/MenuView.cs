// 
// MenuView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.11.07
// 

using UnityEngine;
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

		#endregion


		#region プロパティ

		public MenuTitleScrollView TitleScroll => _titleScroll;

		#endregion


		#region public, protected 関数

		public void Setup(Param param)
		{
			_titleScroll.Setup(param.CategoryData.Select(data => data.Title), 
				index => 
				{
					// カテゴリを切り替える
					Utility.Log.Print($"Selected Category {index}");
				});
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