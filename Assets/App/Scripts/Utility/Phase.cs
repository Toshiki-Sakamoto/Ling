//
// Phase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.30
//

using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using System.Threading;

namespace Utility
{
	public class PhaseArgument
	{
	}

	public class Phase : MonoBehaviour
	{
		public PhaseArgument Argument { get; set; }

		private PhaseController _controller;

		public void SetController(PhaseController controller) =>
			_controller = controller;

		public void Change<TType>(TType type, PhaseArgument argument = null) where TType : Enum =>
			_controller.ChangePhase(type, argument);

		public void Initialize()
		{
			PhaseInit();
		}


		/// <summary>
		/// フェーズが生成された後一度だけ呼び出される
		/// </summary>
		public virtual void PhaseInit() { }

		/// <summary>
		/// フェーズが切り替わった時に一度だけ呼び出される
		/// </summary>
		public virtual void PhaseStart() { }

		/// <summary>
		/// 非同期
		/// </summary>
		public virtual UniTask PhaseStartAsync(CancellationToken token) => 
			UniTask.FromResult(default(Unit));

		/// <summary>
		/// フェーズがアクティブ状態の時に呼び出され続ける
		/// </summary>
		public virtual void PhaseUpdate() { }

		/// <summary>
		/// フェーズが終了する時一度だけ呼び出される
		/// </summary>
		public virtual void PhaseStop() { }
	}
}
