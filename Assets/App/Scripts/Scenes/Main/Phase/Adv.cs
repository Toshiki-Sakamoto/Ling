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
#if false
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

        Ling.Adv.Engine.Manager _advManager = null;

        #endregion


        #region プロパティ

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数

        public override void Awake()
        {
        }

        public override void Init()
        {
            _advManager = Ling.Adv.Engine.Manager.Instance;

            var arg = Arg as Argment;
            if (arg == null)
            {
                // エラー
                Utility.Log.Error("アドベンチャー開始できない（引数がない）");
                return;
            }

            var filename = arg.AdvFilename;

            Utility.Log.Print("アドベンチャー開始 {0}", filename);

            _advManager.Load(filename);
            _advManager.AdvStart();
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
#endif
}
