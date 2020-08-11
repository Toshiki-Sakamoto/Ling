//
// ProcessPlayerMoveStart.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.04
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Scenes.Battle.Process
{
	/// <summary>
	/// プレイヤーの移動始め
	/// </summary>
	public class ProcessPlayerMoveStart : Utility.ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private Chara.CharaManager _charaManager = null;

		private Chara.PlayerView _player;
		private Vector3Int _addMoveDir;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		public ProcessPlayerMoveStart Setup(in Vector3Int addMoveDir)
		{
			_player = _charaManager.PlayerView;
			_addMoveDir = addMoveDir;

			// 足元確認コマンドを次に入れる
			SetNext<Process.ProcessPlayerFoot>();

			return this;
		}

		#endregion


		#region public, protected 関数


		#endregion


		#region private 関数

		private void Start()
		{
			//_player
			//	.MoveByAddPos(_addMoveDir)
			//	.Subscribe(_ => ProcessFinish());
		}

		#endregion
	}
}
