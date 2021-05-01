//
// CharaExpController.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2021.04.29
//

using System;

namespace Ling.Chara.Exp
{
	public interface ICharaExpController
	{
		/// <summary>
		/// レベルアップした時
		/// </summary>
		/// <value></value>
		IObservable<int> OnLvUp { get; }

		/// <summary>
		/// 経験値を追加する
		/// </summary>
		void Add(int exp);
	}
}
