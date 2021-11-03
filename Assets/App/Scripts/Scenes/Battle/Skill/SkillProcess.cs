//
// SkillProcess.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.03
//

using Ling.MasterData.Skill;
using Cysharp.Threading.Tasks;
using System.Threading;
using Zenject;
using Ling.Map;
using Utility.Extensions;
using System.Collections.Generic;
using UniRx;

namespace Ling.Scenes.Battle.Skill
{
	/// <summary>
	/// スキル効果プロセス
	/// 一つの効果のみを表す。もし複数に効果がある場合はこのプロセスがその回数分生成されるようにする
	/// 複数のプロセスに対する制御は別の担当
	/// </summary>
	public class SkillProcess : Utility.ProcessBase, Process.IProcessTargetGetter
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private DiContainer _diContainer;

		private Chara.ICharaController _chara, _target;
		private SkillMaster _skill;
		private SkillCreateImpl _impl;
		private Map.SearchResult _searchResult;

		#endregion


		#region プロパティ

		public List<Chara.ICharaController> Targets { get; } = new List<Chara.ICharaController>();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(Chara.ICharaController chara, SkillMaster skill)
		{
			Setup(chara, skill, null);
		}

		public void Setup(Chara.ICharaController chara, SkillMaster skill, Map.SearchResult result)
		{
			_chara = chara;
			_skill = skill;
			_searchResult = result;

			// ターゲットの方を向く
			chara.Model.SetDirection(result.Dir.ToVec2Int());

			_impl = _diContainer.Instantiate<SkillCreateImpl>();
			_impl.Setup(chara, skill, _searchResult);
		}

		public void SetTarget(Chara.ICharaController target)
		{
			//_target = target;
		}

		protected override void ProcessStartInternal()
		{
			Execute().Forget();
		}

		public async UniTask Execute()
		{
			await UniTask.Delay(100); // todo: ちょっとまつだけ

			_impl.OnTarget
				.Subscribe(target => 
				{
					Targets.Add(target);
				});

			var effectPlayer = _impl.Build();
			if (effectPlayer != null)
			{
				await effectPlayer.PlayAsync();
			}
			
			// 演出開始
			await UniTask.Delay(100); // todo: ちょっとまつだけ

			AttachProcess();

			ProcessFinish();
		}

		#endregion


		#region private 関数

		private void AttachProcess()
		{
			// ターゲットがいない場合は何もせずに終わる
			if (Targets.IsNullOrEmpty())
			{
				ProcessFinish();
				return;
			}

			// スキル内容によってプロセスを後ろにつけ合わす
			if (_skill.Heal != null)
			{
				SetNext<HealSkillProcess>()
					.Setup(_chara, _skill.Heal);
			}

			if (_skill.Damage != null)
			{
				SetNext<DamageSkillProcess>()
					.Setup(_chara, _skill.Damage, Targets);
			}
		}

		#endregion
	}
}
