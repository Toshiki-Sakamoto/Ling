// 
// StatusScene.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.11.02
// 

using UnityEngine;
using Zenject;
using UniRx;

namespace Ling.Scenes.Status
{
	/// <summary>
	/// Status UI Scene
	/// </summary>
	/// <remark>
	/// プレイヤーのHPやアイテムなどのUI
	/// </reamsk>
	public class StatusScene : Common.Scene.Base
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[SerializeField] private StatusView _view = default;

		[Inject] private Chara.CharaManager _charaManager = default;

		private Chara.PlayerControl _player;

		#endregion


		#region プロパティ

		public override Common.Scene.SceneID SceneID => Common.Scene.SceneID.Status;

		#endregion


		#region public, protected 関数

		/// <summary>
		/// シーンが開始される時
		/// </summary>
		public override void StartScene()
		{
			_player = _charaManager.Player;

			if (!_player.IsSetuped)
			{
				// Playerの準備が整った時に通知を受ける
				_player.OnSetuped
					.Subscribe(_ => {}, onCompleted: () => Setup());
			}
			else
			{
				Setup();
			}
		}

		#endregion


		#region private 関数

		private void Setup()
		{
			_player.Status.HP
				.AsObservable()
				.Subscribe(hp_ =>
				{
					_view.HP.SetHP(hp_);
				});

			// Viewのセットアップ
			_view.HP.Setup(_player.Status.HP.Value);
		}

		#endregion


		#region MonoBegaviour


		#endregion
	}
}