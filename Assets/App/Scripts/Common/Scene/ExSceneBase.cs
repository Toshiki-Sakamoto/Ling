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
	public class ExSceneBase : Base
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[ShowInInspector] protected PhaseController _phase = default;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void RegistPhase<TPhase>(Enum phaseType) where TPhase : Phase
		{
			_phase.Regist<TPhase>(phaseType);
		}

		public void StartPhase(Enum phaseType)
		{
			_phase.StartPhase(phaseType);
		}

		#endregion


		#region private 関数

		protected override void Awake()
		{
			base.Awake();

			_phase = _diContainer.InstantiateComponent<PhaseController>(gameObject);
			_phase.SetOwner(gameObject);
		}

		#endregion
	}
}
