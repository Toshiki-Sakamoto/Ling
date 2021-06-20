//
// ValueObject.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.29
//

using UnityEngine;

namespace Utility
{
	/// <summary>
	/// 値をラップする
	/// </summary>
	[System.Serializable]
	public class ValueObject<T>
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		[SerializeField] private T _value = default;

		#endregion


		#region プロパティ

		public T Value { get => _value; set { _value = value; } }

		#endregion


		#region コンストラクタ, デストラクタ

		public ValueObject()
		{
		}

		public ValueObject(T value)
		{
			_value = value;
		}

		#endregion


		#region public, protected 関数
		

		#endregion


		#region private 関数

		#endregion
	}
}
