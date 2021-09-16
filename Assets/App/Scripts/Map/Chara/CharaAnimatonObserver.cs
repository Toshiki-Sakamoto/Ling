// 
// CharaAnimatonObserver.cs  
// ProductName Ling
//  
// Created by  on 2021.09.15
// 

using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

namespace Ling.Chara
{
	public enum AnimationState
	{
		Enter,
		Exit,
	}

	/// <summary>
	/// キャラクタのアニメーションを監視する
	/// </summary>
	[RequireComponent(typeof(Animator))]
	public class CharaAnimatonObserver : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		private Animator _animator;

		private Subject<AnimationState> _onDamageSubject = new Subject<AnimationState>();

		#endregion


		#region プロパティ

		public IObservable<AnimationState> OnDamage => _onDamageSubject;

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
			_animator = GetComponent<Animator>();

			var stateMachineTrigger = _animator.GetBehaviour<ObservableStateMachineTrigger>();
			if (stateMachineTrigger == null)
			{
				Utility.Log.Error("ObservableStateMachineTriggerを持っていません");
				return;
			}

			var exitConnectableObservable = stateMachineTrigger
				.OnStateExitAsObservable()
				.Publish();

			exitConnectableObservable.Connect();

			exitConnectableObservable
				.Where(info => info.StateInfo.IsTag("Damage"))
				.Subscribe(info => 
				{
					_onDamageSubject.OnNext(AnimationState.Exit);
				})
				.AddTo(this);
		}

		#endregion
	}
}