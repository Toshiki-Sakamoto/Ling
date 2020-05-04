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
using UniRx;
using UniRx.Async;
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

		private bool _isPoolCreateProcessFinish = false;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ


		#endregion


		#region public, protected 関数

		public override void Init()
		{
			// タイルのプールを作成する
			// とりあえず最大の数作ってみる
			_poolManager = Resolve<PoolManager>();
			_poolManager.SetupPoolItem(PoolType.Map, 10 * 10);

			_poolManager.CreatePoolItemsAsync().Subscribe(_ =>
			{
				_isPoolCreateProcessFinish = true;
				Debug.Log($"Mapプール作成終了");
			});
		}

		public override void Proc() 
		{
			if (!_isPoolCreateProcessFinish) return;

			Change(BattleScene.Phase.FloorSetup);
		}

		public override void Term() 
		{
			_isPoolCreateProcessFinish = false;
		}

		#endregion


		#region private 関数


		#endregion
	}
}
