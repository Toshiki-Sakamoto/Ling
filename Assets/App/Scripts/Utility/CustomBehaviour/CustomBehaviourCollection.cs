//
// ComponentCollection.cs
// ProductName Ling
//
// Created by  on 2021.08.28
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using System.Linq;

namespace Utility.CustomBehaviour
{
	/// <summary>
	/// 集約する
	/// </summary>
	public class CustomBehaviourCollection : MonoBehaviour, ICustomBehaviourCollection
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private CustomBehaviourCollectionCore _core;

		#endregion


		#region プロパティ

		public bool IsInitialized { get; private set; }

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		void ICustomBehaviourCollection.Register(IEnumerable<ICustomBehaviour> behaviours) => _core.Register(this, behaviours);
		void ICustomBehaviourCollection.Register(ICustomBehaviour behaviour) => _core.Register(this, behaviour);
		void ICustomBehaviourCollection.Initialize() => _core.Initialize();

		void ICustomBehaviourCollection.AddCustomComponent<T>(T component) => _core.AddCustomComponent<T>(component);
		T ICustomBehaviourCollection.GetCustomComponent<T>() => _core.GetCustomComponent<T>();
		List<T> ICustomBehaviourCollection.GetCustomComponents<T>() => _core.GetCustomComponents<T>();

		void IDisposable.Dispose() => _core.Dispose();

		#endregion


		#region private 関数

		#endregion
	}
}
