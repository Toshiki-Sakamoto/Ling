//
// CreatorWindow.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.18
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Ling.Utility.Editor.CustomScript
{
	public class CreatorEditorWindow<T> : EditorWindow where T : EditorWindow
	{
		private const string UXMLPath = "Assets/App/Scripts/Utility/Editor/CustomScript/CustomScriptCreatorView.uxml";


		// 作成する元のテンプレート名
		private static string _templateScriptName = "";

		// 新しく作成するスクリプト及びクラス名
		[SerializeField] private string _newScriptName = default;

		// スクリプトの説明文
		[SerializeField] private string _scriptSummary = default;

		// 作者名
		private static string _authorName = "";

		// 作成日
		private static string _createdData = "";

		// 汎用的なパラメータ(使い方はテンプレートに任せる)
		[SerializeField] private string _param1 = default;


		/// <summary>
		/// Param1を利用するか
		/// </summary>
		protected virtual bool UseParam1 { get; }

		/// <summary>
		/// Param1のタイトル
		/// </summary>
		protected virtual string Param1Title { get; }



		/// <summary>
		/// スクリプト作成Window生成
		/// </summary>
		/// <param name="templateScriptName">Template script name.</param>
		protected static void ShowWindow(string templateScriptName)
		{
			// 各項目を初期化
			_templateScriptName = templateScriptName;
			_createdData = DateTime.Now.ToString("yyyy.MM.dd");

			// 作者名は既に設定されてある場合は初期化しない
			if (string.IsNullOrEmpty(_authorName))
			{
				// 設定から取得
				_authorName = View.DeveloperSetting.GetName();
			}

			// ウィンドウ作成
			GetWindow<T>("Create Script");
		}

		/// <summary>
		/// 表示Window
		/// </summary>
		private void OnGUI()
		{
			// old 
#if false
            // 作成日と元テンプレートを表示
            EditorGUILayout.LabelField("Template Script Name : " + _templateScriptName);
            GUILayout.Space(0);
            EditorGUILayout.LabelField("Created Data : " + _createdData);
            GUILayout.Space(10);

            // 新しく作成するスクリプト及びクラス名の入力欄
            GUILayout.Label("New Script Name");
            _newScriptName = GUILayout.TextField(_newScriptName);
            GUILayout.Space(10);

            // スクリプトの説明文
            GUILayout.Label("Script Summary");
            _scriptSummary = GUILayout.TextArea(_scriptSummary);
            GUILayout.Space(10);

            // 作者名の入力欄
            GUILayout.Label("Author Name");
            _authorName = GUILayout.TextField(_authorName);
            GUILayout.Space(30);

            // 汎用的なパラメータ
            GUILayout.Label("Param 1");
            _param1 = GUILayout.TextField(_param1);
            GUILayout.Space(30);

            // 作成ボタン、作成が成功したらウィンドウを閉じる
            if (GUILayout.Button("Create"))
            {
                var param = new Creator.Param();
                param.scriptName = _newScriptName;
                param.templateScriptName = _templateScriptName;
                param.createdData = _createdData;
                param.authorName = _authorName;
                param.summary = _scriptSummary;
                param.param1 = _param1;

                if (Creator.CreateScript(param))
                {
                    this.Close();
                }
            }
#endif
		}

		private void OnEnable()
		{
			var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXMLPath);

			// まずはこれを呼び出すことでrootVisualElementとUXMLを結びつける
			asset.CloneTree(this.rootVisualElement);

			var rootElement = rootVisualElement;

			// このクラスのSerializeFieldとVisualElementをバインドする
			rootElement.Bind(new SerializedObject(this));

			// テンプレート名
			rootElement.Q<Label>("TemplateScriptNameValue").text = _templateScriptName;

			// 作成日
			var createdDataLabel = rootElement.Q<Label>("CreatedDataValue");
			createdDataLabel.text = _createdData;

			// 作成者
			var authorNameLabel = rootElement.Q<Label>("AuthorNameValue");
			authorNameLabel.text = _authorName;

			// 各種パラメータ
			var param1ViewElement = rootElement.Q<VisualElement>("Param1VisualElement");
			param1ViewElement.SetEnabled(UseParam1);

			if (UseParam1)
			{
				// Param1を表示状態にする
				var param1 = rootElement.Q<TextField>("Param1");
				param1.label = Param1Title;
			}

			// 作成ボタン
			var createButton = rootElement.Q<Button>("CreateButton");
			createButton.clickable.clicked += () =>
				{
					var param = new Creator.Param();
					param.scriptName = _newScriptName;
					param.templateScriptName = _templateScriptName;
					param.createdData = _createdData;
					param.authorName = _authorName;
					param.summary = _scriptSummary;
					param.param1 = _param1;

					if (Creator.CreateScript(param))
					{
						this.Close();
					}
				};
		}
	}
}
