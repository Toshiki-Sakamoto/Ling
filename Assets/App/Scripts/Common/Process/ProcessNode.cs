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

namespace Ling.Common
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
		public TProcess Attach<TProcess>() where TProcess : ProcessBase =>
			Attach(_diContainer.Instantiate<TProcess>());

		public TProcess Attach<TProcess>(TProcess process) where TProcess : ProcessBase
		{
			process.Node = this;

			_processes.Add(process);

			return process;
		}

		/// <summary>
		/// ProcessManagerからアタッチされたときに呼び出される
		/// 最初のProcess
		/// </summary>
		public TProcess StartAttach<TProcess>(bool waitForStart = false) where TProcess : ProcessBase
		{
			var process = Attach<TProcess>();
			_startProcesses.Add(process);

			// 開始するか
			process.SetEnable(!waitForStart);

			return process;
		}

		public TProcess StartAttach<TProcess>(TProcess process, bool waitForStart = false) where TProcess : ProcessBase
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
				var notStaredProcesses = _startProcesses.Where(process_ => !process_.IsStarted);
				foreach (var process in notStaredProcesses)
				{
					// 有効なプロセスのみ開始させる
					if (process.Enabled)
					{
						process.ProcessStart();
					}
				}

				// 開始済みのProcessは削除する
				for (int i = 0; i < _startProcesses.Count;)
				{
					var process = _startProcesses[i];
					if (!process.IsStarted)
					{
						++i;
						continue;
					}

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