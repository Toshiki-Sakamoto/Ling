// 
// CameraAutoChanger.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.14
// 

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ling.Common.Scene
{
#if UNITY_EDITOR
	/// <summary>
	/// Canvas追加時にCameraAutoChangerを自動的にアタッチする
	/// </summary>
	[InitializeOnLoad]
	public static class CameraAutoChangeAttacher
	{
		static CameraAutoChangeAttacher()
		{
			// Component追加時に自動的に呼び出される
			ObjectFactory.componentWasAdded += component => 
				{
					// 有効の場合処理
					var canvas = component.GetComponent<Canvas>();
					if (canvas == null) return;

					Utility.Log.Print("Canvasが見つかったのでCameraAutoChangerを追加します");

					component.gameObject.AddComponent<CameraAutoChanger>();
				};
		}
	}
#endif

	/// <summary>
	/// 自動でカメラを切り替えるスクリプト
	/// </summary>
	[RequireComponent(typeof(Canvas))]
	public class CameraAutoChanger : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			var canvas = GetComponent<Canvas>();
		}

		#endregion
	}
}