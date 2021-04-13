//
// DefineScriptCreator.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.19
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Utility.Editor.DefineCreator
{
	/// <summary>
	/// 定数スクリプトファイルを作成するEditor機能
	/// </summary>
	public class ConstScriptCreator
	{
		#region 定数, class, enum

		public class Param<T>
		{
			public Dictionary<string, T> constPairs;        // 定数
			public bool needsInsertComments;                // コメントを挿入する場合
			public Dictionary<string, string> commentPairs; // コメント


			public Param()
			{
				constPairs = new Dictionary<string, T>();
				commentPairs = new Dictionary<string, string>();
			}

			public string FindByComment(string key)
			{
				if (!commentPairs.TryGetValue(key, out var result))
				{
					return string.Empty;
				}

				return result;
			}
		}

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

		public static void Create<T>(string scriptName, string summary, Param<T> param)
		{
			var typeCode = Type.GetTypeCode(typeof(T));
			var stringBuilder = new StringBuilder();
			var constPairs = param.constPairs;

			// 最大の文字数
			var keyStrLengthMax = constPairs.Keys.Select(key => key.Length).Max();

			bool isFirst = true;

			foreach (var pair in constPairs)
			{
				var typeStr = string.Empty;
				var valueStr = string.Empty;

				switch (typeCode)
				{
					case TypeCode.Int32:
						typeStr = "int";
						valueStr = pair.Value.ToString();
						break;

					case TypeCode.Int64:
						typeStr = "long";
						valueStr = pair.Value.ToString();
						break;

					case TypeCode.Double:
						typeStr = "double";
						valueStr = pair.Value.ToString();
						break;

					case TypeCode.String:
						typeStr = "string";
						valueStr = $"\"{pair.Value}\""; // " " で文字列を囲む
						break;

					default:
						Utility.Log.Error($"型が不明です。作成に失敗しました {typeof(T)}");
						return;
				}

				if (!isFirst)
				{
					// 最初の挿入でない場合は改行(コメントありの場合は２つ)入れる
					if (param.needsInsertComments)
					{
						stringBuilder.AppendLine();
					}

					stringBuilder.AppendLine();
				}
				else
				{
					isFirst = false;
				}

				// コメントを入れる
				if (param.needsInsertComments)
				{
					var comment = param.FindByComment(pair.Key);

					stringBuilder.AppendLine($"\t\t/// <summary>");
					stringBuilder.AppendLine($"\t\t/// {comment}");
					stringBuilder.AppendLine($"\t\t/// </summary>");
				}

				stringBuilder.Append($"\t\tpublic const {typeStr} {pair.Key} = {valueStr};");
			}

			var creatorParam = new CustomScript.Creator.Param();
			creatorParam.replacePairs = new Dictionary<string, string>();
			creatorParam.replacePairs["#PARAM#"] = stringBuilder.ToString();

			// 保存場所をScriptableObjectから取得する
			var defineCreatorSettings = ConstCreatorSettings.Load();
			if (defineCreatorSettings == null) return;

			// 保存ファイル名
			creatorParam.scriptName = scriptName;

			// テンプレートファイル名
			creatorParam.templateScriptName = "ConstClass";

			// 概要
			creatorParam.summary = summary;

			// 上書きを許可する
			creatorParam.isOverwriteSave = true;

			creatorParam.directoryPath = defineCreatorSettings.saveDirectoryPath;

			// あとはデフォルトでスクリプトを作成する
			CustomScript.Creator.CreateScript(creatorParam);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
