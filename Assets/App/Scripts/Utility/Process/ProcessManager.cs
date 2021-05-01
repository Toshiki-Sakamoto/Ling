//
// ProcessManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.04
//

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;
using Zenject;

namespace Utility
{

	/// <summary>
	/// Process管理者
	/// </summary>
	public class ProcessManager : MonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[Inject] DiContainer _diContainer;

		private Dictionary<object, ProcessNode> _processNodes = new Dictionary<object, ProcessNode>();

		#endregion


		#region プロパティ

		/// <summary>
		/// Attach時に何も指定がなかったとき使用されるOwner
		/// シーン切替時に現在のシーンとして設定される
		/// </summary>
		public GameObject WorldOwner { get; set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// WorldOwnerに対してアタッチする
		/// </summary>
		/// <param name="process"></param>
		public TProcess Attach<TProcess>(bool waitForStart = false) where TProcess : ProcessBase =>
			GetOrCreateNode(WorldOwner, WorldOwner.transform,  autoRemove: true).StartAttach<TProcess>(waitForStart);

		public TProcess Attach<TProcess>(Transform parent, bool autoRemove = true, bool waitForStart = false) where TProcess : ProcessBase =>
			GetOrCreateNode(parent, parent, autoRemove).StartAttach<TProcess>(waitForStart);

		public TProcess Attach<TProcess>(TProcess process, Transform parent, bool autoRemove = true, bool waitForStart = false) where TProcess : ProcessBase =>
			GetOrCreateNode(parent, parent, autoRemove).StartAttach(process, waitForStart);

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
		private ProcessNode GetOrCreateNode(object key, Transform parent, bool autoRemove)
		{
			if (key == null) key = this;

			if (_processNodes.TryGetValue(key, out ProcessNode value))
			{
				return value;
			}

			var newGameObject = new GameObject("ProcessNode");
			newGameObject.transform.SetParent(parent);

			var instance = _diContainer.InstantiateComponent<ProcessNode>(newGameObject);
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

		private void Awake()
		{		
			WorldOwner = gameObject;
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
#if false
		/// <summary>
		/// 自分をProcessNodeの親としてアタッチする
		/// </summary>
		public static TProcess AttachProcess<TProcess>(this MonoBehaviour self, bool autoRemove = true, bool waitForStart = false) where TProcess : Utility.ProcessBase
		{
			if (Utility.ProcessManager.IsNull) return null;
			return Utility.ProcessManager.Instance.Attach<TProcess>(self.transform, autoRemove, waitForStart);
		}
		public static TProcess AttachProcess<TProcess>(this MonoBehaviour self, TProcess process, bool autoRemove = true, bool waitForStart = false) where TProcess : Utility.ProcessBase
		{
			if (Utility.ProcessManager.IsNull) return null;
			return Utility.ProcessManager.Instance.Attach(process, self.transform, autoRemove, waitForStart);
		}
#endif
	}
}
