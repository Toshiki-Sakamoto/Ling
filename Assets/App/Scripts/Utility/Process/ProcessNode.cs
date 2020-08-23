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
		/// 自分のGameObjectにアタッチする
		/// </summary>
		/// <typeparam name="TProcess"></typeparam>
		/// <returns></returns>
		public TProcess Attach<TProcess>() where TProcess : ProcessBase, new()
		{
			var process = new TProcess();
			process.Setup(_diContainer);
			process.Node = this;

			_processes.Add(process);

			return process;
		}

		/// <summary>
		/// ProcessManagerからアタッチされたときに呼び出される
		/// 最初のProcess
		/// </summary>
		/// <typeparam name="TProcess"></typeparam>
		/// <returns></returns>
		public TProcess StartAttach<TProcess>(bool waitForStart = false) where TProcess : ProcessBase, new()
		{
			var process = Attach<TProcess>();
			_startProcesses.Add(process);

			// 開始するか
			if (waitForStart)
			{
				process.SetEnable(true);
			}
			else
			{
				process.SetEnable(false);
			}

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
			foreach (var process in _startProcesses.Where(process_ => process_.IsStarted))
			{
				_startProcesses.Remove(process);
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