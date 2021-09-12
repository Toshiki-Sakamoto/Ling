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

		[Inject] private Chara.CharaManager _charaManager = default;
		[Inject] private UserData.IUserDataHolder _userDataHolder = default;

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

			// 手持ちから使ったらアイテムを減らす
			_userDataHolder.ItemRepository.RemoveByUniq(arg.Item.Uniq);

			// スキル
			var skill = arg.Item.Skill;
			var player = _charaManager.Player;
			var skillProcess = player.AddAttackProcess<Skill.SkillProcess>();
			skillProcess.Setup(player, skill);

			// キャラアクションに移動する
			var process = new Process.ProcessCharaAction(player);
			Scene.ProcessContainer.Add(ProcessType.PlayerSkill, process);

			Change(Phase.PlayerActionProcess, new BattlePhasePlayerAction.Arg 
				{ 
					ActionProcessType = ProcessType.PlayerSkill, 
					TargetGetter = skillProcess 
				});
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
