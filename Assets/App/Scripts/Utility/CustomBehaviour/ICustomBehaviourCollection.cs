//
// ComponentCollection.cs
// ProductName Ling
//
// Created by  on 2021.08.28
//

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utility.CustomBehaviour
{
	/// <summary>
	/// <see cref="ICustomBehaviour"/>
	/// </summary>
	public interface ICustomBehaviourCollection : IDisposable
	{
		void Initialize();
		void Register(IEnumerable<ICustomBehaviour> behaviours);
		void Register(ICustomBehaviour behaviour);

		void AddCustomComponent<T>(T component) where T : ICustomComponent;
		T GetCustomComponent<T>() where T : ICustomComponent;
		IEnumerable<T> GetCustomComponents<T>() where T : ICustomComponent;

		void ForEach<T>(System.Func<T, bool> func) where T : ICustomComponent;
		void ForEach<T>(System.Action<T> func) where T : ICustomComponent;
	}
}
