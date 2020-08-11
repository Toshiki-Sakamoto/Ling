﻿// 
// CharaControl.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.10
// 

using UnityEngine;
using System;
using System.Linq;
using UniRx;

namespace Ling.Chara
{
	/// <summary>
	/// 簡易Controller参照用インターフェース
	/// </summary>
	public interface ICharaController
	{
		CharaModel Model { get; }
		
		ViewBase View { get; }
	}

	/// <summary>
	/// キャラのModelとViewをつなげる役目と操作を行う
	/// </summary>
	public abstract class CharaControl<TModel, TView> : MonoBehaviour, ICharaController
		where TModel : CharaModel
		where TView : ViewBase
    {
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

        [SerializeField] private CharaStatus _status = default;
		[SerializeField] private TView _view = default;

		private TModel _model = default;

		#endregion


		#region プロパティ

		
		public TModel Model => _model;

		public TView View => _view;

		// ICharaController
		CharaModel ICharaController.Model => _model;
		ViewBase ICharaController.View => _view;

		#endregion


		#region public, protected 関数

		public void Setup(TModel model)
		{
			_model = model;
			_status = model.Status;

            // 死亡時
            _status.IsDead.Where(isDead_ => isDead_)
                .Subscribe(_ =>
                {
					// Viewにも伝える
					Utility.Log.Print("死んだ！");
                });
		}

		/// <summary>
		/// どういう行動をするか攻撃、移動AIクラスから思考し、決定する。
		/// </summary>
		public async UniTask ThinkAIProcess()
		{
			// 自分が状態異常で行動できない場合はスキップ

			// 第一優先として、自分が「特技」「攻撃」ができるか。

			// それができない場合、「移動」をする。
		}

		#endregion


		#region private 関数


		#endregion


		#region MonoBegaviour

		#endregion
	}
}