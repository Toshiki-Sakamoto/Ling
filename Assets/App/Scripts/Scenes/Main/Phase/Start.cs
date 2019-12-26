//
// Start.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.05.02
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Main.Phase
{
	/// <summary>
	/// 
	/// </summary>
    public class Start : Base<Start>
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


        public override void Init()
        {
            var arg = new Adv.Argment();
            arg.AdvFilename = "test.txt";

            Change(Const.State.Adv, arg);
        }

        #endregion


        #region private 関数

        #endregion
    }
}
