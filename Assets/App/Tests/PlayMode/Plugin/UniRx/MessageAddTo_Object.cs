// 
// MessageAddTo_Object.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.09.20
// 

using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

namespace Ling.Tests.PlayMode.Plugin.UniRx
{
	/// <summary>
	/// AddToテストするためのMonoBehaviourクラス
	/// </summary>
	public class MessageAddTo_Object : MonoBehaviour 
    {
		/// <summary>
		/// AddToテストのためのTimerCounter
		/// </summary>
		public class TimerCounter
		{
			private Subject<int> _timerSubject = new Subject<int>();

			public IObservable<int> OnTimeChanged => _timerSubject;

			public void Start()
			{
				TimerAsync().Forget();
			}

			private async UniTask TimerAsync()
			{
				_timerSubject.Subscribe(num => Debug.Log("UniRxTest AddTo Count " + num));

				var time = 5;
				while (time >= 0)
				{
					_timerSubject.OnNext(--time);
					await UniTask.DelayFrame(1);
				}

				_timerSubject.OnCompleted();
			}
		}

		private TimerCounter _timeCounter = new TimerCounter();

		private async UniTask DestroyAtWaitForFrame()
		{
			await UniTask.DelayFrame(1);

			Destroy(gameObject);
		}

		void Start()
		{
			_timeCounter.OnTimeChanged
				.Where(x => x == 0)
				.Subscribe(_ =>
				{
					transform.position = Vector3.zero;
				}).AddTo(gameObject); // 指定のgameobjectが破棄されたらDisposeする

			_timeCounter.Start();

			DestroyAtWaitForFrame().Forget();
		}
	}
}