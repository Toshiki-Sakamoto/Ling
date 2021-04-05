//
// RoadData.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.05.20
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.Map.Builder.Split
{
	/// <summary>
	/// 道情報
	/// </summary>
	public class RoadData
	{
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		public List<Vector2Int> pos = new List<Vector2Int>();    // 道の座標

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数
		public void Add(Vector2Int pos) =>
					this.pos.Add(pos);

		public void All(System.Action<Vector2Int> action)
		{
			foreach (var pos in this.pos)
			{
				action?.Invoke(pos);
			}
		}

		#endregion


		#region private 関数

		#endregion
	}
}
