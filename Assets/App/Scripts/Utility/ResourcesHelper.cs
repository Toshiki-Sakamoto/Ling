//
// ResourcesHelper.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.14
//

using UnityEngine;

namespace Ling.Utility
{
	/// <summary>
	/// Utilityフォルダ関連のResourcesに関するヘルパークラス
	/// </summary>
	public class ResourcesHelper
	{
		#region 定数, class, enum

		// Utility関連のデフォルトルートパス
		public const string RootPath = "Utility/";

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public static TAsset Load<TAsset>(string path) where TAsset : Object
		{
			return Resources.Load<TAsset>(RootPath + path);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
