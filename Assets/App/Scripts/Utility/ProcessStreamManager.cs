//
// ProcessStreamManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.30
//

using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
	/// <summary>
	/// 優先度順に処理の流れを管理する
	/// 優先度が0(低いほど高い)に近いものから、終了するまで処理を流していき、すべて終わるまで待機する
	/// </summary>
	public class ProcessStreamManager
	{
		#region 定数, class, enum


		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private SortedDictionary<int, List<ProcessBase>> _processDict = new SortedDictionary<int, List<ProcessBase>>();
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

		/// <summary>
		/// 優先度とともにプロセスを追加する
		/// </summary>
		public void Add(int order, ProcessBase process)
		{
			//var process = _diContainer.Instantiate<TProcess>();
			_processManager.Attach(process, _owner.transform, waitForStart: true);

			if (!_processDict.TryGetValue(order, out var list))
			{
				list = new List<ProcessBase>();
				_processDict.Add(order, list);
			}

			list.Add(process);
		}

		/// <summary>
		/// 処理を実行する。終了するまで待機する
		/// </summary>
		public async UniTask ExecuteAsync()
		{
			// 順に実行する
			foreach (var pair in _processDict)
			{
				var list = pair.Value;

				foreach (var process in list)
				{
					// プロセスごとに待機する
					bool finished = false;

					process.AddAllFinishAction(_ => finished = true);
					process.SetEnable(true);

					await UniTask.WaitUntil(() => finished);
				}

				list.Clear();
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
