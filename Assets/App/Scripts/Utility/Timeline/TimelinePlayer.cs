﻿// 
// TimelinePlayer.cs  
// ProductName Ling
//  
// Created by  on 2021.08.22
// 

using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Playables;
using Utility.CustomBehaviour;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Utility.Timeline
{
	public interface ITimelinePlayer : ICustomComponent
	{
	}

	/// <summary>
	/// タイムラインを再生するために必要なものを持つクラス
    /// 汎用的に作る
	/// </summary>
    [RequireComponent(typeof(CustomTimelineCollections))]
	public class TimelinePlayer : Utility.CustomBehaviour.AbstractCustomBehaviour, ITimelinePlayer
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private PlayableDirector _skillTimeline;

		#endregion


		#region プロパティ

		public PlayableDirector PlayableDirector => _skillTimeline;

        #endregion


        #region public, protected 関数

        public override void Register(ICustomBehaviourCollection owner)
        {
            base.Register(owner);

			owner.AddCustomComponent<ITimelinePlayer>(this);
        }

        public void Play()
		{
			PlayAsync().Forget();
		}

		public async UniTask PlayAsync()
		{
			var cts = new CancellationTokenSource();
			var list = new List<UniTask>();

			Owner.ForEach<ICustomTimeline>(elm =>
				{
					list.Add(elm.PlayAsync(cts.Token));
				});

			_skillTimeline.Play();

			await UniTask.WhenAll(list);
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			if (_skillTimeline == null)
			{
				_skillTimeline = GetComponent<PlayableDirector>();
			}
		}

		#endregion
	}
}