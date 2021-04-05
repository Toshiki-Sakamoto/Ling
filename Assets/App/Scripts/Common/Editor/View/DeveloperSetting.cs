//
// DeveloperEdit.cs
// ProductName Ling
//
// Created by toshiki sakmoto on 2020.04.30
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Ling.Editor.View
{
#if false
	public class DeveloperSettingScriptableObject : ScriptableObject
	{
		public const string Path = "Assets/App/Scripts/Editor/View/DeveloperSetting.asset";

		public string name = null;


		public static DeveloperSettingScriptableObject GetOrCreate()
		{
			var settings = AssetDatabase.LoadAssetAtPath<DeveloperSettingScriptableObject>(Path);

			// すでにある場合はそれを返す
			if (settings != null) return settings;

			settings = CreateInstance<DeveloperSettingScriptableObject>();
			AssetDatabase.CreateAsset(settings, Path);
			AssetDatabase.SaveAssets();

			return settings;
		}

		/// <summary>
		/// SerializedObjectに変換する
		/// </summary>
		/// <returns></returns>
		public static SerializedObject GetSerializedObject() =>
			new SerializedObject(GetOrCreate());
	}
#endif

	/// <summary>
	/// Edit > Project Settings から開発者情報を記録できる
	/// </summary>
	public class DeveloperSetting : SettingsProvider
	{
		#region 定数, class, enum

		private const string NameKey = "DeveloperName";

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		//private SerializedObject _serializedObject;

		private static string _developerName;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		public DeveloperSetting(string path, SettingsScope scope)
			: base(path, scope) { }

		#endregion


		#region public, protected 関数

		public override void OnActivate(string searchContext, VisualElement rootElement)
		{
			// 設定が保存されているアセットを取得
			//_serializedObject = DeveloperSettingScriptableObject.GetSerializedObject();

			_developerName = GetName();
		}

		public override void OnGUI(string searchContext)
		{
			//EditorGUILayout.PropertyField(_serializedObject.FindProperty(nameof(DeveloperSettingScriptableObject.name)));

			//_serializedObject.ApplyModifiedProperties();

			//内容の変更チェック開始
			EditorGUI.BeginChangeCheck();

			//開発者名
			GUILayout.Label("Name", EditorStyles.boldLabel);

			_developerName = GUILayout.TextField(_developerName);

			//内容が変更されていれば保存
			if (EditorGUI.EndChangeCheck())
			{
				SetName(_developerName);
			}
		}

		public static void SetName(string name) => EditorPrefs.SetString(NameKey, name);
		public static string GetName() => EditorPrefs.GetString(NameKey, "");

		#endregion


		#region private 関数

		[SettingsProvider]
		private static SettingsProvider Create()
		{
			var path = "Project/Developer Setting";
			var provider = new DeveloperSetting(path, SettingsScope.Project);
			//var settings = DeveloperSettingScriptableObject.GetSerializedObject();

			// 検索ワードの設定
			// GetXXX 関数を使用することで検索ワードを自動で設定してくれる
			//provider.keywords = GetSearchKeywordsFromSerializedObject(settings);

			return provider;
		}

		#endregion
	}
}
