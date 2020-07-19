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
using UnityEngine.UI;
using UnityEditor;

namespace Ling.Common.Editor.CustomScript
{
    public class CreatorEditorWindow : EditorWindow
    {
        // 作成する元のテンプレート名
        private static string _templateScriptName = "";
        // 新しく作成するスクリプト及びクラス名
        private static string _newScriptName = "";
        // スクリプトの説明文
        private static string _scriptSummary = "";
        // 作者名
        private static string _authorName = "";
        // 作成日
        private static string _createdData = "";
        // 汎用的なパラメータ(使い方はテンプレートに任せる)
        private static string _param1 = "";


        /// <summary>
        /// スクリプト作成Window生成
        /// </summary>
        /// <param name="templateScriptName">Template script name.</param>
        protected static void ShowWindow(string templateScriptName)
        {
            // 各項目を初期化
            _templateScriptName = templateScriptName;
            _newScriptName = string.Empty;// templateScriptName;
            _createdData = DateTime.Now.ToString("yyyy.MM.dd");

            // 作者名は既に設定されてある場合は初期化しない
            if (string.IsNullOrEmpty(_authorName))
            {
                // 設定から取得
                _authorName = Ling.Editor.View.DeveloperSetting.GetName();
            }

            // ウィンドウ作成
            GetWindow<CreatorEditorWindow>("Create Script");
        }

        /// <summary>
        /// 表示Window
        /// </summary>
        private void OnGUI()
        {
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
        }
    }
}
