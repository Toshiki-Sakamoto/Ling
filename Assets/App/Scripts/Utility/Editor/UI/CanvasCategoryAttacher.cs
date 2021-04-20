//
// CanvasGroupAttacher.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.16
//

using UnityEngine;
using UnityEditor;
using Utility.UI;

namespace Utility.Editor.UI
{
	/// <summary>
	/// Canvasが追加された時、CameraAutoChangerスクリプトを自動でアタッチする機能
	/// </summary>

	[InitializeOnLoad]
	public static class CanvasCategoryAttacher
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

		static CanvasCategoryAttacher()
		{
			// Component追加時に自動的に呼び出される
			ObjectFactory.componentWasAdded += component => 
				{
					// 設定がOFFのときは何もしない
					if (!UtilityEditorSettings.EnableCanvasGroupAttach) return;

					// 有効の場合処理
					var canvas = component as Canvas;
					if (canvas == null) return;

					// すでについてるなら何もしない
					if (component.GetComponent<CanvasCategory>() != null) return;

					Utility.Log.Print("Canvasが見つかったのでCanvasCategoryを追加します");

					component.gameObject.AddComponent<CanvasCategory>();
				};
		}

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
