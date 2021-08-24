// 
// PlayableTransportClip.cs  
// ProductName Ling
//  
// Created by  on 2021.08.20
// 

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Utility.Timeline
{
	/// <summary>
	/// Clip
	/// 変更できるパラメータ定義
	///
	/// 1. Clipの機能についての記述。Clip間でのブレンドが可能かなど
	/// 2. このTrackが利用するBehaviourインスタンスの作成
	/// </summary>
	[System.Serializable]
	public class PlayableTransportClip : PlayableAsset, ITimelineClipAsset
	{
		[SerializeField] private Vector3 _startPos;
		[SerializeField] private Vector3 _endPos;

		public double EndTime { get; set; }


		public ClipCaps clipCaps => ClipCaps.None;

		public void SetParam(in Vector3 startPos, in Vector3 endPos)
		{
			_startPos = startPos;
			_endPos = endPos;
		}

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			var playable = ScriptPlayable<PlayableTransportBehaviour>.Create(graph);
			var clone = playable.GetBehaviour();
			clone.SetParam(_startPos, _endPos);
			clone.EndTime = EndTime;

			return playable;
		}
	}


}