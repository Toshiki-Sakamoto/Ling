// 
// TimelinePlayer.cs  
// ProductName Ling
//  
// Created by  on 2021.08.22
// 

using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Playables;

namespace Utility.Timeline
{
	/// <summary>
	/// タイムラインを再生するために必要なものを持つクラス
    /// 汎用的に作る
	/// </summary>
    [RequireComponent(typeof(CustomTimelineCollections))]
	public class TimelinePlayer : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private PlayableDirector _skillTimeline;

		private CustomTimelineCollections _timelineCollections;

		#endregion


		#region プロパティ

		public PlayableDirector PlayableDirector => _skillTimeline;

		#endregion


		#region public, protected 関数

		public void AddCustom<T>(T instance) where T : ICustomTimeline =>
			_timelineCollections.AddCustom<T>(instance);

		public T GetCustom<T>() where T : ICustomTimeline =>
			_timelineCollections.GetCustom<T>();

		public void Play()
		{
			_skillTimeline.Play();
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			_timelineCollections = GetComponent<CustomTimelineCollections>();

			if (_skillTimeline == null)
			{
				_skillTimeline = GetComponent<PlayableDirector>();
			}
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