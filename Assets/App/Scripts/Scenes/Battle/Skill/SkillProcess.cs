//
// SkillProcess.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.03
//

using Ling.MasterData.Skill;
using Cysharp.Threading.Tasks;
using System.Threading;

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

		private Chara.ICharaController _chara;
		private SkillMaster _skill;

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

		protected override void ProcessStartInternal()
		{
			AttachProcess();

			Execute().Forget();
		}

		public async UniTask Execute()
		{
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
