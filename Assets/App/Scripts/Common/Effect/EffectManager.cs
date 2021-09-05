// 
// EffectManager.cs  
// ProductName Ling
//  
// Created by  on 2021.09.01
// 

using UnityEngine;
using Utility.Pool;
using Zenject;
using UniRx;

namespace Ling.Common.Effect
{
	public interface IEffectManager
	{
		EffectPlayer CreatePlayer(MasterData.Skill.SkillMaster master);
	}

	/// <summary>
	/// エフェクト管理者
	/// </summary>
	public class EffectManager : MonoBehaviour, IEffectManager
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField, ES3NonSerializable] private EffectPool _poolManager;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		/// <summary>
        /// マスタデータからエフェクトを生成する
        /// </summary>
		EffectPlayer IEffectManager.CreatePlayer(MasterData.Skill.SkillMaster master)
		{
			var player = _poolManager.Pop<EffectPlayer>(master.EffectType, master.Filename);
			if (player == null)
			{
				return default(EffectPlayer);
			}

			return player;
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
		}

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update()
		{
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		void OnDestroy()
		{
		}

		#endregion
	}
}