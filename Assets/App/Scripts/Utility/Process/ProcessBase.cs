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

namespace Ling.Utility
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class ProcessBase : MonoBehaviour
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public ProcessBase Next { get; private set; }

		/// <summary>
		/// 管理Node
		/// </summary>
		public ProcessNode Node { get; set; }

		/// <summary>
		/// 終了時呼び出される
		/// </summary>
		public System.Action OnFinish { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public TProcess SetNext<TProcess>() where TProcess : ProcessBase
		{
			var process = Node.Attach<TProcess>();

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
		public TProcess SetNextLast<TProcess>() where TProcess : ProcessBase
		{
			if (Next != null)
			{
				return Next.SetNextLast<TProcess>();
			}

			return SetNext<TProcess>();
		}

		/// <summary>
		/// 前のプロセスが終了したときに呼び出される
		/// </summary>
		public virtual void ProcessStart()
		{
		}

		public void ProcessFinish()
		{
			OnFinish?.Invoke();

			// 次に進める
			if (Next != null)
			{
				Next.enabled = true;
				Next.ProcessStart();
			}

			// 終了したものは削除する
			Node.Remove(this);
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
