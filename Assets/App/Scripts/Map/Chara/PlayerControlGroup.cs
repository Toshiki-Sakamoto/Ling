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

		[SerializeField, ES3NonSerializable] private PlayerControl _player = default;
		[ES3Serializable] private int aaaa;

		[Inject] private DiContainer _diContainer;
		[Inject] private Utility.IEventManager _eventManager;

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

		public async UniTask SetupAsync(CharaStatus status)
		{
			await base.SetupAsync();
			
			// プレイヤーが未生成ならば生成する
			// todo: Player情報は外から生成すること
			var model = Player.Model;
			var param = new CharaModel.Param();
			param.charaType = CharaType.Player;

			model.Setup(param);

			//var status = new CharaStatus(5, 100, 5, 0);
			model.SetStatus(status);
			status.Setup();

			Player.Setup();

			Controls.Add(Player);
			Models.Add(model);
		}

		// <summary>
		/// 復帰時の処理
		/// </summary>
		public async override UniTask ResumeAsync()
		{
			// Playerを読み込む
			if (_saveDataHelper.Exists("Chara/Player.save", "Player"))
			{
				_saveDataHelper.Load("Chara/Player.save", "Player");
			}

			// ControlのModelを入れ込む
			Models.Add(Player.Model);
				
			foreach (var model in Models)
			{
				model.Status.Setup();
			}
			
			Player.Setup();
			
			Controls.Add(Player);
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

		private void Awake()
		{
			_eventManager.Add<Utility.SaveData.EventSaveCall>(this, ev =>
				{
					// PlayerModelを保存する
					_saveDataHelper.Save("Chara/Player.save", "Player", _player.gameObject);
				});
			
			_eventManager.Add<Utility.SaveData.EventLoadCall>(this, ev =>
				{
					ev.Add(UniTask.Create((async () =>
						{
							/*
							// PlayerModel情報
							if (_saveDataHelper.Exists("Chara/Player.save", "Player"))
							{
								_saveDataHelper.Load("Chara/Player.save", "Player");
							}*/
						})));
				});
		}

		#endregion
	}
}
