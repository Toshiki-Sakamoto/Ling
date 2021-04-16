//
// CanvasGroupAttacher.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.16
//

using UnityEngine;
using UnityEditor;

namespace Ling.Utility.Editor.UI
{
	/// <summary>
	/// Canvasが追加された時、CameraAutoChangerスクリプトを自動でアタッチする機能
	/// </summary>

	[InitializeOnLoad]
	public static class CanvasGroupAttacher
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

		static CanvasGroupAttacher()
		{
			// Component追加時に自動的に呼び出される
			ObjectFactory.componentWasAdded += component => 
				{
					// 設定がOFFのときは何もしない
					if (!UtilityEditorSettings.EnableCanvasGroupAttach) return;

					// 有効の場合処理
					var canvas = component.GetComponent<Canvas>();
					if (canvas == null) return;

					Utility.Log.Print("Canvasが見つかったのでCanvasGroupControllerを追加します");

					component.gameObject.AddComponent<Utility.UI.CanvasGroup>();
				};
		}

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion
	}
}
