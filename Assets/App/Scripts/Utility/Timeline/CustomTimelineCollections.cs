//
// ICustomTImelineCollections.cs
// ProductName Ling
//
// Created by  on 2021.08.22
//

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utility.Timeline
{
	/// <summary>
	/// <see cref="ICustomTimeline"/>を内部に保持する
	/// </summary>
	public class CustomTimelineCollections : SerializedMonoBehaviour
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private Dictionary<Type, ICustomTimeline> _customs  = default;

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void AddCustom<T>(T customTimeline) where T : ICustomTimeline
		{
			var type = typeof(T);
			if (_customs.ContainsKey(type))
			{
				Utility.Log.Error("二重登録は禁止されています");
				return;
			}

			_customs.Add(type, customTimeline);
		}

		public T GetCustom<T>() where T : ICustomTimeline
		{
			if (!_customs.TryGetValue(typeof(T), out var instance))
			{
				Utility.Log.Error("存在しない");
				return default(T);
			}

			return (T)instance;
		}

		#endregion


		#region private 関数

		#endregion
	}
}
