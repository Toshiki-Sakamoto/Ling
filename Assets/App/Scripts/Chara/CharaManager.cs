// 
// CharaManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.01
// 
using Cysharp.Threading.Tasks;
using Ling.Adv;
using Ling.MasterData.Stage;
using Ling.Scenes.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using Zenject;


namespace Ling.Chara
{
	/// <summary>
	/// Player, Enemy等のキャラ全般を扱うPresenter
	/// </summary>
	public class CharaManager : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private PlayerControlGroup _playerControlGroup = default;
		[SerializeField] private List<EnemyControlGroup> _enemyControlGroups = default;

		[Inject] private Utility.IEventManager _eventManager = default;
		[Inject] private DiContainer _diContainer = default;

		private bool _isInitialized = false;    // 初期化済みであればtrue
		private StageMaster _stageMaster = null;
		private SortedDictionary<int, EnemyControlGroup> _enemyControlDict = new SortedDictionary<int, EnemyControlGroup>();

		#endregion


		#region プロパティ

		/// <summary>
		/// プレイヤー情報
		/// </summary>
		public PlayerControlGroup PlayerControlGroup => _playerControlGroup;

		/// <summary>
		/// 敵のModel情報。階層ごとにModelGroupが分かれている
		/// </summary>
		public SortedDictionary<int, EnemyControlGroup> EnemyControlDict => _enemyControlDict;

		/// <summary>
		/// PlayerControl
		/// </summary>
		public Chara.PlayerControl Player => PlayerControlGroup.Player;

		/// <summary>
		/// PlayerModel
		/// </summary>
		public Chara.PlayerModel PlayerModel => Player.Model;

		/// <summary>
		/// PlayerView
		/// </summary>
		public Chara.PlayerView PlayerView => Player.View;

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 必要な初期設定を行う
		/// </summary>
		public async UniTask InitializeAsync()
		{
			// 既に初期化済みであれば何もしない
			if (_isInitialized) return;

			// プレイヤー情報の生成
			await PlayerControlGroup.SetupAsync();

			foreach (var enemyControlGroup in _enemyControlGroups)
			{
				// プール情報から敵モデルを生成する
				await enemyControlGroup.SetupAsync();
			}

			_isInitialized = true;
		}

		public void SetStageMaster(StageMaster stageMaster)
		{
			_stageMaster = stageMaster;
		}

		/// <summary>
		/// 一度すべての情報を削除する
		/// </summary>
		public void Refresh()
		{
			// Playerの状態を元に戻す

			// すべてのプールを戻す
			foreach (var enemyControlGroup in _enemyControlGroups)
			{
				enemyControlGroup.ReturnViewAll();
			}

			_isInitialized = true;
		}

		/// <summary>
		/// Playerが移動した上昇をもとに戻す
		/// </summary>
		public void ResetPlayerUpPosition()
		{
			var localPos = PlayerView.transform.localPosition;
			PlayerView.transform.localPosition = new Vector3(localPos.x, localPos.y, 0f);
		}

		/// <summary>
		/// 次のレベルに移動する
		/// </summary>
		public void ChangeNextLevel(UnityEngine.Tilemaps.Tilemap tilemap, int level)
		{
			// 座標をもとに戻す
			ResetPlayerUpPosition();

			PlayerModel.SetMapLevel(level);

			// 移動後のTilemapをPlayerに登録し直す
			PlayerView.SetTilemap(tilemap, level);
		}

		/// <summary>
		/// 指定レベルの敵を作成する
		/// </summary>
		/// <param name="level"></param>
		public async UniTask<EnemyControlGroup> BuildEnemyGroupAsync(int level, Map.GroundTilemap groundTilemap)
		{
			ResetEnemyGroup(level);

			var mapMaster = _stageMaster.GetMapMasterByLevel(level);
			if (mapMaster == null)
			{
				Utility.Log.Error($"指定したMapMasterがない Level:{level}");
				return null;
			}

			var enemyControlGroup = groundTilemap.EnemyControlGroup;

			enemyControlGroup.Startup(mapMaster);

			_enemyControlDict.Add(level, enemyControlGroup);

			return enemyControlGroup;
		}

		/// <summary>
		/// 指定したレベルの<see cref="EnemyControlGroup"/>を削除する
		/// </summary>
		public void ResetEnemyGroup(int level)
		{
			var enemyGroup = FindEnemyGroup(level);
			if (enemyGroup == null) return;

			enemyGroup.Reset();

			// Dictionaryから指定したEnemyControlを削除
			_enemyControlDict.Remove(level);
		}

		/// <summary>
		/// 敵一体を作成する
		/// </summary>
		/// <returns></returns>
		public CharaModel CreateEnemy()
		{
			return null;
		}

		/// <summary>
		/// 敵Controlを返す
		/// </summary>
		public EnemyControl FindEnemyByModel(CharaModel model)
		{
			foreach (var enemyControlGroup in _enemyControlGroups)
			{
				var enemy = enemyControlGroup.FindEnemyByModel(model);
				if (enemy != null)
				{
					return enemy;
				}
			}

			return null;
		}

		/// <summary>
		/// 指定座標に何かしらのキャラクターが存在するか
		/// </summary>
		public bool ExistsCharaInPos(int level, in Vector2Int pos)
		{
			if (PlayerControlGroup.ExistsCharaInPos(pos)) return true;

			if (_enemyControlDict.TryGetValue(level, out var value))
			{
				if (value.ExistsCharaInPos(pos)) return true;
			}

			return false;
		}

		/// <summary>
		/// 指定階層のEnemyControlGroupを検索
		/// </summary>
		public EnemyControlGroup FindEnemyControlGroup(int level) =>
			_enemyControlDict[level];

		/// <summary>
		/// すべてのキャラの移動Processを実効する
		/// </summary>
		public void ExecuteMoveProcesses()
		{
			_playerControlGroup.ExecuteMoveProcesses();
			
			foreach (var enemyControlGroup in _enemyControlGroups)
			{
				enemyControlGroup.ExecuteMoveProcesses();
			}
		}

		/// <summary>
		/// 移動プロセスがすべて終了するまで待機する
		/// </summary>
		/// <returns></returns>
		public async UniTask WaitForMoveProcessAsync()
		{
			await _playerControlGroup.WaitForMoveProcessAsync();
			
			foreach (var enemyControlGroup in _enemyControlGroups)
			{
				await enemyControlGroup.WaitForMoveProcessAsync();
			}
		}

		#endregion


		#region private 関数

		/// <summary>
		/// 指定階層のEnemyControlGroupを検索する
		/// </summary>
		private EnemyControlGroup FindEnemyGroup(int level)
		{
			_enemyControlDict.TryGetValue(level, out var enemyControlGroup);

			return enemyControlGroup;
		}

		#endregion


		#region MonoBegaviour

		private void Awake()
		{
			// マップが削除されたとき敵をプールに戻す
			_eventManager.Add<Map.EventRemoveMap>(this, 
				_ev => 
				{
					ResetEnemyGroup(_ev.level);
				});
		}

		private void OnDestroy()
		{
			_eventManager.RemoveAll(this);
		}

		#endregion
	}
}