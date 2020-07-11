//
// MasterRepository.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.07.11
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Ling.MasterData
{
	/// <summary>
	/// 指定したMasterを配列で保持する
	/// </summary>
	public class MasterRepository<T> where T : MasterBase<T>
    {
		#region 定数, class, enum

		#endregion


		#region public, protected 変数

		#endregion


		#region private 変数

		private List<T> _entities = new List<T>();

		#endregion


		#region プロパティ

		#endregion


		#region コンストラクタ, デストラクタ

		#endregion


		#region public, protected 関数

		public void Add(T master)
		{
			_entities.Add(master);
		}

		public void Clear()
		{
			_entities.Clear();
		}

		#endregion


		#region private 関数

		#endregion
	}
}
