using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEditor;
using System.IO;

namespace Ling.Common.Editor.CustomScript
{
    public class Creator : EditorWindow
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
//                _authorName = Environment.UserName;
            }

            // ウィンドウ作成
            GetWindow<Creator>("Create Script");
        }

        /// <summary>
        /// テンプレートからスクリプト作成
        /// </summary>
        /// <returns><c>true</c>, if script was created, <c>false</c> otherwise.</returns>
        private static bool CreateScript()
        {
            // スクリプト名が空欄の場合は作成失敗
            if (string.IsNullOrEmpty(_newScriptName))
            {
                Debug.Log("スクリプト名が入力されていないため、スクリプトが作成できませんでした");
                return false;
            }

            // 現在作成しているファイルのパスを取得、選択されていない場合はスクリプト失敗
            var directoryPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(directoryPath))
            {
                Debug.Log("作成場所が選択されていないため、スクリプトが作成できませんでした");
                return false;
            }

            // 選択されているファイルに拡張子がある場合(ディレクトリでない場合)は一つ上のディレクトリ内に作成する
            if (!string.IsNullOrEmpty(new FileInfo(directoryPath).Extension))
            {
                directoryPath = Directory.GetParent(directoryPath).FullName;
            }

            // 同名ファイル名があった場合はスクリプト作成失敗にする(上書きしてしまうため)
            var exportPath = directoryPath + "/" + _newScriptName + Const.EXT_SCRIPT;
            if (File.Exists(exportPath))
            {
                Debug.Log(exportPath + "が既に存在するため、スクリプトが作成できませんでした");
                return false;
            }

            // テンプレートへのパスを作成しテンプレート読み込み
            var templatePath = Const.TEMPLATE_SCRIPT_DIRECTORY_PATH + "/" + _templateScriptName + Const.EXT_TEMPLATE_SCRIPT;
            var streamReader = new StreamReader(templatePath, System.Text.Encoding.GetEncoding("Shift_JIS"));
            var scriptText = streamReader.ReadToEnd();

            // 各項目を置換
            scriptText = scriptText.Replace(Const.TemplateTag.PRODUCT_NAME, PlayerSettings.productName);
            scriptText = scriptText.Replace(Const.TemplateTag.AUTHOR, _authorName);
            scriptText = scriptText.Replace(Const.TemplateTag.DATA, _createdData);
            scriptText = scriptText.Replace(Const.TemplateTag.SUMMARY, _scriptSummary.Replace(Environment.NewLine, Environment.NewLine + "///")); // 改行するとコメントアウトから外れるので修正
            scriptText = scriptText.Replace(Const.TemplateTag.SCRIPT_NAME, _newScriptName);
			scriptText = scriptText.Replace(Const.TemplateTag.PARAM1, _param1);

            // namespace
            var array = new List<string>(directoryPath.Split('/'));
            int scriptIndex = array.LastIndexOf("Scripts");
            if (scriptIndex > 0)
            {
                array[scriptIndex] = PlayerSettings.productName;
                array.RemoveRange(0, scriptIndex);

                // ドットでつなげる
                string pathBelowScripts = string.Join(".", array.ToArray());
                scriptText = scriptText.Replace(Const.TemplateTag.NAMESPACE, pathBelowScripts);
            }


            // スクリプトを書き出し
            File.WriteAllText(exportPath, scriptText, System.Text.Encoding.UTF8);
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);

            return true;
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
                if (CreateScript())
                {
                    this.Close();
                }
            }
        }
    }
}
