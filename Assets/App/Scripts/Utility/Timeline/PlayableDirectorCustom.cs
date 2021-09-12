// 
// PlayableDirectorCustom.cs  
// ProductName Ling
//  
// Created by  on 2021.09.09
// 

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;

namespace Utility.Timeline
{
	/// <summary>
	/// PlayableDirectorを使いやすくする
	/// </summary>
	[RequireComponent(typeof(PlayableDirector))]
	public class PlayableDirectorCustom : MonoBehaviour 
	{
		#region 定数, class, enum

		private enum State
		{
			Stopped, Playing, Paused,
		}

		#endregion


		#region public 変数

		public double Time
		{
			get => _director.time;
		}

		#endregion


		#region private 変数

		[SerializeField] private PlayableDirector _director;

		private Subject<Unit> _completeSubject = new Subject<Unit>();
		private Subject<Unit> _stepCompleteSubject = new Subject<Unit>();

		private State _state;

		#endregion


		#region プロパティ

		/// <summary>
		/// 終了したとき
		/// </summary>
		public IObservable<Unit> OnCompleteAsObservable => _completeSubject;

		/// <summary>
		/// ループの場合、最後にたどり着くたびに呼び出される
		/// </summary>
		public IObservable<Unit> OnStepCompleteAsObservable => _stepCompleteSubject;

		public DirectorWrapMode ExtrapolationMode => _director.extrapolationMode;

		public double Duration => _director.duration;

		/// <summary>
		/// 最後までたどり着いたカウント数
		/// </summary>
		public int CompletedLoops { get; private set; }

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		private async UniTaskVoid WaitComplete(CancellationToken ct)
		{
			// キャンセルされたら途中で抜け出す
			while (!ct.IsCancellationRequested)
			{
				await UniTask.Yield(PlayerLoopTiming.Update, ct);
				var prevTime = Time;

				await UniTask.Yield(PlayerLoopTiming.LastEarlyUpdate, ct);
				var currentTime = Time;

				switch (ExtrapolationMode)
				{
					case DirectorWrapMode.Loop:
						if (currentTime < prevTime)
						{
							++CompletedLoops;
							_stepCompleteSubject.OnNext(Unit.Default);
						}
						continue;

					// Holdでの再生終了時stoppedが呼ばれない
					case DirectorWrapMode.Hold:
					{
						if (!_director.playableGraph.IsValid()) continue;
						if (Time < Duration) continue;
						if (_state == State.Stopped) continue;

						_state = State.Stopped;
						CompletedLoops = 1;
						_completeSubject.OnNext(Unit.Default);
					}
					break;

					case DirectorWrapMode.None:
					{
						continue;
					}

					default:
						continue;
				}
			}
		}

		public void Play()
		{
			if (_state == State.Playing)
			{
				Utility.Log.Warning("すでに再生しています");
				return;
			}

			switch (_state)
			{
				case State.Stopped:
				{
					CompletedLoops = 0;
					_director.time = 0;
					_director.Evaluate();
					break;
				}

				case State.Paused:
				{
					_director.Evaluate();
					break;
				}

				default:
					throw new ArgumentOutOfRangeException();
			}

			var prevState = _state;
			_state = State.Playing;

			_director.Play();

			if (prevState == State.Stopped)
			{

			}
		}

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			if (_director == null)
			{
				_director = GetComponent<PlayableDirector>();
			}
		}

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
			if (_director.state == UnityEngine.Playables.PlayState.Playing)
			{
				_state = State.Playing;


			}

			// Noneはstoppedが呼ばれる
			_director.stopped += _ => 
				{
					if (_state != State.Playing) return;

					_state = State.Stopped;
					CompletedLoops = 1;

					_stepCompleteSubject.OnNext(Unit.Default);
					_completeSubject.OnNext(Unit.Default);
				};

			// Hold/Loopの場合
			WaitComplete(this.GetCancellationTokenOnDestroy()).Forget();
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
			_completeSubject.OnCompleted();
			_completeSubject.Dispose();

			_stepCompleteSubject.OnCompleted();
			_stepCompleteSubject.Dispose();
		}

		#endregion
	}
}