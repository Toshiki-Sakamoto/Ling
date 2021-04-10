using System;
// 
// MenuModel.cs  
// ProductName Ling
//  
// Created by toshiki sakamoto on 2021.04.09
// 

using UnityEngine;

namespace Ling.Scenes.Menu
{
	/// <summary>
	/// Menum Model
	/// </summary>
	public class MenuModel : MonoBehaviour 
	{
		#region 定数, class, enum

		#endregion


		#region public 変数

		#endregion


		#region private 変数

		#endregion


		#region プロパティ

		public MenuDefine.Type Type { get; private set; }

		#endregion


		#region public, protected 関数

		public void SetArgument(MenuArgument argument)
		{
			Type = argument.Type;
		}

		#endregion


		#region private 関数

		#endregion


		#region MonoBegaviour

		#endregion
	}
}