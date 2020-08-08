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

		[SerializeField] private CharaView _view = default;

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
		public Chara.Player Player => _view.Player;

		/// <summary>
		/// 敵のモデルプール管理者
		/// </summary>
		public Chara.EnemyPoolManager EnemyPoolManager => _view.EnemyPoolManager;

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

			SetupCharaView(Player, PlayerModelGroup.Player);

			// プール情報から敵モデルを生成する
			await EnemyPoolManager.CreateObjectsAsync();
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

			// CharaModelが削除されるときに一緒に削除する
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
		public async UniTask<EnemyModelGroup> BuildEnemyGroupAsync(int level, Tilemap tilemap)
		{
			RemoveEnemyGroup(level);

			var mapMaster = _stageMaster.GetMapMasterByLevel(level);
			if (mapMaster == null)
			{
				Utility.Log.Error($"指定したMapMasterがない Level:{level}");
				return null;
			}

			var enemyModelGroup = new EnemyModelGroup();
			enemyModelGroup.SetMapMaster(mapMaster);

			await enemyModelGroup.SetupAsync();

			// ViewとModelを結びつける
			foreach (var model in enemyModelGroup.Models)
			{
				SetupCharaView(_view.GetEnemyByPool(model), model);
			}

			EnemyModelGroups.Add(level, enemyModelGroup);

			return enemyModelGroup;
		}

		/// <summary>
		/// 指定したレベルの<see cref="EnemyModelGroup"/>を削除する
		/// </summary>
		public void RemoveEnemyGroup(int level)
		{
			var enemyGroup = FindEnemyGroup(level);
			if (enemyGroup == null) return;

			// 敵をViewから取り除く
			foreach (var enemy in enemyGroup.Models)
			{
				_view.RemoveChara(enemy);
			}

			enemyGroup.OnDestroy();

			EnemyModelGroups.Remove(level);
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
		/// 敵Viewを返す
		/// </summary>
		public Enemy FindEnemyView(CharaModel model) =>
			_view.FindEnemy(model);

		/// <summary>
		/// 指定座標に何かしらのキャラクターが存在するか
		/// </summary>
		public bool ExistsCharaInPos(int level, in Vector2Int pos)
		{
			if (PlayerModelGroup.ExistsCharaInPos(pos)) return true;

			if (EnemyModelGroups.TryGetValue(level, out var value))
			{
				if (value.ExistsCharaInPos(pos)) return true;
			}

			return false;
		}

		#endregion


		#region private 関数

		private EnemyModelGroup FindEnemyGroup(int level) =>
			EnemyModelGroups.FirstOrDefault(pair => pair.Key == level).Value;

		#endregion


		#region MonoBegaviour

		private void Awake()
		{
			// マップが削除されたとき敵をプールに戻す
			_eventManager.Add<EventRemoveMap>(this, 
				_ev => 
				{
					RemoveEnemyGroup(_ev.level);
				});
		}

		private void OnDestroy()
		{
			_eventManager.RemoveAll(this);
		}

		#endregion
	}
}