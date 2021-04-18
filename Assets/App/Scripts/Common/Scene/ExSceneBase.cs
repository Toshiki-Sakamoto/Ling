//
// ExSceneBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.18
//

using UnityEngine;
using System;
using Ling.Utility;
using Sirenix.OdinInspector;

namespace Ling.Common.Scene
{
	/// <summary>
	/// SceneBaseを拡張したクラス
	/// 基本的に使用されるものを内包している
	/// </summary>
	public class ExSceneBase<TPhaseType> : Base
		where TPhaseType : Enum
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[ShowInInspector] protected PhaseController<TPhaseType> _phase = default;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void AddPhase<TPhase>(TPhaseType phaseType) where TPhase : Phase<TPhaseType>
		{
			_phase.Add<TPhase>(phaseType);
		}

		#endregion


		#region private 関数

		private void Awake()
		{
			_phase = gameObject.AddComponent<PhaseController<TPhaseType>>();
		}

		#endregion
	}
}
