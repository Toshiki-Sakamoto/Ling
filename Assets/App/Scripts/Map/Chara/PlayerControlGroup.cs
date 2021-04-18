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
	public class PlayerControlGroup : ControlGroupBase<PlayerControl, PlayerModel, PlayerView>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private PlayerControl _player = default;

		[Inject] private DiContainer _diContainer;

		#endregion


		#region プロパティ

		/// <summary>
		/// Player Control
		/// </summary>
		public PlayerControl Player => _player;

		public PlayerModel PlayerModel => Player.Model;

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		protected override async UniTask SetupAsyncInternal()
		{
			// プレイヤーが未生成ならば生成する
			var model = Player.Model;
			var param = new CharaModel.Param();
			param.charaType = CharaType.Player;

			model.Setup(param);
			model.SetStatus(new CharaStatus(5));
			model.SetMapLevel(1);

			Player.Setup();

			Controls.Add(Player);
			Models.Add(model);
		}

		/// <summary>
		/// 指定座標にキャラクターが存在するか
		/// </summary>
		public bool ExistsCharaInPos(Vector2Int pos)
		{
			if (PlayerModel.CellPosition.Value == pos) return true;
			return Models.Exists(model => model.CellPosition.Value == pos);
		}

		#endregion


		#region private 関数

		#endregion
	}
}
