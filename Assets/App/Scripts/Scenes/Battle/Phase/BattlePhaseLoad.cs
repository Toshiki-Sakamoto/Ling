//
// BattlePhaseLoad.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
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
	public class BattlePhaseLoad : Utility.PhaseScene<BattleScene.Phase, BattleScene>.Base
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

		public BattlePhaseLoad()
		{
			_builderManager = GameManager.Instance.Resolve<Map.Builder.IManager>();
			_builderFactory = GameManager.Instance.Resolve<Map.Builder.BuilderFactory>();
		}

		#endregion


		#region public, protected 関数

		public override void Awake() 
		{ 
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
			if (!_isLoadFinish) return;

			Change(BattleScene.Phase.CharaCreate);
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

			var builder = _builderManager.Builder;
			var tileDataMap = builder.TileDataMap;

			// マップを作成、配置する
			foreach(var tileData in tileDataMap)
			{

			}

			_isLoadFinish = true;
		}

		#endregion
	}
}
