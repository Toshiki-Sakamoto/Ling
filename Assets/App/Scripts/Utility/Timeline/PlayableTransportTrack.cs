//
// PlayableTransportTrack.cs
// ProductName Ling
//
// Created by  on 2021.08.18
//

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Utility.Timeline
{
	/// <summary>
	/// 座標を指定して移動させるトラック
    /// タイムラインのトラックを表現する。トラック：シーン中のオブジェクトをどのように操作するか定義したもの
	/// </summary>
    [TrackColor(1f, 0f, 0.02f)]
	[TrackClipType(typeof(PlayableTransportClip))]
	[TrackBindingType(typeof(Transform))]	// 何を操作するか
	public class PlayableTransportTrack : TrackAsset
	{
		public PlayableTransportMixerBehavior MixerBehaviour { get; private set; }

		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
			// 対応するMixerBehaviourインスタンスを作成
			var instance = ScriptPlayable<PlayableTransportMixerBehavior>.Create(graph, inputCount);
			MixerBehaviour =  instance.GetBehaviour();

			return instance;
        }
    }


	/// <summary>
    /// パラメータ変更処理
    ///
    /// クリップごとの振る舞いを実装する。Mixerで実装するのが大半なのでクリップごとのパラメータ保持に留めるのがポピュラー
    /// </summary>
	[System.Serializable]
	public class PlayableTransportBehaviour : PlayableBehaviour
	{
		[SerializeField] private Vector3 _startPos;
		[SerializeField] private Vector3 _endPos;

		public double EndTime { get; set; }


		public void SetParam(in Vector3 startPos, in Vector3 endPos)
		{
			_startPos = startPos;
			_endPos = endPos;
		}

		/// <summary>
        /// 再生中にオブジェクトを移動させる
        /// </summary>
		public void Transport(Transform transform, float rate)
		{
			var easingValueX = Mathf.Lerp(_startPos.x, _endPos.x, rate);
			var easingValueY = Mathf.Lerp(_startPos.y, _endPos.z, rate);
			var easingValueZ = Mathf.Lerp(_startPos.y, _endPos.z, rate);

			transform.position = new Vector3(easingValueX, easingValueY, easingValueZ);
		}
	}

	/// <summary>
    /// クリップ全体制御する
    ///
    /// トラックの振る舞いそのものの実装
    /// </summary>
	public class PlayableTransportMixerBehavior : PlayableBehaviour
	{
		private const float MaxProgress = 1f;
		private const int RootPlayable = 0;

		public class ClipInfo
        {
			public ScriptPlayable<PlayableTransportBehaviour> Playable; // PlayableGraphに生成されたPlayableインスタンス
			public PlayableTransportBehaviour Behaviour;    // PlayableBehaviourのインスタンス
			public double EndTime;  // トラック上のクリップの終端
			public bool IsSeeked;	// シーク済みかどうか
        }			


		private Transform _transformBinding;
		protected List<ClipInfo> ClipInfoList;

		public bool HasClips { get; set; }

		/// <summary>
        /// グラフ再生時に呼ばれる
        /// </summary>
        public override void OnGraphStart(Playable playable)
        {
			var inputCount = playable.GetInputCount();

			HasClips = inputCount > 0;
			if (!HasClips) return;

			// PlayableGraph開始時に必要なClip情報を事前にリスト化
			ClipInfoList = new List<ClipInfo>();

			for (int i = 0; i < inputCount; ++i)
            {
				var instance = (ScriptPlayable<PlayableTransportBehaviour>)playable.GetInput(i);
				var data = new ClipInfo
				{
					Playable = instance,
					Behaviour = instance.GetBehaviour(),
					EndTime = instance.GetBehaviour().EndTime,
					IsSeeked = false
				};

				ClipInfoList.Add(data);
			}

			ClipInfoList = ClipInfoList.OrderBy(data => data.EndTime).ToList();
        }

		/// <summary>
        /// グラフ停止時
        /// </summary>
        public override void OnGraphStop(Playable playable)
        {
			_transformBinding = null;
			ClipInfoList?.Clear();
        }

		/// <summary>
        /// フレーム再生中呼ばれ続ける
        /// </summary>
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
			if (!HasClips) return;

			_transformBinding = playerData as Transform;

			if (_transformBinding != null)
            {
				ProcessFrameTransport();
            }
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
			if (!HasClips) return;

			var lastClip = ClipInfoList.Last();
			if (lastClip.Behaviour != null && _transformBinding != null)
			{
				lastClip.Behaviour.Transport(_transformBinding, MaxProgress);
			}
        }

		private void ProcessFrameTransport()
        {
			// 現在再生中のクリップ
			var playingClip = ClipInfoList.FirstOrDefault(info => info.Playable.GetPlayState() == PlayState.Playing);

			foreach (var clipInfo in ClipInfoList)
            {
				var playable = clipInfo.Playable;
				var behaviour = clipInfo.Behaviour;
				if (behaviour == null) continue;

				// SeekがClip外、かつ、まだ一度も該当ClipがSeekされていない、かつ、ほかのClip内で現在Seekしていない.
				var isOver = playable.GetGraph().GetRootPlayable(RootPlayable).GetTime() >= clipInfo.EndTime;
				if (isOver && !clipInfo.IsSeeked && playingClip == null)
                {
					// 最終値を適用
					behaviour.Transport(_transformBinding, MaxProgress);
					clipInfo.IsSeeked = true;
                }
				else if (playingClip != null && playingClip.Behaviour == behaviour)
				{
					var progress = GetPlayableProgress(playable);
					behaviour.Transport(_transformBinding, progress);
				}
			}
		}

		private float GetPlayableProgress(Playable playable)
        {
			var getTIme = playable.GetTime();
			var duration = playable.GetDuration();
			var progress = Mathf.Clamp01((float)(getTIme / duration));

			return progress;
        }
	}
}
