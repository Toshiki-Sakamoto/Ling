// 
// ScreenTouchManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.26
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Utility
{
	/// <summary>
	/// 空間にRayを飛ばして衝突したもののタッチ判定を判定
	/// uGUIがタッチされていればそっちを優先する
	/// </summary>
	/// <remark>
	/// https://kan-kikuchi.hatenablog.com/entry/IsPointerOverGameObject
	/// </remark>
	public class ScreenTouchManager : MonoSingleton<ScreenTouchManager>
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
		/// 更新前処理
		/// </summary>
		void Start()
		{
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update()
		{
			// UIを優先
			if (EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}

			Vector2 clickOrTouchPosition;
			if (Input.GetMouseButton(0))
			{
				clickOrTouchPosition = Input.mousePosition;
			}
			else if (Input.touchCount > 0)
			{
				clickOrTouchPosition = Input.GetTouch(0).position;
			}
			else
			{
				return;
			}

			// カメラからRayを飛ばす
			var ray = Camera.main.ScreenPointToRay(clickOrTouchPosition);
			var hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

			if (hit.collider != null)
			{
				// HIT!
				Utility.Log.Print("${hit.colloder.gameObject.name}がクリックされた");
			}
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		void OnDestroy()
		{
		}

		#endregion
	}
}