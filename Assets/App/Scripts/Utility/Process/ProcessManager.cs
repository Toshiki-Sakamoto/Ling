//
// ProcessManager.cs
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
		public TProcess Attach<TProcess>() where TProcess : ProcessBase
		{
			// DIContainerからAddComponentすることでInjectを働かせる
			var process = _diContainer.InstantiateComponent<TProcess>(gameObject);
			process.Node = this;
			process.enabled = false;    // 最初はDisable

			_processes.Add(process);

			return process;
		}

		/// <summary>
		/// ProcessManagerからアタッチされたときに呼び出される
		/// 最初のProcess
		/// </summary>
		/// <typeparam name="TProcess"></typeparam>
		/// <returns></returns>
		public TProcess StartAttach<TProcess>() where TProcess : ProcessBase
		{
			var process = Attach<TProcess>();
			_startProcesses.Add(process);

			return process;
		}

		/// <summary>
		/// リストから削除する
		/// </summary>
		/// <param name="process"></param>
		public void Remove(ProcessBase process)
		{
			_processes.Remove(process);
			_startProcesses.Remove(process);

			// コンポーネントを削除する
			Destroy(process);
		}

		public void Update()
		{
			// 開始されていないプロセスをスタートさせる
			foreach(var process in _startProcesses)
			{
				if (process.enabled) continue;

				// 開始
				process.enabled = true;

				process.ProcessStart();
			}

			_startProcesses.Clear();
		}
	}

	/// <summary>
	/// Process管理者
	/// </summary>
	public class ProcessManager : Utility.MonoSingleton<ProcessManager>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] private DiContainer _diContainer;

		private Dictionary<object, ProcessNode> _processNodes = new Dictionary<object, ProcessNode>();

		#endregion


		#region プロパティ

		/// <summary>
		/// Attach時に何も指定がなかったとき使用されるOwner
		/// シーン切替時に現在のシーンとして設定される
		/// </summary>
		public Common.Scene.Base OwnerScene { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 現在のシーンインスタンスを設定する
		/// </summary>
		/// <param name="ownerScene"></param>
		public void SetupScene(Common.Scene.Base ownerScene)
		{
			OwnerScene = ownerScene;
		}

		/// <summary>
		/// 現在のシーンに対してアタッチする
		/// </summary>
		/// <param name="process"></param>
		public TProcess Attach<TProcess>() where TProcess : ProcessBase =>
			GetOrCreateNode(OwnerScene, OwnerScene.transform, OwnerScene.DiContainer).StartAttach<TProcess>();

		/// <summary>
		/// 指定したobjectのProcessを全て破棄する。
		/// 終了イベントは呼び出されない
		/// </summary>
		public void RemoveAll(object owner)
		{
			if (_processNodes.TryGetValue(owner, out ProcessNode node))
			{
				GameObject.Destroy(node.gameObject);

				_processNodes.Remove(owner);
			}
		}

		/// <summary>
		/// Nodeを一つも持っていないオブジェクトを削除する
		/// シーン終了時に呼び出せばいいかな
		/// </summary>
		public void RemovePureList()
		{
			var removeList = _processNodes.Where(node_ => node_.Value.IsEmpty);
			foreach(var elm in removeList)
			{
				RemoveAll(elm.Key);
			}
		}

		public void Update()
		{
		}

		#endregion


		#region private 関数

		/// <summary>
		/// KeyのProcessNodeがあれば取得する。
		/// なければ生成する
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private ProcessNode GetOrCreateNode(object key, Transform parent, DiContainer diContainer)
		{
			if (key == null) key = this;

			if (_processNodes.TryGetValue(key, out ProcessNode value))
			{
				return value;
			}

			var newGameObject = new GameObject("ProcessNode");
			newGameObject.transform.SetParent(parent);

			var instance = diContainer.InstantiateComponent<ProcessNode>(newGameObject);
			_processNodes[key] = instance;

			return instance;
		}

		#endregion
	}
}
