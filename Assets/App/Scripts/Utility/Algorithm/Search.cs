// 
// SearchAlgorithm.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2020.08.14
// 

using UnityEngine;

namespace Ling.Utility.Algorithm
{
	/// <summary>
	/// 探索系Algorithm
	/// </summary>
	public class Search : Utility.MonoSingleton<Search>
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public Astar Astar { get; } = new Astar();

		#endregion


		#region public, protected 関数

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		/// <summary>
		/// 初期処理
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			Astar.Setup();
		}

		#endregion
	}
}