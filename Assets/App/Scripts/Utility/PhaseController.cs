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

namespace Ling.Utility
{
	/// <summary>
	/// Phaseを管理する
	/// </summary>
	public class PhaseController<T> : MonoBehaviour
		where T : Enum
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[ShowInInspector, ReadOnly] private T _currentType = default(T);

		private GameObject _owner;
		private Dictionary<T, Phase<T>> _phaseDict = new Dictionary<T, Phase<T>>();
		private Phase<T> _currentPhase;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void SetOwner(GameObject owner)
		{
			_owner = owner;
		}

		public void Add<TPhase>(T type) where TPhase : Phase<T>
		{
			var phase = _owner.AddComponent<TPhase>();

			_phaseDict[type] = phase;
		}

		public void Start(T type, PhaseArgument argument = null)
		{
			foreach (var pair in _phaseDict)
			{
				var phase = pair.Value;
				phase.SetController(this);
			}
			
			Change(type, argument);
		}

		public void Change(T type, PhaseArgument argument = null)
		{
			if (!_phaseDict.TryGetValue(type, out var phase))
			{
				Utility.Log.Error($"有効なPhaseが見つからない {type}");
				return;
			}

			_currentPhase?.PhaseStop();

			phase.Argument = argument;
			_currentPhase = phase;
			_currentType = type;

			phase.PhaseStart();
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