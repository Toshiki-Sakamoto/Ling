﻿//
// StartupScene.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2020.04.19
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Startup
{
	/// <summary>
	/// 
	/// </summary>
	public class StartupScene : Common.Scene.Base
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

		#endregion


		#region private 関数

		private void Start()
		{
			_sceneManager.StartScene(Common.Scene.SceneID.Title);
		}

		#endregion
	}
}