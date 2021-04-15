//
// SettingsHelper.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.15
//

using UnityEditor;
using UnityEngine;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using System.IO;
using System.Collections.Generic;

namespace Ling.Utility.Editor.AssetBundle
{
	/// <summary>
	/// AddressableAssetBundleSettingsのヘルパクラス
	/// </summary>
	public class SettingsHelper : UnityEditor.Editor
	{
		#region 定数, class, enum

		private const string rootPath = "Assets/AssetBundles";

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private static AddressableAssetSettings settings;

		#endregion


		#region プロパティ

		public static AddressableAssetSettings Settings 
		{
			get 
			{
				if (settings != null) return settings;

				settings = AssetHelper.LoadAsset<AddressableAssetSettings>();
				return settings;
			}
		}

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 指定したディレクトリ配下をフォルダごとにGroup化する
		/// </summary>
		[MenuItem("Tools/AssetBundle/RegistAssetGroups")]
		public static void RegistAssetGroups()
		{
			var setting = Settings;
			RegistAssetGroupInternal(rootPath);
		}

		#endregion


		#region private 関数

		private static void RegistAssetGroupInternal(string folderPath)
		{
			var settings = Settings;

			// すべてのファイルを取得
			var guids = AssetDatabase.FindAssets("", new [] { folderPath });

			foreach (var guid in guids)
			{
				var path = AssetDatabase.GUIDToAssetPath(guid);
				if (Directory.Exists(path)) continue;

				var localPath = Path.GetDirectoryName(path)
					.Replace(rootPath + "/", "");

				// フォルダを分解する
				var folders = localPath.Split('/');
				
				// 1番目をGroupとする
				if (folders.Length > 0)
				{
					var groupName = folders[0];

					// Addressableに登録
					// グループの取得
					var group = GetOrCreateGroup(groupName);

					// 2番目以降は登録
			//		var entry = settings.CreateOrMoveEntry(guid, group);
				}


				var address = Path.GetFileNameWithoutExtension(path);

			//	var entry = settings.CreateOrMoveEntry(guid, group);

				// 重複チェックするか
//				var entries = new List<AddressableAssetEntry>();

		//		entry.SetAddress(address);
			}
		}

		/// <summary>
		/// グループを作成する
		/// </summary>
		private static AddressableAssetGroup GetOrCreateGroup(string groupName)
		{
			var settings = Settings;

			// すでにあれば何もしない
			var group = settings.groups.Find(group => group.name == groupName);
			if (group != null)
			{
				return group;
			}

			var schema = new List<AddressableAssetGroupSchema>() 
				{
					BundledAssetGroupSchema.CreateInstance<BundledAssetGroupSchema>(),
					ContentUpdateGroupSchema.CreateInstance<ContentUpdateGroupSchema>()
				};

			return settings.CreateGroup(groupName, setAsDefaultGroup: false, readOnly: false, postEvent: true, schema);
		}

		#endregion
	}
}
