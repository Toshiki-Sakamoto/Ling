﻿//
// CustomComponentCollectionCore.cs
// ProductName Ling
//
// Created by  on 2021.08.30
//

using UnityEngine;
using System;
using System.Collections.Generic;
using UniRx;
using System.Linq;

namespace Utility.CustomBehaviour
{
	/// <summary>
	/// 
	/// </summary>
	public class CustomBehaviourCollectionCore
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private Dictionary<Type, List<ICustomComponent>> _components = new Dictionary<Type, List<ICustomComponent>>();
		private List<ICustomBehaviour> _behaviours = new List<ICustomBehaviour>();

		#endregion


		#region プロパティ

		protected CompositeDisposable CompositeDisposable { get; } = new CompositeDisposable();

		public bool IsInitialized { get; private set; }


		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
        /// 機能の登録
        /// </summary>
		public void Register(ICustomBehaviourCollection collection, IEnumerable<ICustomBehaviour> behaviours)
		{
			foreach (var behaviour in behaviours)
            {
				Register(collection, behaviour);
            }
		}

		public void Register(ICustomBehaviourCollection collection, ICustomBehaviour behaviour)
		{
			behaviour.Register(collection);

			_behaviours.Add(behaviour);

			behaviour.AddTo(CompositeDisposable);
		}

		/// <summary>
		/// Component初期化
		/// </summary>
		public void Initialize()
		{
			foreach (var behaviour in _behaviours)
			{
				behaviour.Initialize();
			}

			IsInitialized = true;
		}

		public void AddCustomComponent<T>(T component) where T : ICustomComponent
		{
			var type = typeof(T);

			if (!_components.TryGetValue(type, out var list))
			{
				list = new List<ICustomComponent>();
				_components.Add(type, list);
			}

			list.Add(component);
		}

		public T GetCustomComponent<T>() where T : ICustomComponent
		{
			return GetCustomComponents<T>()?.FirstOrDefault();
		}

		public List<T> GetCustomComponents<T>() where T : ICustomComponent
		{
			var type = typeof(T);
			if (_components.TryGetValue(type, out var list))
			{
				return null;
			}

			return list as List<T>;
		}

		public void Dispose()
        {
			CompositeDisposable.Clear();

			_components.Clear();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
