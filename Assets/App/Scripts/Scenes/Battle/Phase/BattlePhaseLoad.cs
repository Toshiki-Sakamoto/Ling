//
// BattlePhaseLoad.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
//

using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// 
	/// </summary>
	public class BattlePhaseLoad : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private PoolManager _poolManager;
		private MapManager _mapManager = null;
		private Chara.CharaManager _charaManager = null;

		private bool _isFinish = false;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ


		#endregion


		#region public, protected 関数

		public override void Awake()
		{
			base.Awake();

			_mapManager = Resolve<MapManager>();
			_charaManager = Resolve<Chara.CharaManager>();
		}

		public override void Init()
		{
			// タイルのプールを作成する
			// とりあえず最大の数作ってみる
			_poolManager = Resolve<PoolManager>();

			LoadAsync().Forget();
		}

		public override void Proc() 
		{
			if (!_isFinish) return;

			Change(BattleScene.Phase.FloorSetup);
		}

		public override void Term() 
		{
			_isFinish = false;
		}

		#endregion


		#region private 関数

		private async UniTask LoadAsync()
		{
			// 最初のマップ作成
			_mapManager.Setup(_model.StageMaster);

			await _mapManager.BuildMapAsync(1, 2);
			
			// 1階層目を開始地点とする
			_mapManager.SetCurrentMap(1);

			// キャラクタのセットアップ処理
			await _charaManager.InitializeAsync();

			_charaManager.SetStageMaster(_model.StageMaster);

			// 初期マップの敵を生成する
			await _charaManager.BuildEnemyGroupAsync(1, _mapManager.FindTilemap(1));
			await _charaManager.BuildEnemyGroupAsync(2, _mapManager.FindTilemap(2));

			_isFinish = true;
		}

		#endregion
	}
}
