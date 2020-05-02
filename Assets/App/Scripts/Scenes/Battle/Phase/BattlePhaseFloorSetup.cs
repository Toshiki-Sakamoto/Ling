//
// BattlePhaseFloorSetup.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.02
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// 
	/// </summary>
	public class BattlePhaseFloorSetup : Utility.PhaseScene<BattleScene.Phase, BattleScene>.Base
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Map.Builder.IManager _builderManager = null;
		private Map.Builder.BuilderFactory _builderFactory = null;
		private bool _isLoadFinish;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void Awake()
		{
			_builderManager = Resolve<Map.Builder.IManager>();
			_builderFactory = Resolve<Map.Builder.BuilderFactory>();

			var builderData = new Map.Builder.BuilderData();

			var builder = _builderFactory.Create(Map.Builder.BuilderConst.BuilderType.Split);
			builder.Initialize(20, 20);

			_builderManager.SetData(builderData);
			_builderManager.SetBuilder(builder);

			_ = LoadAsync();
		}

		public override void Init() 
		{
		}

		public override void Proc() 
		{
		}

		public override void Term() 
		{ 
		}

		#endregion


		#region private 関数

		private async UniTask LoadAsync()
		{
			// マップの作成
			await _builderManager.Builder.Execute();

			var view = Resolve<BattleView>();
			var miniMap = view.MiniMap;

			miniMap.Setup(20, 20);

			var builder = _builderManager.Builder;
			var tileDataMap = builder.TileDataMap;

			// ミニマップを作成する
			// マップを作成、配置する
			foreach (var tileData in tileDataMap)
			{

			}

			_isLoadFinish = true;
		}


		private void CreateMiniMap()
		{

		}

		#endregion
	}
}
