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

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public override void Awake()
		{
		}

		public override void Init() 
		{
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

			if (moveDir != Vector3Int.zero)
			{
				var eventPlayerMove = GameManager.Instance.EventHolder.PlayerMove;

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
