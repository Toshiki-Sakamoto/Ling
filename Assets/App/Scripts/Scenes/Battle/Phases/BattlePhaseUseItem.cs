//
// BattlePhaseUseItem.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.03
//

using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;
using Utility.Extensions;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// アイテムを使用した
	/// </summary>
	public class BattlePhaseUseItem : BattlePhaseBase
	{
		#region 定数, class, enum

		public class Arg : Utility.PhaseArgument
		{
			public Common.Item.ItemEntity Item;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseStart()
		{
			// アイテムによって処理を変更する
			var arg = Argument as Arg;

			// スキル
			var skill = arg.Item.Skill;
		}

		/// <summary>
		/// 非同期
		/// </summary>
		public override async UniTask PhaseStartAsync(CancellationToken token)
		{
			await Scene.ProcessContainer.ExecuteAsync(ProcessType.Reaction, token);
		}

		public override void PhaseUpdate()
		{
		}

		public override void PhaseStop()
		{
		}

		#endregion


		#region private 関数

		#endregion
	}
}
