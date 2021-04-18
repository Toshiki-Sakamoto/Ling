//
// MenuArgument.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.18
//

namespace Ling.Common.Scene.Menu
{
	/// <summary>
	/// Menuシーンに渡すデータ類
	/// </summary>
	public class MenuArgument : Common.Scene.Argument
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public MenuDefine.Group Group { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 通常メニュー
		/// </summary>
		public static MenuArgument CreateAtMenu()
		{
			return new MenuArgument { Group = MenuDefine.Group.Menu };
		}

		/// <summary>
		/// ショップ
		/// </summary>
		public static MenuArgument CreateAtShop()
		{
			return new MenuArgument { Group = MenuDefine.Group.Shop };
		}

		#endregion


		#region private 関数

		#endregion
	}
}
