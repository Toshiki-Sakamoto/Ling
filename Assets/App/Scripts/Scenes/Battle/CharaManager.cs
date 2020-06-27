// 
// CharaManager.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.05.01
// 
using Ling.Adv;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;


namespace Ling.Scenes.Battle
{
	/// <summary>
	/// 
	/// </summary>
	public class CharaManager : Utility.MonoSingleton<CharaManager>
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private Chara.PlayerFactory _playerFactory = null;

		[Inject] private MapManager _mapManager = null;
		[Inject] private Utility.IEventManager _eventManager = null;

		#endregion


		#region プロパティ

		public Chara.Player Player { get; private set; }

		#endregion


		#region public, protected 関数

		/// <summary>
		/// プレイヤーを作成する
		/// プレイヤーが生成済みなら何もしない
		/// </summary>
		/// <returns></returns>
		public Chara.Player CreatePlayer()
		{
			if (Player != null) return Player;

			Player = _playerFactory.Create();
			Player.SetTilemap(_mapManager.CurrentTilemap);

			return Player;
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

		/// <summary>
		/// 初期処理
		/// </summary>
		protected override void Awake()
		{
			base.Awake();
		}

		/// <summary>
		/// 更新前処理
		/// </summary>
		void Start()
		{
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		void Update()
		{
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		void OnDestoroy()
		{
			_eventManager.RemoveAll(this);
		}

		#endregion
	}
}