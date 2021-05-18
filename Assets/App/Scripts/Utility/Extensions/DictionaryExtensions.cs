//
// DictionaryExtensions.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.05.10
//

using System.Linq;
using System.Collections.Generic;

namespace Utility.Extensions
{
	/// <summary>
	/// Dictionaryの拡張機能
	/// </summary>
	public static class DictionaryExtensions
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// ランダムにValueを返す
		/// </summary>
		public static TValue GetRandomValue<TKey, TValue>(this Dictionary<TKey, TValue> self)
		{
			return GetRandomPair(self).Value;
		}

		public static TKey GetRandomKey<TKey, TValue>(this Dictionary<TKey, TValue> self)
		{
			return GetRandomPair(self).Key;
		}

		public static KeyValuePair<TKey, TValue> GetRandomPair<TKey, TValue>(this Dictionary<TKey, TValue> self)
		{
			return self.ElementAt(Utility.Random.Range(self.Count));
		}

		#endregion


		#region private 関数

		#endregion
	}
}
