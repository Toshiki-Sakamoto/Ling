//
// Asset.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.23
//

using System;
using System.Linq;
using UnityEditor;

namespace Ling.Utility.Editor
{
	/// <summary>
	/// Asset操作ヘルパークラス
	/// </summary>
	public class AssetHelper
    {
		/// <summary>
		/// 指定クラスのアセットを検索し、取得する。
		/// 複数あった場合は最初に見つかったもの
		/// </summary>
		public static T LoadAsset<T>() where T : UnityEngine.Object
		{
			var guid = AssetDatabase.FindAssets("t:" + typeof(T).Name).FirstOrDefault();
			if (string.IsNullOrEmpty(guid))
			{
				throw new System.IO.FileNotFoundException($"アセットが見つかりません {typeof(T).Name}");
			}

			var filePath = AssetDatabase.GUIDToAssetPath(guid);
			if (string.IsNullOrEmpty(filePath))
			{
				throw new System.IO.FileNotFoundException($"アセットパスが見つからない {guid}");
			}

			return AssetDatabase.LoadAssetAtPath<T>(filePath);
		}
	}
}
