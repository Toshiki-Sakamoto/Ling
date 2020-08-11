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

namespace Ling.Chara
{
	/// <summary>
	/// 現在のPlayer＋仲間の情報を持つ
	/// </summary>
	public class PlayerControlGroup : ControlGroupBase<PlayerControl, PlayerModel, PlayerView>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		/// <summary>
		/// Player Control
		/// </summary>
		public CharaControl<PlayerModel, PlayerView> Player { get; private set; }

		//public Player Player => _player;

		public PlayerModel PlayerModel { get; private set; }

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

				PlayerModel = new PlayerModel();
				var param = new CharaModel.Param();
				param.charaType = CharaType.Player;

				PlayerModel.Setup(param);
				PlayerModel.SetStatus(new CharaStatus(100));

				//_playerControl = PlayerControl.Create(PlayerControlGroup.Player, PlayerView);
			}
		}

		/// <summary>
		/// 指定座標にキャラクターが存在するか
		/// </summary>
		public bool ExistsCharaInPos(Vector2Int pos)
		{
			if (PlayerModel.Pos == pos) return true;
			return Models.Exists(model => model.Pos == pos);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
