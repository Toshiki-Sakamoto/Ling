//
// BattlePhaseItemGet.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.16
//

using Cysharp.Threading.Tasks;
using Ling;
using System.Collections.Generic;
using Utility.Extensions;
using Zenject;
using System.Threading;

namespace Ling.Scenes.Battle.Phases
{
	/// <summary>
	/// アイテムを拾う
	/// </summary>
	public class BattlePhaseItemGet : BattlePhaseBase
	{
		#region 定数, class, enum

		public class Arg : Utility.PhaseArgument
		{
			public Phase NextPhase;
		}

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Map.MapManager _mapManager;
		[Inject] private Chara.CharaManager _charaManager;
		[Inject] private UserData.IUserDataHolder _userDataHolder;
		[Inject] private Utility.IEventManager _eventManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void PhaseStart()
		{
			var player = _charaManager.Player;
			var index = player.Model.CellIndex;

			var dropItemController = _mapManager.MapControl.FindDropItemController(_mapManager.CurrentMapIndex);
			if (!dropItemController.TryGetItemObject(index, out var itemControl))
			{
				// ないので終了
				ChangeNextPhase();
				return;
			}

			// あった場合、持ち物に空きがあれば加える。
			_userDataHolder.ItemRepository.ConfirmAndAdd(itemControl.Master);

			// なければ入れ替えの選択肢を出す

			_eventManager.Trigger(new Chara.EventItemGet { ItemMaster = itemControl.Master });

			// 入れ替えたらオブジェクトを削除
			dropItemController.Release(index);

			// 終了
			ChangeNextPhase();
		}

/*
		public override async UniTask PhaseStartAsync(CancellationToken token)
		{
			await Scene.ProcessContainer.UniTaskExecuteOnceAsync(ProcessType.PlayerSkill, token: token);

			Change(Phase.EnemyTink);
		}
		*/

		#endregion


		#region private 関数

		private void ChangeNextPhase()
		{
			var arg = Argument as Arg;
			if (arg == null) return;

			Change(arg.NextPhase);
		}

		#endregion
	}
}
