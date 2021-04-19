// 
// MonoSingleton.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.22.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Utility
{
	/// <summary>
	/// 
	/// </summary>
	public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private static T _instance = null;

		#endregion


		#region プロパティ

		public static T Instance
		{
			get
			{
				return _instance;
			}
		}

		public static bool IsNull { get { return _instance == null; } }

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		protected virtual void Awake()
		{
			if (_instance != null)
			{
				if (this != _instance)
				{
					Destroy(_instance.gameObject);
				}
				else
				{
					return;
				}
			}

			_instance = this.GetComponent<T>();
		}

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
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		protected virtual void OnDestroy()
		{
			_instance = null;
		}

		#endregion
	}
}