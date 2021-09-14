// 
// MiniMapPointObject.cs  
// ProductName Ling
//  
// Created by  on 2021.09.13
// 

using UnityEngine;
using Utility;

namespace Ling.Map
{
	/// <summary>
	/// ミニマップ上のオブジェクト
	/// </summary>
	public class MiniMapPointObject : MonoBehaviour 
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

		public void SetFollowObject(GameObject follow)
		{
			_follower.SetFolow(follow.transform);
		}

		#endregion


		#region private 関数

		private MiniMapObjectFollower _follower;

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			_follower = gameObject.GetOrAddComponent<MiniMapObjectFollower>();
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
		void OnDestroy()
		{
		}

		#endregion
	}
}