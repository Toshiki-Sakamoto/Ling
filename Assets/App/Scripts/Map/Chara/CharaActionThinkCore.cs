//
// CharaActionThinkCpre.cs
// ProductName Ling
//
// Created by Toshiki Sakamoto on 2021.10.24
//

using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Ling.Chara
{
	/// <summary>
	/// 
	/// </summary>
	public class CharaActionThinkCore
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private GameObject _target;
		private bool _isFinished;

		#endregion


		#region プロパティ

		public MasterData.Skill.SkillMaster SkillMaster { get; private set; }

		public Map.SearchResult Result { get; private set; }

		public System.Action OnEnded { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void SetTarget(GameObject gameObject) =>
			_target = gameObject;


		public async UniTask ThinkStartAsync(MasterData.Skill.SkillMaster skillMaster)
		{
			_isFinished = false;
			SkillMaster = skillMaster;

			CustomEvent.Trigger(_target, "SkillActionThink", skillMaster);

			if (!_isFinished)
			{
				await UniTask.WaitUntil(() => _isFinished);
			}
		}

		public void SetResult(Map.SearchResult result)
		{
			Result = result;
		}

		public void ThinkEnded()
		{
			_isFinished = true;

			OnEnded?.Invoke();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
