//
// PrefsRemover.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.30
//

using UnityEngine;
using UnityEditor;

namespace Utility.Editor
{
	/// <summary>
	/// Prefsの削除
	/// </summary>
	public class PrefsRemover : EditorWindow
	{
		#region 定数, class, enum

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

		[MenuItem("Tools/PrefsRemover/Editor Remove All")]
		public static void EditorDeleteAll()
		{
			EditorPrefs.DeleteAll();
		}
		
		#endregion


		#region private 関数


		#endregion
	}
}
