// 
// CharaControl.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.10
// 

using UnityEngine;
using System;
using System.Linq;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Ling;

namespace Ling.Chara
{
	/// <summary>
	/// 簡易Controller参照用インターフェース
	/// </summary>
	public interface ICharaController
	{
		CharaModel Model { get; }
		
		ViewBase View { get; }

		TProcess AddActionProcess<TProcess>() where TProcess : Utility.ProcessBase, new();
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
		private List<Utility.ProcessBase> _actionProcesses = new List<Utility.ProcessBase>();

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
		public async UniTask ThinkAIProcess(Utility.Async.TimeAwaiter timeAwaiter)
		{
			// 自分が状態異常で行動できない場合はスキップ

			// 第一優先として、自分が「特技」「攻撃」ができるか。

			// それができない場合、「移動」をする。
			await _model.MoveAI.ExecuteAsync(this, timeAwaiter);
		}

		/// <summary>
		/// AIを設定する
		/// </summary>
		public TMoveAI AttachMoveAI<TMoveAI>() where TMoveAI : AI.Move.AIBase
		{
			var moveAI = gameObject.AddComponent<TMoveAI>();
			_model.SetMoveAI(moveAI);

			return moveAI;
		}

		public TAttackAI AttachAttackAI<TAttackAI>() where TAttackAI : AI.Attack.AIBase
		{
			var attackAI = gameObject.AddComponent<TAttackAI>();
			_model.SetAttackAI(attackAI);

			return attackAI;
		}

		/// <summary>
		/// 行動プロセスの追加
		/// 実行は待機する
		/// </summary>
		public TProcess AddActionProcess<TProcess>() where TProcess : Utility.ProcessBase, new()
		{
			var process = this.AttachProcess<TProcess>(waitForStart: true);
			_actionProcesses.Add(process);

			return process;
		}


		#endregion


		#region private 関数


		#endregion


		#region MonoBegaviour

		#endregion
	}
}