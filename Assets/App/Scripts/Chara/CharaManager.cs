// 
// CharaManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.01
// 
using Cysharp.Threading.Tasks;
using Ling.Adv;
using Ling.MasterData.Stage;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
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

		[SerializeField] private Player _player = null;
		[SerializeField] private Chara.EnemyPoolManager _enemyPoolManager = null;

		[Inject] private Utility.IEventManager _eventManager = null;

		private bool _isInitialized = false;    // 初期化済みであればtrue
		private StageMaster _stageMaster = null;

		#endregion


		#region プロパティ

		/// <summary>
		/// プレイヤー情報
		/// </summary>
		public PlayerModelGroup PlayerModelGroup { get; } = new PlayerModelGroup();

		/// <summary>
		/// 敵のModel情報。階層ごとにModelGroupが分かれている
		/// </summary>
		public Dictionary<int, EnemyModelGroup> EnemyModelGroups { get; } = new Dictionary<int, EnemyModelGroup>();

		/// <summary>
		/// PlayerView
		/// </summary>
		public Chara.Player Player => _player;

		/// <summary>
		/// 敵のモデルプール管理者
		/// </summary>
		public Chara.EnemyPoolManager EnemyPoolManager => _enemyPoolManager;

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
			await PlayerModelGroup.SetupAsync();

			SetupCharaView(_player, PlayerModelGroup.Player);

			// プール情報から敵モデルを生成する
			await _enemyPoolManager.CreateObjectsAsync();
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
			EnemyPoolManager.ReturnAllItems();

			_isInitialized = true;
		}

		/// <summary>
		/// ModelとViewを紐づける
		/// </summary>
		public void SetupCharaView(Chara.Base chara, CharaModel model)
		{
			var status = model.Status;
			chara.Setup(status);
		}

		public Vector3Int GetPlayerCellPos() =>
			Player.CellPos;

		/// <summary>
		/// Playerが移動した上昇をもとに戻す
		/// </summary>
		public void ResetPlayerUpPosition()
		{
			var localPos = Player.transform.localPosition;
			Player.transform.localPosition = new Vector3(localPos.x, localPos.y, 0f);
		}

		/// <summary>
		/// 次のレベルに移動する
		/// </summary>
		public void ChangeNextLevel(UnityEngine.Tilemaps.Tilemap tilemap)
		{
			// 座標をもとに戻す
			ResetPlayerUpPosition();

			// 移動後のTilemapをPlayerに登録し直す
			Player.SetTilemap(tilemap);
		}

		/// <summary>
		/// 指定レベルの敵を作成する
		/// </summary>
		/// <param name="level"></param>
		public void BuildEnemyGroup(int level)
		{
			RemoveEnemyGroup(level);

			var mapMaster = _stageMaster.GetMapMasterByLevel(level);
			if (mapMaster == null)
			{
				Utility.Log.Error($"指定したMapMasterがない Level:{level}");
				return;
			}

			var enemyModelGroup = new EnemyModelGroup();


			EnemyModelGroups.Add(level, enemyModelGroup);
		}

		/// <summary>
		/// 指定したレベルの<see cref="EnemyModelGroup"/>を削除する
		/// </summary>
		public void RemoveEnemyGroup(int level)
		{
			var enemyGroup = FindEnemyGroup(level);
			if (enemyGroup == null) return;

			enemyGroup.OnDestroy();

			EnemyModelGroups.Remove(level);
		}

		#endregion


		#region private 関数

		private EnemyModelGroup FindEnemyGroup(int level) =>
			EnemyModelGroups.FirstOrDefault(pair => pair.Key == level).Value;

		#endregion


		#region MonoBegaviour

		#endregion
	}
}