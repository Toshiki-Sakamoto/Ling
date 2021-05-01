// 
// PhaseController.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.18
// 

using UnityEngine;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Utility
{
	/// <summary>
	/// Phaseを管理する
	/// </summary>
	public class PhaseController : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[Inject] private DiContainer _container;

		[Inject] protected Utility.IEventManager _eventManager = null;
		[ShowInInspector] private Enum _currentType = default(Enum);

		private GameObject _owner;
		private Dictionary<Enum, Phase> _phaseDict = new Dictionary<Enum, Phase>();
		private Phase _currentPhase;
		private CancellationTokenSource _cancellationTokenSource;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void SetOwner(GameObject owner)
		{
			_owner = owner;
		}

		public void Regist<TPhase>(Enum type) where TPhase : Phase
		{
			var phase = _container.InstantiateComponent<TPhase>(_owner);

			_phaseDict[type] = phase;
		}

		public void StartPhase(Enum type, PhaseArgument argument = null)
		{
			foreach (var pair in _phaseDict)
			{
				var phase = pair.Value;
				phase.SetController(this);

				// 初期化処理を一度だけ呼び出す
				phase.Initialize();
			}
			
			ChangePhase(type, argument);
		}

		public void ChangePhase(Enum type, PhaseArgument argument = null)
		{
			if (!_phaseDict.TryGetValue(type, out var phase))
			{
				Utility.Log.Error($"有効なPhaseが見つからない {type}");
				return;
			}

			_cancellationTokenSource?.Cancel();

			if (_currentPhase != null)
			{
				_currentPhase.IsPlaying = false;
				_currentPhase.PhaseStop();
			}

			phase.Argument = argument;
			_currentPhase = phase;
			_currentType = type;

			phase.IsPlaying = true;
			phase.PhaseStart();

			if (phase.IsPlaying)
			{
				// 非同期を投げっぱにする
				_cancellationTokenSource = new CancellationTokenSource();

				phase.PhaseStartAsync(_cancellationTokenSource.Token).Forget();
			}
		}

		public void Update()
		{
			_currentPhase?.PhaseUpdate();
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}