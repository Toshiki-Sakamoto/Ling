// 
// CameraAutoChanger.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.16
// 

using UnityEngine;

namespace Ling.Utility.UI
{
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

			// Canvasについているカメラを変更する
		}

		#endregion
	}
}