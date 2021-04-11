// 
// MenuTitleScrollView.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.09
// 

using UnityEngine;
using Ling.Utility.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UniRx;

namespace Ling.Scenes.Menu
{
	public class TitleData
	{
		public string Title;
		public int Index;
	}

	/// <summary>
	/// Menuのタイトル部分
	/// </summary>
	public class MenuTitleScrollView : MonoBehaviour,
		RecyclableScrollView.IContentDataProvider
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		[SerializeField] private RecyclableScrollView _scrollView = default;
		[SerializeField] private GameObject _titleObject = default;

		#endregion


		#region private 変数

		private List<TitleData> _titleData = new List<TitleData>();
		private float _titleButtonSize;
		private System.Action<int> _onClicked;

		#endregion


		#region プロパティ

		/// <summary>
		/// データの個数
		/// </summary>
		int RecyclableScrollView.IContentDataProvider.DataCount => _titleData.Count;

		#endregion


		#region public, protected 関数

		public void Setup(IEnumerable<string> titles, System.Action<int> onClicked)
		{
			_onClicked = onClicked;
			_titleData = titles.Select(title => new TitleData { Title = title }).ToList();

			_titleButtonSize = _titleObject.GetComponent<RectTransform>().sizeDelta.x;

			_scrollView.Initialize(this);
		}

		/// <summary>
		/// セルの高さor幅を返す
		/// </summary>
		float RecyclableScrollView.IContentDataProvider.GetItemSize(int index) =>
			_titleButtonSize;

		/// <summary>
		/// セルのGameObjectを返す
		/// </summary>
		GameObject RecyclableScrollView.IContentDataProvider.GetItemObj(int index) =>
			_titleObject;

		/// <summary>
		/// スクロールアイテムの更新を行う
		/// </summary>
		void RecyclableScrollView.IContentDataProvider.ScrollItemUpdate(int index, GameObject obj, bool init)
		{
			var titleData = _titleData[index];
			titleData.Index = index;
			
			var item = obj.GetComponent<MenuTitleItem>();
			item.SetTitleData(titleData);

			if (init)
			{
				item.OnClicked = titleData => 
					{
						_onClicked?.Invoke(titleData.Index);
					};
			}
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