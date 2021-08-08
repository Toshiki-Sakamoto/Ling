//
// PrefabMissingCheck.cs
// ProductName Ling
//
// Created by  on 2021.08.08
//

using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;


namespace Ling.Tests.EditMode.Utility
{
	/// <summary>
	/// PrefabのMissigになっている箇所があるかチェックする
	/// </summary>
	public class PrefabMissingCheck
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		/// <summary>
		/// すべてのPrefabのパスを返す
		/// </summary>
		private static IEnumerable<TestCaseData> AllPrefabPaths =>
			Directory.GetFiles("Assets/App", "*.prefab", SearchOption.AllDirectories)
				.Select(path => new TestCaseData(path));

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		[TestCaseSource("AllPrefabPaths")]
		public void PrefabにMissingが存在しないかチェックする(string prefabPath)
		{
			var prefab = PrefabUtility.LoadPrefabContents(prefabPath);

			Assert.That(TryGetMissingNames(prefab, out var propertyNames), Is.False, $"プレハブ {prefabPath} の中にMissingがあります {propertyNames.ElementToString()}");

			PrefabUtility.UnloadPrefabContents(prefab);
		}

		#endregion


		#region private 関数

		private bool TryGetMissingNames(GameObject target, out List<string> propertyNames)
		{
			propertyNames = new List<string>();

			for (int childIndex = 0, count = target.transform.childCount; childIndex < count; ++childIndex)
			{
				TryGetMissingNamesInternal(target.transform.GetChild(childIndex).gameObject, propertyNames);
			}

			return propertyNames.Count > 0;
		}

		private bool TryGetMissingNamesInternal(GameObject target, List<string> propertyNames)
		{
			bool result = false;

			foreach (var component in target.GetComponents<Component>())
			{
				var so = new SerializedObject(component);

				for (var it = so.GetIterator(); it.NextVisible(true);)
				{
					if (it.propertyType != SerializedPropertyType.ObjectReference) continue;

					// 参照が存在しない時
					if (it.objectReferenceValue == null && it.objectReferenceInstanceIDValue != 0)
					{
						propertyNames.Add(it.propertyPath);
						result = true;
					}
				}
			}

			return result;
		}

		#endregion
	}

	public static class ListExtentions
	{
		public static string ElementToString(this IList<string> self)
		{
			var result = string.Empty;

			foreach (var elm in self)
			{
				result += $"{elm}, ";
			}

			return result;
		}

	}
}
