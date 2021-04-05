//
// DefineCreatorSettings.cs
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

namespace Ling.Common.Editor.DefineCreator
{
	/// <summary>
	/// DefineCreator内の設定
	/// </summary>
	[CreateAssetMenu(menuName = "Ling/DefineCreator/Settings")]
	public class ConstCreatorSettings : ScriptableObject
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		public string saveDirectoryPath;    // 保存先ディレクトリパス

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public static ConstCreatorSettings Load()
		{
			var instance = Utility.Editor.AssetHelper.LoadAsset<ConstCreatorSettings>();
			if (instance == null)
			{
				Utility.Log.Error("指定された保存先にScriptableObjectがありません");
				return null;
			}

			return instance;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
