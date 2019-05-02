//
// Adv.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.05.01
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
    public class Adv : Base<Adv>
    {
        #region 定数, class, enum

        public class Argment : Utility.PhaseArgBase
        {
            public string AdvFilename { get; set; }
        }


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
            var arg = Arg<Argment>();
            if (arg == null)
            {
                // エラー
                Utility.Log.Error("アドベンチャー開始できない（引数がない）");
                return;
            }

            var filename = arg.AdvFilename;
        }

        public override void Proc()
        {
        }

        public override void Term()
        {
        }

        #endregion


        #region private 関数

        #endregion
    }
}
