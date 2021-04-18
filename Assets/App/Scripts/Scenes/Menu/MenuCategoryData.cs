//
// MenuData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.11
//
using Ling.Common.Scene.Menu;

namespace Ling.Scenes.Menu
{
	/// <summary>
	/// メニューに表示するデータ類
	/// </summary>
	public class MenuCategoryData
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public MenuDefine.Category Category { get; }

		public string Title { get; }

		#endregion


		#region コンストラクタ, デストラクタ

		public MenuCategoryData(MenuDefine.Category category)
		{
			Category = category;
			Title = category.GetCategoryName();
		}

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
