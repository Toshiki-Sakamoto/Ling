// 
// ProcessNode.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.23
// 

using UnityEngine;
using System.Collections.Generic;
using Zenject;
using System.Linq;

namespace Ling.Utility
{
	/// <summary>
	/// Processを管理するNode
	/// </summary>
	public class ProcessNode : MonoBehaviour
	{
		[Inject] private DiContainer _diContainer = null;

		private List<ProcessBase> _processes = new List<ProcessBase>();
		private List<ProcessBase> _startProcesses = new List<ProcessBase>();


		public bool IsEmpty => _processes.Count <= 0;

		/// <summary>
		/// 自分にアタッチする
		/// </summary>
		public TProcess Attach<TProcess>() where TProcess : ProcessBase, new() =>
			Attach(new TProcess());

		public TProcess Attach<TProcess>(TProcess process) where TProcess : ProcessBase, new()
		{
			process.Setup(_diContainer);
			process.Node = this;

			_processes.Add(process);

			return process;
		}

		/// <summary>
		/// ProcessManagerからアタッチされたときに呼び出される
		/// 最初のProcess
		/// </summary>
		public TProcess StartAttach<TProcess>(bool waitForStart = false) where TProcess : ProcessBase, new()
		{
			var process = Attach<TProcess>();
			_startProcesses.Add(process);

			// 開始するか
			process.SetEnable(!waitForStart);

			return process;
		}

		public TProcess StartAttach<TProcess>(TProcess process, bool waitForStart = false) where TProcess : ProcessBase, new()
		{
			Attach(process);
			_startProcesses.Add(process);

			// 開始するか
			process.SetEnable(!waitForStart);

			return process;
		}

		/// <summary>
		/// リストから削除する
		/// </summary>
		/// <param name="process"></param>
		public void Remove(ProcessBase process)
		{
			process.SetEnable(false);

			_processes.Remove(process);
			_startProcesses.Remove(process);
		}

		private void Update()
		{
			if (_startProcesses.Count > 0)
			{
				// 開始されていないプロセスをスタートさせる
				foreach(var process in _startProcesses)
				{
					// 有効なプロセスのみ開始させる
					if (process.Enabled)
					{
						process.ProcessStart();
					}
				}

				// 開始済みのProcessは削除する
				var startedProcesses = _startProcesses.Where(process_ => process_.IsStarted).ToArray();
				foreach (var process in startedProcesses)
				{
					_startProcesses.Remove(process);
				}
			}

			// 開始済みのものは更新する
			foreach (var process in _processes)
			{
				if (!process.Enabled) continue;

				process.Update();
			}
		}
	}
}