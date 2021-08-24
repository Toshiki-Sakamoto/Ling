// 
// BattleDebugSkillTest.cs  
// ProductName Ling
//  
// Created by  on 2021.08.14
// 

using Ling.Common.Skill;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using System;

namespace Ling.Scenes.Battle._Debug
{
	/// <summary>
	/// デバッグスキルテスト
	/// </summary>
	public class BattleDebugSkillTest : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		[Inject] private Utility.DebugConfig.DebugConfigManager _debugManager;
		[Inject] private Map.MapManager _mapManager = null;

		[SerializeField] private InputField _xStartInput;
		[SerializeField] private InputField _yStartInput;
		[SerializeField] private InputField _xEndInput;
		[SerializeField] private InputField _yEndInput;
		[SerializeField] private Transform _root;
		[SerializeField] private EffectPlayer _player;
		[SerializeField] private Button _playButton;

		private int _xStartPos, _yStartPos;
		private int _xEndPos, _yEndPos;

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		public void PlaySkill(EffectPlayer player)
		{
			var trans = player.GetCustom<Utility.Timeline.TransformTweenSetter>();

			// 座標から取得する
			var startPos = _mapManager.CurrentTilemap.GetCellCenterWorld(new Vector3Int(_xStartPos, _yStartPos, 0));
			trans.SetStartPosition(startPos);

			var endPos = _mapManager.CurrentTilemap.GetCellCenterWorld(new Vector3Int(_xEndPos, _yEndPos, 0));
			trans.SetEndPosition(endPos);

			// デバッグを閉じてから再生
			_debugManager.Close();

			// 0.2秒後に再生
			Observable.Timer(TimeSpan.FromSeconds(1))
				.Subscribe(_ =>
				{
					player.Play();
				});
		}

        #endregion


        #region private 関数

        #endregion


        #region MonoBegaviour

        private void Awake()
        {
			// root取得
			_root = _mapManager.MapView.EffectRoot;

			_playButton.OnClickAsObservable()
				.Subscribe(_ =>
				{
					if (_player == null)
					{
						Utility.Log.Error("エフェクトが選択されていません");
						return;
					}

					// 現在のスキルを再生させる
					var instance = Instantiate(_player, _root);
					PlaySkill(instance);
				}).AddTo(this);

			_xStartInput.OnEndEditAsObservable()
				.Subscribe(txt =>
				{
					if (!int.TryParse(txt, out var result)) return;
					_xStartPos = result;
				});

			_yStartInput.OnEndEditAsObservable()
				.Subscribe(txt =>
				{
					if (!int.TryParse(txt, out var result)) return;
					_yStartPos = result;
				});

			_xEndInput.OnEndEditAsObservable()
				.Subscribe(txt =>
				{
					if (!int.TryParse(txt, out var result)) return;
					_xEndPos = result;
				});

			_yEndInput.OnEndEditAsObservable()
				.Subscribe(txt =>
				{
					if (!int.TryParse(txt, out var result)) return;
					_yEndPos = result;
				});
		}

        #endregion
    }
}