// 
// UtilityInitializer.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.17
// 

using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Utility
{
	/// <summary>
	/// Utility関連の初期化を行う
	/// </summary>
	public class UtilityInitializer : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region public, protected 関数

		/// <summary>
		/// 必要なデータの読み込みを非同期で行う
		/// </summary>
		public async UniTask LoadAllAsync()
		{
			// Canvasが持つSortOrderを変更する
			var task1 = UI.SortOrderSettings.LoadAsync(this);

			await UniTask.WhenAll(task1);
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		void Awake()
		{
		}

		#endregion
	}
}