using System.Security.AccessControl;
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
using Ling.Utility;
using Zenject;

namespace Ling.Common
{

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
		public TProcess Attach<TProcess>(bool waitForStart = false) where TProcess : ProcessBase, new() =>
			GetOrCreateNode(OwnerScene, OwnerScene.transform, OwnerScene.DiContainer, autoRemove: true).StartAttach<TProcess>(waitForStart);

		public TProcess Attach<TProcess>(Transform parent, bool autoRemove = true, bool waitForStart = false) where TProcess : ProcessBase, new() =>
			GetOrCreateNode(parent, parent, OwnerScene.DiContainer, autoRemove).StartAttach<TProcess>(waitForStart);

		public TProcess Attach<TProcess>(TProcess process, Transform parent, bool autoRemove = true, bool waitForStart = false) where TProcess : ProcessBase, new() =>
			GetOrCreateNode(parent, parent, OwnerScene.DiContainer, autoRemove).StartAttach(process, waitForStart);

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
			foreach (var elm in removeList)
			{
				RemoveAll(elm.Key);
			}
		}


		#endregion


		#region private 関数

		/// <summary>
		/// KeyのProcessNodeがあれば取得する。
		/// なければ生成する
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private ProcessNode GetOrCreateNode(object key, Transform parent, DiContainer diContainer, bool autoRemove)
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

			// 削除時にRemoveしてもらう
			if (autoRemove)
			{
				parent.gameObject.AddDestroyCallbackIfNeeded(gameObject_ =>
					{
						RemoveAll(gameObject_);
					});
			}

			return instance;
		}


		private void Update()
		{
		}

		#endregion
	}
}

namespace Ling
{
	public static class ProcessExtensions
	{
		/// <summary>
		/// 自分をProcessNodeの親としてアタッチする
		/// </summary>
		public static TProcess AttachProcess<TProcess>(this MonoBehaviour self, bool autoRemove = true, bool waitForStart = false) where TProcess : Common.ProcessBase, new()
		{
			if (Common.ProcessManager.IsNull) return null;
			return Common.ProcessManager.Instance.Attach<TProcess>(self.transform, autoRemove, waitForStart);
		}
		public static TProcess AttachProcess<TProcess>(this MonoBehaviour self, TProcess process, bool autoRemove = true, bool waitForStart = false) where TProcess : Common.ProcessBase, new()
		{
			if (Common.ProcessManager.IsNull) return null;
			return Common.ProcessManager.Instance.Attach(process, self.transform, autoRemove, waitForStart);
		}
	}
}
