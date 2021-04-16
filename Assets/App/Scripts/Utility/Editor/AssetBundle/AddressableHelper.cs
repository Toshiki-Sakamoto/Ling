//
// AddressableHelper.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.16
//

using UnityEditor;
using UnityEngine;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Ling.Utility.Editor.AssetBundle
{
	/// <summary>
	/// AddressableのEditor機能ヘルパクラス
	/// </summary>
	public class AddressableHelper
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private static Dictionary<string, AddressableAssetEntry> _assetDict = null;

		#endregion


		#region プロパティ

		public static Dictionary<string, AddressableAssetEntry> AssetDict 
		{
			get
			{
				if (_assetDict == null) 
				{
					CacheAllAssets();
				}

				return _assetDict;
			}
		}

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// アドレスからオブジェクトを取得する
		/// </summary>
		public static TObject LoadAsset<TObject>(string address) where TObject : UnityEngine.Object
		{
			var settings = SettingsHelper.Settings;
			if (AssetDict.TryGetValue(address, out var entry))
			{
            	return AssetHelper.LoadAssetByGUID<TObject>(entry.guid);
			}

			// ない場合は警告出して終了
			Utility.Log.Error($"Assetが見つからない {address}"); 
			return default(TObject);
		}

		#endregion


		#region private 関数


		/// <summary>
		/// EditorではないHelperに対して参照を結びつける
		/// </summary>
		[InitializeOnLoadMethod]
		private static void Initialize()
		{
			Utility.AssetBundle.AddressableHelper.AssetObjectGetter = address => 
				{
					return LoadAsset<UnityEngine.Object>(address);
				};
		}

		internal static void CacheAllAssets()
		{
			var settings = SettingsHelper.Settings;

			var entries = new List<AddressableAssetEntry>();
			settings.GetAllAssets(entries, includeSubObjects: false);

			_assetDict = entries.ToDictionary(entry => entry.address);

			foreach (var entry in entries)
			{
				Debug.Log(entry.address);
			}
		}

		#endregion
	}
}
