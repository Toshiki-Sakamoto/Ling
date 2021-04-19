using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEditor;
using System.IO;
using Utility.Extensions;

namespace Utility.Editor.CustomScript
{
	public class Creator
	{
		public class Param
		{
			public string directoryPath;
			public string scriptName;
			public string templateScriptName;
			public string authorName;   // 署名
			public string createdData;  // 作成日
			public string summary = string.Empty;   // 概要

			public bool isOverwriteSave;    // 既存スクリプトを上書き保存する場合true
			public string param1 = string.Empty;    // 汎用パラメータ
			public Dictionary<string, string> replacePairs; // 置き換えテキストKeyValue
		}


		/// <summary>
		/// テンプレートからスクリプト作成
		/// </summary>
		public static bool CreateScript(Param param)
		{
			if (param == null)
			{
				Utility.Log.Error("スクリプトを作成するパラメータがNULLのため作成失敗");
				return false;
			}

			if (string.IsNullOrEmpty(param.directoryPath))
			{
				// 現在作成しているファイルのパスを取得、選択されていない場合はスクリプト失敗
				param.directoryPath = AssetDatabase.GetAssetPath(Selection.activeObject);
				if (string.IsNullOrEmpty(param.directoryPath))
				{
					Debug.Log("作成場所が選択されていないため、スクリプトが作成できませんでした");
					return false;
				}

				// 選択されているファイルに拡張子がある場合(ディレクトリでない場合)は一つ上のディレクトリ内に作成する
				if (!string.IsNullOrEmpty(new FileInfo(param.directoryPath).Extension))
				{
					param.directoryPath = Directory.GetParent(param.directoryPath).FullName;
				}
			}

			// スクリプト名が空欄の場合は作成失敗
			if (string.IsNullOrEmpty(param.scriptName))
			{
				Debug.Log("スクリプト名が入力されていないため、スクリプトが作成できませんでした");
				return false;
			}

			// 署名がない場合はデフォルト
			if (string.IsNullOrEmpty(param.authorName))
			{
				// 設定から取得
				param.authorName = View.DeveloperSetting.GetName();
			}

			// 日付がない場合は現在の日付
			if (string.IsNullOrEmpty(param.createdData))
			{
				param.createdData = DateTime.Now.ToString("yyyy.MM.dd");
			}

			// 同名ファイル名があった場合はスクリプト作成失敗にする(上書きしてしまうため)
			var exportPath = param.directoryPath + "/" + param.scriptName + Const.EXT_SCRIPT;
			if (!param.isOverwriteSave)
			{
				if (File.Exists(exportPath))
				{
					Debug.Log(exportPath + "が既に存在するため、スクリプトが作成できませんでした");
					return false;
				}
			}


			// テンプレートへのパスを作成しテンプレート読み込み
			var templatePath = Const.TEMPLATE_SCRIPT_DIRECTORY_PATH + "/" + param.templateScriptName + Const.EXT_TEMPLATE_SCRIPT;
			var streamReader = new StreamReader(templatePath, System.Text.Encoding.GetEncoding("Shift_JIS"));
			var scriptText = streamReader.ReadToEnd();

			// 各項目を置換
			scriptText = scriptText.Replace(Const.TemplateTag.PRODUCT_NAME, PlayerSettings.productName);
			scriptText = scriptText.Replace(Const.TemplateTag.AUTHOR, param.authorName);
			scriptText = scriptText.Replace(Const.TemplateTag.DATA, param.createdData);
			scriptText = scriptText.Replace(Const.TemplateTag.SUMMARY, param.summary.Replace(Environment.NewLine, Environment.NewLine + "///")); // 改行するとコメントアウトから外れるので修正
			scriptText = scriptText.Replace(Const.TemplateTag.SCRIPT_NAME, param.scriptName);
			scriptText = scriptText.Replace(Const.TemplateTag.PARAM1, param.param1);

			// namespace
			var array = new List<string>(param.directoryPath.Split('/'));

			var pathFindNames = new string[] { "Scripts", "App" };
			int scriptIndex = -1;

			foreach (var pathFindName in pathFindNames)
			{
				scriptIndex = array.LastIndexOf(pathFindName); 
				if (scriptIndex >= 0) break;
			}
	
			if (scriptIndex >= 0)
			{
				array[scriptIndex] = PlayerSettings.productName;
				array.RemoveRange(0, scriptIndex);
			}
			else
			{
				// どれも存在しない場合先頭にProductNameを入れる
				array.Insert(0, PlayerSettings.productName);
			}
			
			// ドットでつなげる
			string pathBelowScripts = string.Join(".", array.ToArray());
			scriptText = scriptText.Replace(Const.TemplateTag.NAMESPACE, pathBelowScripts);

			if (param.replacePairs != null)
			{
				foreach (var pair in param.replacePairs)
				{
					scriptText = scriptText.Replace(pair.Key, pair.Value);
				}
			}

			// スクリプトを書き出し
			File.WriteAllText(exportPath, scriptText, System.Text.Encoding.UTF8);
			AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);

			return true;
		}
	}
}
