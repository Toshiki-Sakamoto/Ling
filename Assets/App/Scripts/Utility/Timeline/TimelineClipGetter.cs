//
// TimelineClipGetter.cs
// ProductName Ling
//
// Created by  on 2021.08.20
//

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Linq;
using System;

namespace Utility.Timeline
{
	/// <summary>
	/// タイムラインのクリップ情報を保持、取得する
	/// </summary>
    [RequireComponent(typeof(PlayableDirector))]
	public class TimelineClipGetter : MonoBehaviour
	{
		#region 定数, class, enum

		public class TrackContainer
		{
			public Dictionary<string, TrackData> TrackDict;

			public TrackData Find(string name)
			{
				if (TrackDict.TryGetValue(name, out var result))
				{
					return result;
				}

				return null; // 例外出す
			}

			public TTrack FindTrack<TTrack>(string name) where TTrack : TrackAsset =>
				Find(name)?.Track as TTrack;

			public void Add(string name, TrackData track) =>
				TrackDict.Add(name, track);
		}

		public class TrackData
		{
			public TrackAsset Track;
			public Dictionary<string, TimelineClip> ClipDict = new Dictionary<string, TimelineClip>();
			public List<TimelineClip> Clips;

			public TrackData(TrackAsset track)
			{
				Track = track;

				var clips = Track.GetClips();

				// 名前で辞書を作る
				ClipDict = clips.ToDictionary(clip => clip.displayName);
				Clips = clips.ToList();
			}

			public TimelineClip GetClip(int index) => Clips[index];
			public T GetClip<T>(int index) where T : TimelineClip => GetClip(index) as T;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private PlayableDirector _director;
		private IEnumerable<TrackAsset> _allTracks;
		private Dictionary<Type, List<TrackData>> _tracks;
		private Dictionary<Type, List<PlayableAsset>> _clips = new Dictionary<Type, List<PlayableAsset>>();

		#endregion


		#region プロパティ

		private IEnumerable<TrackAsset> AllTracks => _allTracks ??= ((TimelineAsset)(_director.playableAsset)).GetOutputTracks();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
        /// 指定したクラスのトラックのリストを取得する
        /// </summary>
		public List<TrackData> GetTracks<T>() where T : TrackAsset
		{
			var type = typeof(T);
			if (_tracks.TryGetValue(type, out var result))
			{
				return result;
			}

			result = new List<TrackData>();
			_tracks.Add(type, result);

			foreach (var elm in AllTracks.OfType<T>())
			{
				result.Add(new TrackData(elm));
			}

			return result;
		}

		/// <summary>
        /// 最初のトラックデータを取得する
        /// </summary>
		public TrackData GetTrack<T>() where T : TrackAsset =>
			GetTracks<T>().FirstOrDefault();

		public T GetTrackAsset<T>() where T : TrackAsset =>
			GetTrack<T>().Track as T;

#if false
		/// <summary>
		/// 指定したトラックのクリップのリストを取得
		/// </summary>
		public List<T> GetClips<T>() where T : PlayableAsset
		{
			if (_clips.TryGetValue(typeof(T), out var list))
			{
				return list as List<T>;
			}

			if (AllTracks.Count() <= 0) return null;

			Tracks
		}
#endif

		#endregion


		#region private 関数

		private void CreateTrackCachesIfNeeded()
		{

		}

		private void Awake()
		{
			_director = GetComponent<PlayableDirector>();
		}

		#endregion
	}
}
