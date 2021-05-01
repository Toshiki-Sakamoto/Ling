//
// ProcessContainer.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.30
//

using System;
using System.Collections.Generic;
using Utility;
using Utility.Process;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;


namespace Ling.Scenes.Battle.ProcessContainer
{
	public interface INodeExecuter
	{
		UniTask Execute(CancellationToken token);
	}

	public class ProcessNodeExecuter : INodeExecuter
	{
		private ProcessBase _process;

		public ProcessNodeExecuter(ProcessBase process)
		{
			_process = process;
		}

		async UniTask INodeExecuter.Execute(CancellationToken token)
		{
			bool finished = false;
			
			_process.AddAllFinishAction(_ => finished = true);
			_process.SetEnable(true);

			await UniTask.WaitUntil(() => finished);
		}
	}
/*
	public class TaskNodeExecuter : INodeExecuter
	{
		async UniTask INodeExecuter.Execute()
		{
			
		}
	}
	*/

	/// <summary>
	/// プロセスを種類ごとに管理する
	/// </summary>
	public class ProcessContainer<TType>
		where TType : Enum
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Dictionary<TType, List<INodeExecuter>> _nodesDict = new Dictionary<TType, List<INodeExecuter>>();
		private ProcessManager _processManager;
		private GameObject _owner;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Setup(ProcessManager processManager, GameObject owner)
		{
			_processManager = processManager;
			_owner = owner;
		}

		public void Add(TType type, ProcessBase process)
		{
			if (!_nodesDict.TryGetValue(type, out var list))
			{
				list = new List<INodeExecuter>();
				_nodesDict.Add(type, list);
			}

			_processManager.Attach(process, _owner.transform, waitForStart: true);
			list.Add(new ProcessNodeExecuter(process));
		}

		/// <summary>
		/// 一つだけ実行する
		/// </summary>
		public async UniTask UniTaskExecuteOnceAsync(TType type, CancellationToken token)
		{
			if (!_nodesDict.TryGetValue(type, out var list))
			{
				return;
			}

			var node = list[0];

			await node.Execute(token);

			list.RemoveAt(0);
		}


		public async UniTask ExecuteAsync(TType type, CancellationToken token)
		{
			if (!_nodesDict.TryGetValue(type, out var list))
			{
				return;
			}

			while (list.Count != 0)
			{
				var node = list[0];

				await node.Execute(token);

				list.RemoveAt(0);

				// キャンセルされている場合途中で抜ける
				if (token.IsCancellationRequested)
				{
					return;
				}
			}
		}

		public bool Exists(TType type)
		{
			if (!_nodesDict.TryGetValue(type, out var list))
			{
				return false;
			}
			
			return list.Count > 0;
		}



		#endregion


		#region private 関数

		#endregion
	}
}
