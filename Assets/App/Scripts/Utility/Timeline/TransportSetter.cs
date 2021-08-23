// 
// TransportSetter.cs  
// ProductName Ling
//  
// Created by  on 2021.08.21
// 

using UnityEngine;
using UnityEngine.Playables;

namespace Utility.Timeline
{
	/// <summary>
	/// 
	/// </summary>
	[RequireComponent(typeof(TimelineClipGetter))]
	public class TransportSetter : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private TimelineClipGetter _clipGetter;

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
			_clipGetter = GetComponent<TimelineClipGetter>();
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