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
using Ling.Common.Process;
using Ling.Common.Input;
using UnityEngine.InputSystem;

using Zenject;

namespace Ling.Scenes.Battle.Phase
{
	/// <summary>
	/// プレイヤーの行動開始
	/// </summary>
	public class BattlePhasePlayerAction : BattlePhaseBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Chara.CharaManager _charaManager;
		private Map.MapManager _mapManager;
		private IInputProvider<InputControls.IMoveActions> _moveInputProvider;
		private IInputProvider<InputControls.IActionActions> _actionInputProvider;
		private Dictionary<InputAction, System.Func<bool>> _inputActionDict = new Dictionary<InputAction, System.Func<bool>>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		protected override void AwakeInternal()
		{
			_charaManager = Resolve<Chara.CharaManager>();
			_mapManager = Resolve<Map.MapManager>();

			var inputManager = Resolve<Common.Input.IInputManager>();
			_moveInputProvider = inputManager.Resolve<InputControls.IMoveActions>();
			_actionInputProvider = inputManager.Resolve<InputControls.IActionActions>();
		}

		public override void Init()
		{
			// マップ情報を更新する
			_mapManager.UpdateMapData();

			// キーが押されたときの処理

/*
			// 移動
			var move = _moveInputProvider.Controls.Move;
			_inputActionDict.Add(move.Left, () => MoveCommand(new Vector2Int(-1, 0)));
			_inputActionDict.Add(move.LeftUp, () => MoveCommand(new Vector2Int(-1, 1)));
			_inputActionDict.Add(move.LeftDown, () => MoveCommand(new Vector2Int(-1, -1)));
			_inputActionDict.Add(move.Right, () => MoveCommand(new Vector2Int(1, 0)));
			_inputActionDict.Add(move.RightUp, () => MoveCommand(new Vector2Int(1, 1)));
			_inputActionDict.Add(move.RightDown, () => MoveCommand(new Vector2Int(1, -1)));
			_inputActionDict.Add(move.Up, () => MoveCommand(new Vector2Int(0, 1)));
			_inputActionDict.Add(move.Down, () => MoveCommand(new Vector2Int(0, -1)));
*/
			// 攻撃
			var action = _actionInputProvider.Controls.Action;
//			_inputActionDict.Add(action.Attack, () => Attack());

			// メニューを開く
			_inputActionDict.Add(action.Menu, () => Menu());

			KeyCommandProcess();
		}

		public override void Proc()
		{
			KeyCommandProcess();
		}

		public override void Term()
		{
			_inputActionDict.Clear();
		}

		#endregion


		#region private 関数

		/// <summary>
		/// 移動コマンド
		/// </summary>
		/// <param name="moveDistance"></param>
		private bool MoveCommand(Vector2Int moveDistance)
		{
			if (moveDistance == Vector2Int.zero) return false;

			var playerModel = _charaManager.PlayerModel;
			var player = _charaManager.Player;

			// 攻撃できるか

			// 移動できるか
			if (!_mapManager.CanMoveChara(playerModel, moveDistance))
			{
				// 向きだけ変える
				playerModel.SetDirection(moveDistance);
				return false;
			}
			// 移動であればプロセスを追加し、敵思考に回す
			var moveProcess = player.AddMoveProcess<Chara.Process.ProcessMove>();
			moveProcess.SetAddPos(player, playerModel.CellPosition.Value, moveDistance);

			// Playerの座標を変更する(見た目は反映させない)
			playerModel.AddCellPosition(moveDistance, reactive: false);

			Change(BattleScene.Phase.EnemyTink);

			//var process = _processManager.Attach<Process.ProcessPlayerMoveStart>().Setup(moveDistance);

			// Player行動中に遷移
			//var argument = new BattlePhasePlayerActionProcess.Argument { process = process };
			//Change(BattleScene.Phase.PlayerActionProcess, argument);

			// 移動したイベントを投げる
			var eventPlayerMove = _gameManager.EventHolder.PlayerMove;
			eventPlayerMove.moveDistance = new Vector3Int(moveDistance.x, moveDistance.y, 0);

			return true;
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

		/// <summary>
		/// 通常攻撃
		/// </summary>
		private bool Attack()
		{
			// 攻撃対象がいるかどうか関わらず攻撃に移行する
			Change(BattleScene.Phase.PlayerAttack);

			return true; 
		}

		/// <summary>
		/// メニューを開く
		/// </summary>
		private bool Menu()
		{
			Change(BattleScene.Phase.MenuAction);

			return true;
		}

		/// <summary>
		/// 移動
		/// </summary>
		private void Move(Vector2Int move)
		{
			if (move == Vector2Int.zero) return;

			MoveCommand(move);

			var eventPlayerMove = _gameManager.EventHolder.PlayerMove;
			eventPlayerMove.moveDistance = new Vector3Int(move.x, move.y, 0);
		}


		private void KeyCommandProcess()
		{
			// 先頭から入力を確認する
			foreach (var pair in _inputActionDict)
			{
				if (!pair.Key.IsPressed()) continue;

				// 処理されたら終了
				if (pair.Value.Invoke()) return;
			}

			/*
			// x, y の入力
			// 関連付けはInput Managerで行っている
			var moveDir = Vector2Int.zero;

			if (Input.GetKey(KeyCode.A))
			{
				// 通常攻撃
				Attack();
			}
			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				moveDir = Vector2Int.left;
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				moveDir = Vector2Int.right;
			}
			else if (Input.GetKey(KeyCode.UpArrow))
			{
				moveDir = Vector2Int.up;
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				moveDir = Vector2Int.down;
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

			if (moveDir != Vector2Int.zero)
			{
				MoveCommand(moveDir);

				var eventPlayerMove = _gameManager.EventHolder.PlayerMove;

				eventPlayerMove.moveDistance = new Vector3Int(moveDir.x, moveDir.y, 0);
			}*/
		}

		#endregion
	}
}
