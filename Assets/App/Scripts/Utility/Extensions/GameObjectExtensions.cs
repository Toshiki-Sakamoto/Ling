//
// GameObjectExtensions.cs
// ProductName Ling
//
// Created by  on 2021.08.30
//

using UnityEngine;

namespace Utility
{
	/// <summary>
	/// GameObject拡張機能
	/// </summary>
	public static class GameObjectExtensions
	{
		#region public, protected 関数

		public static T GetOrAddComponent<T>(this GameObject self) where T : Component
		{
			var result = self.GetComponent<T>();
			if (result != null) return result;

			return self.AddComponent<T>();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
