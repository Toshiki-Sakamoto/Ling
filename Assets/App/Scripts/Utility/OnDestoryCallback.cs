// 
// OnDestoryCallback.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.06.03
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;


namespace Ling.Utility
{
	/// <summary>
	/// オブジェクトが削除された時を検知する
	/// </summary>
	public class OnDestoryCallback : MonoBehaviour 
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private System.Action<GameObject> _onDestory;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public static void AddOnDestoryCallback(GameObject gameObject, System.Action<GameObject> callback)
		{
			var instance = gameObject.GetComponent<OnDestoryCallback>();
			if (instance == null)
			{
				instance = gameObject.AddComponent<OnDestoryCallback>();
				instance.hideFlags = HideFlags.HideAndDontSave;
			}

			instance._onDestory += callback;
		}

		public static bool ExistsOnDestroyCallback(GameObject gameObject) =>
			gameObject.GetComponent<OnDestoryCallback>() != null;

		#endregion


		#region private 関数

		private void OnDestroy()
		{
			_onDestory?.Invoke(gameObject);
		}

		#endregion


		#region MonoBegaviour


		#endregion
	}


	public static class OnDestoryCallbackExtensions
	{
		public static void AddOnDestroyCallback(this GameObject gameObject, System.Action<GameObject> callback) =>
			OnDestoryCallback.AddOnDestoryCallback(gameObject, callback);

		public static bool ExistsOnDesroyCallback(this GameObject gameObject) =>
			OnDestoryCallback.ExistsOnDestroyCallback(gameObject);
	}
}