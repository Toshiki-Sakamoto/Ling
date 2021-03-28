// 
// EventSystemRemover.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.03.22
// 

using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

namespace Ling.Utility
{
	/// <summary>
	/// 起動時に自分が持っているEventSystemを削除(無効化)する
	/// </summary>
	public class EventSystemRemover : MonoBehaviour 
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
			var scene = gameObject.scene;

			// シーン内にEventSystemがあるか
			var eventSystemObjects = scene.GetRootGameObjects().Where(gameObject => gameObject.GetComponent<EventSystem>() != null);
			foreach (var elm in eventSystemObjects)
			{
				elm.SetActive(false);
			}
		}

		#endregion
	}
}