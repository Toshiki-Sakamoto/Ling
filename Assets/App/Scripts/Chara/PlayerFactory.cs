//
// PlayerFactory.cs
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

namespace Ling.Chara
{
	/// <summary>
	/// 
	/// </summary>
	public class PlayerFactory : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private Transform _root = null;
		[SerializeField] private Player _playerPrefab = null;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public Player Create()
		{
			var player = GameObject.Instantiate<Player>(_playerPrefab, _root);
			if (player == null)
			{
				Utility.Log.Assert(false, "Playerの生成に失敗しました");
				return null;
			}

			return player;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
