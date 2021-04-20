//
// ProcessBase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.04
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Common
{
	/// <summary>
	/// 一つのタスク
	/// </summary>
	public abstract class ProcessBase
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		// 終了時呼び出される
		public System.Action _onFinish;

		// つながっているすべてのProcessが終了した場合だけ呼び出される
		public System.Action<ProcessBase> _onAllFinish;

		#endregion


		#region private 変数

		[Inject] DiContainer _diContainer;

		#endregion


		#region プロパティ

		public ProcessBase Next { get; private set; }

		/// <summary>
		/// 管理Node
		/// </summary>
		public ProcessNode Node { get; set; }

		/// <summary>
		/// プロセスが有効状態か
		/// false(無効)の場合、更新もされない
		/// </summary>
		public bool Enabled { get; private set; }

		/// <summary>
		/// 開始済みかどうか。(ProcessStartが呼び出された)
		/// </summary>
		public bool IsStarted { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public TProcess SetNext<TProcess>() where TProcess : ProcessBase
		{
			var process = _diContainer.Instantiate<TProcess>();
			Node.Attach<TProcess>(process);
			
			process.SetEnable(false);

			if (Next != null)
			{
				process.Next = Next;
			}

			Next = process;

			return process;
		}


		/// <summary>
		/// そのプロセスが持つ連結リストの一番最後につける
		/// </summary>
		/// <typeparam name="TProcess"></typeparam>
		/// <returns></returns>
		public TProcess SetNextLast<TProcess>() where TProcess : ProcessBase, new()
		{
			if (Next != null)
			{
				return Next.SetNextLast<TProcess>();
			}

			return SetNext<TProcess>();
		}

		public void SetEnable(bool enable)
		{
			Enabled = enable;

			// 有効にしたとき開始してなければスタートさせる
			if (Enabled && !IsStarted)
			{
				ProcessStart();
			}
		}

		/// <summary>
		/// 前のプロセスが終了したときに呼び出される
		/// </summary>
		public void ProcessStart()
		{
			IsStarted = true;
			ProcessStartInternal();
		}

		protected virtual void ProcessStartInternal() { }

		public void ProcessFinish()
		{
			_onFinish?.Invoke();

			// 次に進める
			if (Next != null)
			{
				Next.AddAllFinishAction(_onAllFinish);
				Next.SetEnable(true);
				Next.ProcessStart();
			}
			else
			{
				// 次がないので全て終わり
				_onAllFinish?.Invoke(this);
			}

			// 終了したものは削除する
			Node.Remove(this);
		}

		public void Update()
		{

		}

		public void AddFinishAction(System.Action action)
		{
			_onFinish += action;
		}

		public void AddAllFinishAction(System.Action<ProcessBase> action)
		{
			_onAllFinish += action;
		}

		#endregion


		#region private 関数


		#endregion
	}

#if false
	public abstract class ProcessBase<TParam1> : ProcessBase
	{
		public ProcessBase Setup(TParam1 param1)
		{
			SetupInternal(param1);
			return this;
		}

		protected abstract void SetupInternal(TParam1 param1);
	}

	public abstract class ProcessBase<TParam1, TParam2> : ProcessBase
	{
		public ProcessBase Setup(TParam1 param1, TParam2 param2)
		{
			SetupInternal(param1, param2);
			return this;
		}

		protected abstract void SetupInternal(TParam1 param1, TParam2 param2);
	}

	public abstract class ProcessBase<TParam1, TParam2, TParam3> : ProcessBase
	{
		public ProcessBase Setup(TParam1 param1, TParam2 param2, TParam3 param3)
		{
			SetupInternal(param1, param2, param3);
			return this;
		}

		protected abstract void SetupInternal(TParam1 param1, TParam2 param2, TParam3 param3);
	}
#endif
}
