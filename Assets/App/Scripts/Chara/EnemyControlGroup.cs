﻿//
// EnemyModelGroup.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.10
//

using Cysharp.Threading.Tasks;
using Ling.MasterData.Stage;
using Ling.Scenes.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Ling.Utility.Pool;
using Zenject;

namespace Ling.Chara
{
	/// <summary>
	/// 敵のControl管理クラス
	/// </summary>
	public class EnemyControlGroup : ControlGroupBase<EnemyControl, CharaModel, EnemyView>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private Chara.EnemyPoolManager _enemyPoolManager = null;

		private Dictionary<CharaModel, EnemyView> _enemyViews = new Dictionary<CharaModel, EnemyView>();
		private MapMaster _mapMaster;

		#endregion


		#region プロパティ

		/// <summary>
		/// EnemyViewPool管理者
		/// </summary>
		public Chara.EnemyPoolManager EnemyPoolManager => _enemyPoolManager;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数


		/// <summary>
		/// 敵のViewを一つプールから取り出す
		/// </summary>
		/// <returns></returns>
		public EnemyView GetEnemyByPool(CharaModel charaModel)
		{
			var enemy = EnemyPoolManager.Pop<EnemyView>(EnemyType.Normal);
			_enemyViews.Add(charaModel, enemy);

			return enemy;
		}

		/// <summary>
		/// 敵Viewを検索して取得
		/// </summary>
		public EnemyView FindEnemy(CharaModel model) =>
			_enemyViews[model];

		/// <summary>
		/// 指定したModelのViewを削除(プールに戻す)
		/// </summary>
		public void RemoveChara(CharaModel charaModel)
		{
			if (!_enemyViews.TryGetValue(charaModel, out var enemy))
			{
				// 存在しない
				return;
			}

			var poolItem = enemy.GetComponent<PoolItem>();
			poolItem?.Detach();

			_enemyViews.Remove(charaModel);
		}

		public void ReturnViewAll()
		{
			foreach (var enemy in _enemyViews)
			{
				var poolItem = enemy.Value.GetComponent<PoolItem>();
				poolItem?.Detach();
			}

			_enemyViews.Clear();
		}

		/// <summary>
		/// MapMasterを指定して敵Viewの生成等、開始処理を行う
		/// </summary>
		public async UniTask StartupAsync(MapMaster mapMaster)
		{
			_mapMaster = mapMaster;

			for (int i = 0, size = _mapMaster.InitCreateNum.GetRandomValue(); i < size; ++i)
			{
				var model = CreateEnemyModel();

				// ViewとModelを結びつける
			}

			foreach (var model in Models)
			{
				//var enemyControl = EnemyControl.Create(model, _view.GetEnemyByPool(model));
			}
		}

		public CharaModel CreateEnemyModel()
		{
			var mapEnemyData = _mapMaster.GetRandomEnemyDataFromPopRate();
			var enemyModel = EnemyFactory.Create(mapEnemyData);

			Models.Add(enemyModel);

			return enemyModel;
		}

		protected override async UniTask SetupAsyncInternal()
		{
			// プール情報から敵Viewを生成する
			await EnemyPoolManager.CreateObjectsAsync();
		}

		/// <summary>
		/// 指定座標にキャラクターが存在するか
		/// </summary>
		public bool ExistsCharaInPos(Vector2Int pos)
		{
			return Models.Exists(model => model.Pos == pos);
		}

		#endregion


		#region private 関数

		protected override void ResetInternal()
		{
			// ViewをすべてPoolに戻す
			ReturnViewAll();
		}

		#endregion
	}
}