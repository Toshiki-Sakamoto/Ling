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
		}

		public override void Init()
		{
			// マップ情報を更新する
			_mapManager.UpdateMapData();

#if UNITY_EDITOR
		//	KeyCommandProcess();
#endif
			// 移動
			var move = _moveInputProvider.Controls.Move;
			move.Left.performed += OnLeftClick;
			move.LeftUp.performed += OnLeftUpClick;
			move.LeftDown.performed += OnLeftDownClick;
			move.Down.performed += OnDownClick;
			move.Up.performed += OnUpClick;
			move.Right.performed += OnRightClick;
			move.RightUp.performed += OnRightUpClick;
			move.RightDown.performed += OnRightDownClick;
		}

		public override void Proc()
		{
#if UNITY_EDITOR
		//	KeyCommandProcess();
#endif
		}

		public override void Term() 
		{
			var move = _moveInputProvider.Controls.Move;
			move.Left.performed -= OnLeftClick;
			move.LeftUp.performed -= OnLeftUpClick;
			move.LeftDown.performed -= OnLeftDownClick;
			move.Down.performed -= OnDownClick;
			move.Up.performed -= OnUpClick;
			move.Right.performed -= OnRightClick;
			move.RightUp.performed -= OnRightUpClick;
			move.RightDown.performed -= OnRightDownClick;
		}

		#endregion


		#region private 関数

		private void OnLeftClick(InputAction.CallbackContext callbackContext) =>
			Move(Vector2Int.left);

		private void OnLeftUpClick(InputAction.CallbackContext callbackContext) =>
			Move(new Vector2Int(-1, 1));

		private void OnLeftDownClick(InputAction.CallbackContext callbackContext) =>
			Move(new Vector2Int(-1, -1));

		private void OnDownClick(InputAction.CallbackContext callbackContext) =>
			Move(Vector2Int.down);

		private void OnRightClick(InputAction.CallbackContext callbackContext) =>
			Move(Vector2Int.right);

		private void OnRightUpClick(InputAction.CallbackContext callbackContext) =>
			Move(new Vector2Int(1, 1));

		private void OnRightDownClick(InputAction.CallbackContext callbackContext) =>
			Move(new Vector2Int(1, -1));

		private void OnUpClick(InputAction.CallbackContext callbackContext) =>
			Move(Vector2Int.up);

		/// <summary>
		/// 移動コマンド
		/// </summary>
		/// <param name="moveDistance"></param>
		private void MoveCommand(Vector2Int moveDistance)
		{
			if (moveDistance == Vector2Int.zero) return;

			var playerModel = _charaManager.PlayerModel;
			var player = _charaManager.Player;

			// 攻撃できるか

			// 移動できるか
			if (!_mapManager.CanMoveChara(playerModel, moveDistance))
			{
				// 向きだけ変える
				playerModel.SetDirection(moveDistance);
				return;
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
		private void Attack()
		{
			// 攻撃対象がいるかどうか関わらず攻撃に移行する
			Change(BattleScene.Phase.PlayerAttack);
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


#if UNITY_EDITOR
		private void KeyCommandProcess()
		{
			// 通常攻撃
			var move = _moveInputProvider.Controls.Move;
			var isTest = move.Left.ReadValue<bool>();

			if (isTest)
			{
				Utility.Log.Print("Left Test");
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
#endif

		#endregion
	}
}
