// 
// CharaManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.01
// 
using Cysharp.Threading.Tasks;
using Ling.Adv;
using System.Collections;
using System.Collections.Generic;
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

		#endregion


		#region プロパティ

		/// <summary>
		/// プレイヤー情報
		/// </summary>
		public PlayerModelGroup PlayerModelGroup { get; } = new PlayerModelGroup();

		/// <summary>
		/// 敵のModel情報。階層ごとにModelGroupが分かれている
		/// </summary>
		public List<EnemyModelGroup> EnemyModelGroups { get; } = new List<EnemyModelGroup>();

		/// <summary>
		/// PlayerView
		/// </summary>
		public Chara.Player Player => _player;

		public Chara.EnemyManager EnemyManager { get; } = new Chara.EnemyManager();

		/// <summary>
		/// 敵のモデルプール管理者
		/// </summary>
		public Chara.EnemyPoolManager EnemyPoolManager => _enemyPoolManager;

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 必要な初期設定を行う
		/// </summary>
		public async UniTask SetupAsync()
		{
			// 既に初期化済みであれば何もしない
			if (_isInitialized) return;

			// プレイヤー情報の生成
			await PlayerModelGroup.SetupAsync();

			SetupCharaView(_player, PlayerModelGroup.Player);

			// プール情報から敵モデルを生成する
			await _enemyPoolManager.CreateObjectsAsync();
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
			//Player = _playerFactory.Create();
			//Player.SetTilemap(_mapManager.CurrentTilemap);

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

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}