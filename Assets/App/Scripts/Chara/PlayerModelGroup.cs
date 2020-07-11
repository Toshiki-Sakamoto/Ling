//
// PlayerModelGroup.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.10
//

using Cysharp.Threading.Tasks;
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
	/// 現在のPlayer＋仲間の情報を持つ
	/// </summary>
	public class PlayerModelGroup : ModelGroupBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		/// <summary>
		/// Player
		/// </summary>
		public CharaModel Player { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		protected override async UniTask SetupAsyncInternal()
		{
			// プレイヤーが未生成ならば生成する
			if (Player == null)
			{
				// プレイヤー情報を読み込む

				Player = CreateModel();
				Player.Setup();
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
