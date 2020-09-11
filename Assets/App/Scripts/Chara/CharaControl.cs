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
using Zenject;
using Ling.Map.TileDataMapExtensions;
using Ling.Const;

namespace Ling.Chara
{
	/// <summary>
	/// 簡易Controller参照用インターフェース
	/// </summary>
	public interface ICharaController
	{
		CharaModel Model { get; }
		
		ViewBase View { get; }

		TProcess AddMoveProcess<TProcess>() where TProcess : Utility.ProcessBase, new();
		TProcess AddAttackProcess<TProcess>() where TProcess : Utility.ProcessBase, new();
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

		[Inject] private DiContainer _diContainer = default;

		private TModel _model = default;
		private List<Utility.ProcessBase> _moveProcesses = new List<Utility.ProcessBase>();
		private List<Utility.ProcessBase> _attackProcess = new List<Utility.ProcessBase>();

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
		/// 初期座標設定
		/// </summary>
		public void InitPos(in Vector2Int pos)
		{
			_model.InitPos(pos);
			_view.SetCellPos(pos);

			// マップ側に伝える
		}

		/// <summary>
		/// どういう行動をするか攻撃、移動AIクラスから思考し、決定する。
		/// </summary>
		public async UniTask ThinkAIProcess(Utility.Async.WorkTimeAwaiter timeAwaiter)
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
			var moveAI = _diContainer.InstantiateComponent<TMoveAI>(gameObject);
			_model.SetMoveAI(moveAI);

			return moveAI;
		}

		public TAttackAI AttachAttackAI<TAttackAI>() where TAttackAI : AI.Attack.AIBase
		{
			var attackAI = _diContainer.InstantiateComponent<TAttackAI>(gameObject);
			_model.SetAttackAI(attackAI);

			return attackAI;
		}

		/// <summary>
		/// 移動プロセスの追加
		/// 実行は待機する
		/// </summary>
		public TProcess AddMoveProcess<TProcess>() where TProcess : Utility.ProcessBase, new()
		{
			var process = this.AttachProcess<TProcess>(waitForStart: true);
			_moveProcesses.Add(process);

			return process;
		}

		/// <summary>
		/// 攻撃プロセスの追加
		/// 実行は待機する
		/// </summary>
		public TProcess AddAttackProcess<TProcess>() where TProcess : Utility.ProcessBase, new()
		{
			var process = this.AttachProcess<TProcess>(waitForStart: true);
			_attackProcess.Add(process);

			return process;
		}

		/// <summary>
		/// 移動プロセスの実行
		/// </summary>
		public void ExecuteMoveProcess()
		{
			foreach (var process in _moveProcesses)
			{
				// 終了時、移動プロセスリストから削除する
				process.AddAllFinishAction(action_ => 
					{
						_moveProcesses.Remove(action_);
					});

				process.SetEnable(true);
			}
		}

		/// <summary>
		/// すべての移動プロセスが終了したか
		/// </summary>
		public bool IsMoveAllProcessEnded()
		{
			// 終わったものは自動で削除されるので存在だけ確認
			return _moveProcesses.Count == 0;
		}

		/// <summary>
		/// 攻撃プロセスの実行
		/// </summary>
		public void ExecuteAttackProcess()
		{
			foreach (var process in _attackProcess)
			{
				process.SetEnable(true);
			}
		}

		/// <summary>
		/// 指定した座標に移動できるか
		/// </summary>
		public bool CanMove(Map.TileDataMap tileDataMap, in Vector2Int addMoveDir)
		{
			// 目的地
			var destPos = _model.Pos + addMoveDir;

			// 範囲外なら移動できない
			if (!tileDataMap.InRange(destPos.x, destPos.y))
			{
				return false;
			}

			var tileFlag = tileDataMap.GetTileFlag(destPos.x, destPos.y);

			// 移動できないフラグ
			if (tileFlag.HasAny(_model.UnmovableTileFlag))
			{
				return false;
			}

			return true;		
		}

		#endregion


		#region private 関数


		#endregion


		#region MonoBegaviour

		#endregion
	}
}