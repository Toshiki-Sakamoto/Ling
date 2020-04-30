// 
// Scene.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.04.13
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Ling.Scenes.Battle
{
	/// <summary>
	/// 
	/// </summary>
	public class BattleScene : Common.Scene.Base 
    {
		#region 定数, class, enum

		public enum Phase
		{
			Start,
		}

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		[Inject] private Map.Builder.IManager _builderManager = null;
		[Inject] private Map.Builder.BuilderFactory _builderFactory = null;

		[SerializeField] private BattleView _view = null;

		protected Utility.PhaseObj<Phase> _phase = new Utility.PhaseObj<Phase>();

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
			//_phase.Add(Phase.Start, )
		}

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
			var builderData = new Map.Builder.BuilderData();

			var builder = _builderFactory.Create(Map.Builder.BuilderConst.BuilderType.Split);
			builder.Initialize(20, 20);

			_builderManager.SetData(builderData);
			_builderManager.SetBuilder(builder);

			_builderManager.Builder.Execute();
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
		void OnDestoroy()
		{
		}

		#endregion
	}
}