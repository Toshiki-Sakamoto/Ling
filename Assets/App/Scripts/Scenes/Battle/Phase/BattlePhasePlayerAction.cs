//
// BattlePhasePlayerAction.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.01
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Ling.Utility.Process;

using Zenject;

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// 
	/// </summary>
	public class BattlePhasePlayerAction : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Chara.CharaManager _charaManager;
		private MapManager _mapManager;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void Init()
		{
			_charaManager = Resolve<Chara.CharaManager>();
			_mapManager = Resolve<MapManager>();
		}

		public override void Proc()
		{
#if UNITY_EDITOR
			KeyCommandProcess();
#endif
		}

		public override void Term() 
		{ 
		}

		#endregion


		#region private 関数

		/// <summary>
		/// 移動コマンド
		/// </summary>
		/// <param name="moveDistance"></param>
		private void MoveCommand(Vector3Int moveDistance)
		{
			if (moveDistance == Vector3Int.zero) return;

			var player = _charaManager.Player;

			// 移動できるか
			if (!_mapManager.CanMoveChara(player, moveDistance))
			{
				// 向きだけ変える
				player.SetDirection(moveDistance);
				return;
			}

			var process = _processManager.Attach<Process.ProcessPlayerMoveStart>().Setup(moveDistance);

			// Player行動中に遷移
			var argument = new BattlePhasePlayerActionProcess.Argument { process = process };
			Change(BattleScene.Phase.PlayerActionProcess, argument);
		}

		/// <summary>
		/// 足元確認コマンド
		/// </summary>
		private void FootConfirmCommand()
		{
			var process = _processManager.Attach<Process.ProcessPlayerFoot>();

			var argument = new BattlePhasePlayerActionProcess.Argument { process = process };
			Change(BattleScene.Phase.PlayerActionProcess, argument);
		}


#if UNITY_EDITOR
		private void KeyCommandProcess()
		{
			// x, y の入力
			// 関連付けはInput Managerで行っている
			var moveDir = Vector3Int.zero;

			if (Input.GetKey(KeyCode.LeftArrow))
			{
				moveDir = Vector3Int.left;
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				moveDir = Vector3Int.right;
			}
			else if (Input.GetKey(KeyCode.UpArrow))
			{
				moveDir = Vector3Int.up;
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				moveDir = Vector3Int.down;
			}
			else if (Input.GetKey(KeyCode.Space))
			{
				Change(BattleScene.Phase.Adv);
				return;
			}
			else if (Input.GetKey(KeyCode.Q))
			{
				Change(BattleScene.Phase.NextStage);
				return;
			}
			else if (Input.GetKey(KeyCode.W))
			{
				var eventHolder = _gameManager.EventHolder;
				eventHolder.MessageText.text = "ABCああ<color=#ff0000>あ９</color>９９";

				_eventManager.Trigger<EventMessageText>(eventHolder.MessageText);
				return;
			}

			if (moveDir != Vector3Int.zero)
			{
				MoveCommand(moveDir);

				var eventPlayerMove = _gameManager.EventHolder.PlayerMove;

				eventPlayerMove.moveDistance = new Vector3Int(moveDir.x, moveDir.y, 0);
				//_trsModel.SetDirection(new Vector3(moveDir.x, moveDir.y, 0.0f));

				//var movePos = _trsModel.CellPos + moveDir;

				//_moveList.Add(movePos);

				//StartCoroutine(Move());
			}
		}
#endif

		#endregion
	}
}
