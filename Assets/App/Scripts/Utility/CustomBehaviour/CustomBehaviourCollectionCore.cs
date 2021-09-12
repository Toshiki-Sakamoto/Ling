//
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

		//private Dictionary<Type, List<ICustomComponent>> _components = new Dictionary<Type, List<ICustomComponent>>();
		private List<ICustomComponent> _components = new List<ICustomComponent>();
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
		public void Register(ICustomBehaviourCollection collection, IEnumerable<ICustomBehaviour> behaviours, bool isInitialize)
		{
			foreach (var behaviour in behaviours)
            {
				Register(collection, behaviour, isInitialize);
            }
		}

		public void Register(ICustomBehaviourCollection collection, ICustomBehaviour behaviour, bool isInitialize)
		{
			behaviour.Register(collection);

			_behaviours.Add(behaviour);

			behaviour.AddTo(CompositeDisposable);

			if (isInitialize)
			{
				behaviour.Initialize();
			}
		}

		/// <summary>
		/// Component初期化
		/// </summary>
		public void Initialize()
		{
			if (IsInitialized) return;

			IsInitialized = true;

			foreach (var behaviour in _behaviours)
			{
				behaviour.Initialize();
			}

			IsInitialized = true;
		}

		public void AddCustomComponent<T>(T component) where T : ICustomComponent
		{
			_components.Add(component);
#if false
			var type = typeof(T);

			if (!_components.TryGetValue(type, out var list))
			{
				list = new List<ICustomComponent>();
				_components.Add(type, list);
			}

			list.Add(component);
#endif
		}

		public T GetCustomComponent<T>() where T : ICustomComponent
		{
			return GetCustomComponents<T>().FirstOrDefault();
		}

		public IEnumerable<T> GetCustomComponents<T>() where T : ICustomComponent
		{
			return _components
				.Where(component => component is T)
				.Select(component => (T)component);
#if false
			var type = typeof(T);
			if (_components.TryGetValue(type, out var list))
			{
				return null;
			}

			return list as List<T>;
#endif
		}

		public void ForEach<T>(System.Func<T, bool> func) where T : ICustomComponent
		{
			foreach (var component in GetCustomComponents<T>())
			{
				if (func(component)) return;
			}
		}
		public void ForEach<T>(System.Action<T> action) where T : ICustomComponent
		{
			ForEach<T>(elm =>
				{
					action(elm);
					return false;
				});
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
