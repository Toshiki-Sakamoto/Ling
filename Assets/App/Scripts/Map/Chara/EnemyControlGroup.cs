//
// EnemyModelGroup.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.10
//

using Cysharp.Threading.Tasks;
using Ling.MasterData.Stage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Utility.Pool;
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

		[SerializeField, ES3NonSerializable] private Chara.EnemyPoolManager _enemyPoolManager = null;
		[SerializeField] private int _mapLevel = 0;
		[ES3Serializable] private int _enemyCount = 0;

		[Inject] private MasterData.IMasterHolder _masterHolder = default;
		[Inject] private Utility.IEventManager _eventManager = default;

		private Dictionary<CharaModel, EnemyControl> _keyModelValueControl = new Dictionary<CharaModel, EnemyControl>();
		private MapMaster _mapMaster;

		#endregion


		#region プロパティ

		/// <summary>
		/// EnemyViewPool管理者
		/// </summary>
		public Chara.EnemyPoolManager EnemyPoolManager => _enemyPoolManager;

		public int MapLevel => _mapLevel;

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
		public void RemoveChara(EnemyControl chara)
		{
			var model = chara.Model; 
			
			Controls.Remove(chara);
			Models.Remove(model);

			if (!_keyModelValueControl.TryGetValue(model, out var enemy))
			{
				// 存在しない
				return;
			}

			var poolItem = enemy.GetComponent<PoolItem>();
			poolItem?.Detach();

			_keyModelValueControl.Remove(model);

			_enemyCount = _keyModelValueControl.Count;
		}

		public void ReturnViewAll()
		{
			foreach (var enemy in _keyModelValueControl)
			{
				var poolItem = enemy.Value.GetComponent<PoolItem>();
				poolItem?.Detach();
			}

			_keyModelValueControl.Clear();
			_enemyCount = 0;
		}

		/// <summary>
		/// MapMasterを指定して敵Viewの生成等、開始処理を行う
		/// </summary>
		public void Startup(MapMaster mapMaster, int mapLevel)
		{
			_mapMaster = mapMaster;
			_mapLevel = mapLevel;

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
						RemoveChara(enemy_);
					});

				enemyControl.OnDestroyed = subject;

				++_enemyCount;
			}
		}

		public override async UniTask SetupAsync()
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

		public override async UniTask ResumeAsync()
		{
			for (int i = 0; i < _enemyCount; ++i)
			{
				// プールから敵を生成する
				var filename = $"Chara/Enemy {_mapLevel}.save";
				
				var enemyControl = GetEnemyByPool();
				_saveDataHelper.LoadInto(filename, $"Enemy {i}", enemyControl.gameObject);

				enemyControl.Resume();
				
				var enemyMaster = _masterHolder.EnemyRepository.Find(enemyControl.Model.Type);
				EnemyFactory.Resume(this, enemyControl, enemyMaster);
				
				Models.Add(enemyControl.Model);
				_keyModelValueControl.Add(enemyControl.Model, enemyControl);

				// 削除時
				var subject = new Subject<EnemyControl>();
				subject.Subscribe(enemy_ =>
					{
						RemoveChara(enemy_);
					});

				enemyControl.OnDestroyed = subject;
			}
		}

		#endregion


		#region private 関数

		protected override void ResetInternal()
		{
			// ViewをすべてPoolに戻す
			ReturnViewAll();
		}

		
		private void Awake()
		{
			_eventManager.Add<Utility.SaveData.EventSaveCall>(this, ev =>
				{
					for (int i = 0; i < _enemyCount; ++i)
					{
						var control = Controls[i];
						var filename = $"Chara/Enemy {_mapLevel}.save";
						
						_saveDataHelper.Save(filename, $"Enemy {i}", control.gameObject);
					}
				});
		}

		#endregion
	}
}
