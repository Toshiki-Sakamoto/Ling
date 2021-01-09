//
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
using UniRx;

namespace Ling.Chara
{
	/// <summary>
	/// 敵のControl管理クラス
	/// </summary>
	public class EnemyControlGroup : ControlGroupBase<EnemyControl, EnemyModel, EnemyView>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private Chara.EnemyPoolManager _enemyPoolManager = null;

		[Inject] MasterData.IMasterHolder _masterHolder = default;

		private Dictionary<CharaModel, EnemyControl> _keyModelValueControl = new Dictionary<CharaModel, EnemyControl>();
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
		public EnemyControl GetEnemyByPool()
		{
			var enemy = EnemyPoolManager.Pop<EnemyControl>(EnemyType.Normal);
			Controls.Add(enemy);

			return enemy;
		}

		/// <summary>
		/// 敵Viewを検索して取得
		/// </summary>
		public EnemyControl FindEnemyByModel(CharaModel model) =>
			_keyModelValueControl[model];

		/// <summary>
		/// 指定したModelのViewを削除(プールに戻す)
		/// </summary>
		public void RemoveChara(CharaModel charaModel)
		{
			if (!_keyModelValueControl.TryGetValue(charaModel, out var enemy))
			{
				// 存在しない
				return;
			}

			var poolItem = enemy.GetComponent<PoolItem>();
			poolItem?.Detach();

			_keyModelValueControl.Remove(charaModel);
		}

		public void ReturnViewAll()
		{
			foreach (var enemy in _keyModelValueControl)
			{
				var poolItem = enemy.Value.GetComponent<PoolItem>();
				poolItem?.Detach();
			}

			_keyModelValueControl.Clear();
		}

		/// <summary>
		/// MapMasterを指定して敵Viewの生成等、開始処理を行う
		/// </summary>
		public void Startup(MapMaster mapMaster)
		{
			_mapMaster = mapMaster;

			for (int i = 0, size = _mapMaster.InitCreateNum.GetRandomValue(); i < size; ++i)
			{
				// プールから敵を取得する
				var enemyControl = GetEnemyByPool();
				var mapEnemyData = _mapMaster.GetRandomEnemyDataFromPopRate();
				var enemyMaster = _masterHolder.EnemyRepository.Find(mapEnemyData.EnemyType);
				EnemyFactory.Create(this, enemyControl, enemyMaster);

				Models.Add(enemyControl.Model);
				_keyModelValueControl.Add(enemyControl.Model, enemyControl);

				// 削除時
				var subject = new Subject<EnemyControl>();
				subject.Subscribe(enemy_ => 
					{
						Controls.Remove(enemy_);
						Models.Remove(enemy_.Model);

						var poolItem = enemy_.GetComponent<PoolItem>();
						poolItem?.Detach();
						
						_keyModelValueControl.Remove(enemy_.Model);
					});

				enemyControl.OnDestroyed = subject;
			}
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
			return Models.Exists(model => model.CellPosition.Value == pos);
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
