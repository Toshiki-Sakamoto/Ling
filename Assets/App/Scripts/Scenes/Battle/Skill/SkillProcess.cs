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

namespace Ling.Scenes.Battle.Skill
{
	/// <summary>
	/// スキル効果プロセス
	/// 一つの効果のみを表す。もし複数に効果がある場合はこのプロセスがその回数分生成されるようにする
	/// 複数のプロセスに対する制御は別の担当
	/// </summary>
	public class SkillProcess : Utility.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Map.MapManager _mapManager;
		[Inject] private Common.Effect.IEffectManager _effectManager;

		private Chara.ICharaController _chara, _target;
		private SkillMaster _skill;
		private CancellationTokenSource _cts;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(Chara.ICharaController chara, SkillMaster skill)
		{
			_chara = chara;
			_skill = skill;
		}

		public void SetTarget(Chara.ICharaController target)
		{
			_target = target;
		}

		protected override void ProcessStartInternal()
		{
			AttachProcess();

			Execute().Forget();
		}

		public async UniTask Execute()
		{
			_cts = new CancellationTokenSource();

			// キャラの向き直線にスキルを放つ
			// 壁にぶつかるまで直進する
			var searcher = _chara.FindTileDataMap(_mapManager).Seacher;
			var tileData = searcher.SearchLine(_chara.CellPos, _chara.Model.Dir.Value, Const.TileFlag.Wall);

			Common.Effect.IEffectMoveCore move = new Common.Effect.EffectMoveCoreConstantLiner();
			move.SetStartPos(_chara.CellToWorld(_mapManager));
			move.SetEndPos(_chara.CellToWorld(tileData.Pos, _mapManager));

			var effect = _effectManager.CreatePlayer(_skill);

			effect.Mover.RegisterCore(move);
			await effect.Mover.PlayAsync(_cts.Token);
			

			// 演出開始
			await UniTask.Delay(100); // todo: ちょっとまつだけ

			ProcessFinish();
		}

		#endregion


		#region private 関数

		private void AttachProcess()
		{
			// スキル内容によってプロセスを後ろにつけ合わす
			if (_skill.Heal != null)
			{
				SetNext<HealSkillProcess>().Setup(_chara, _skill.Heal);
			}

			if (_skill.Damage != null)
			{

			}
		}

		#endregion
	}
}
