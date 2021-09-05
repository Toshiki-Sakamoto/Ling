//
// ComponentBase.cs
// ProductName Ling
//
// Created by  on 2021.08.28
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Utility.CustomBehaviour
{
	/// <summary>
	/// Oomponentベース
	/// </summary>
    [System.Serializable]
	public class AbstractCustomBehaviour : MonoBehaviour, ICustomBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public ICustomBehaviourCollection Owner { get; private set; }

		public CompositeDisposable Disposables { get; } = new CompositeDisposable();

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 初期化
		/// </summary>
		public virtual void Initialize()
		{

		}

		public virtual void Register(ICustomBehaviourCollection owner)
		{
			Owner = owner;
		}

		public virtual void Dispose()
		{
			Disposables.Clear();
		}

		#endregion


		#region private 関数

		protected virtual void Awake()
        {
			if (Owner == null)
			{
				Owner = gameObject.GetOrAddComponent<CustomBehaviourCollection>();
				Owner.Register(this);

				Owner.Initialize();
			}
		}

		#endregion
	}
}
