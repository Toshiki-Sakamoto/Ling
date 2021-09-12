//
// HealSkillPorcess.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.04
//

using Ling.MasterData.Skill;
using Zenject;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using MessagePipe;

namespace Ling.Scenes.Battle.Skill
{
	/// <summary>
	/// ダメージスキル
	/// </summary>
	public class DamageSkillProcess : Utility.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Utility.IEventManager _eventManager;
		[Inject] private IPublisher<Chara.EventKilled> _killedEvent;


		private Chara.ICharaController _chara;
		private SkillDamageEntity _entity;
		private List<Chara.ICharaController> _targets;
		private List<Chara.ICharaController> _deadChara = new List<Chara.ICharaController>();


		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		public void Setup(Chara.ICharaController chara, SkillDamageEntity entity, List<Chara.ICharaController> targets)
		{
			_chara = chara;
			_entity = entity;
			_targets = targets;
		}

		#endregion


		#region public, protected 関数

		protected override void ProcessStartInternal()
		{
			Execute().Forget();
		}

		#endregion


		#region private 関数

		private async UniTask Execute()
		{
			foreach (var target in _targets)
			{
				// todo ダメージ演出

				// HPをへらす
				var damage = Chara.Calculator.CharaSkillCalculator.Calculate(_chara, target);
				target.Damage(damage);

				// 死亡しているか
				if (target.Status.IsDead.Value)
				{
					_deadChara.Add(target);
				}

				await UniTask.Delay(500);
			}

			// 死亡していた場合は倒した情報を送る
			foreach (var target in _deadChara)
			{
				// 倒した情報を送る
				_killedEvent.Publish(new Chara.EventKilled { unit = _chara, opponent = target });

				await UniTask.Delay(200);
			}

			ProcessFinish();
		}

		#endregion
	}
}
