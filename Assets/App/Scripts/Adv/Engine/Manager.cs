//
// Manager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.24
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv.Engine
{
	/// <summary>
	/// 
	/// </summary>
    public class Manager : Utility.Singleton<Manager>
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        #endregion


        #region プロパティ

        /// <summary>
        /// コマンド管理者
        /// </summary>
        /// <value>The command.</value>
        public Command.Manager Cmd { get; private set; } = new Command.Manager();

        #endregion


		#region コンストラクタ, デストラクタ

		#endregion


        #region public, protected 関数

        public void Load(string filename)
        {
            var creator = new Command.Creator(Cmd);
            creator.Read(filename);
        }

        #endregion


        #region private 関数

        #endregion
    }
}
