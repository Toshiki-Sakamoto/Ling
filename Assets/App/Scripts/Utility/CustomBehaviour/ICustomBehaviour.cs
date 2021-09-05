//
// IComponent.cs
// ProductName Ling
//
// Created by  on 2021.08.28
//

using System;

namespace Utility.CustomBehaviour
{
	/// <summary>
	/// コンポーネント基礎インターフェイス
	/// </summary>
	public interface ICustomBehaviour : IDisposable
	{
		void Initialize();

		void Register(ICustomBehaviourCollection owner);
	}
}
