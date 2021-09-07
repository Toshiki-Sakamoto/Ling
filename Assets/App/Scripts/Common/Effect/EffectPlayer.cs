﻿// 
// SkillPlayer.cs  
// ProductName Ling
//  
// Created by  on 2021.08.10
// 

using UnityEngine;
using UnityEngine.Playables;
using System;
using System.Collections.Generic;
using Utility.Timeline;

namespace Ling.Common.Effect
{
	/// <summary>
	/// エフェクトの再生を担う
	/// </summary>
	public class EffectPlayer : TimelinePlayer
	{
		#region 定数, class, enum

		public enum Type
		{
			StartToEnd,
			Start,
			End,
		}

		#endregion


		#region public 変数


		#endregion


		#region private 変数

		[SerializeField] private Type _type = Type.StartToEnd;

		#endregion


		#region プロパティ

		// 移動はよく使うのでデフォルト化
		public IEffectMover Mover { get; private set; }

		#endregion


		#region public, protected 関数

		public override void Initialize()
		{
			var mover = gameObject.AddComponent<EffectMover>();
			Mover = mover;

			Owner.Register(mover);
		}

		public void Setup()
		{
			// スキルの情報を適用
			
		}

		#endregion



		#region private 関数

		#endregion


		#region MonoBegaviour

        #endregion
    }
}