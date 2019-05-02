//
// Base.cs
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
    public class Creator<T> where T : Base<T>, new()
    {
        public static T Create(Scene scene)
        {
            var instance = new T();
            instance.Scene = scene;
            instance.View = scene.View;

            return instance;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class Base<T> : Utility.PhaseObj<Const.State>.Base
        where T : Base<T>, new()
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        #endregion


        #region プロパティ

        public Scene Scene { get; set; }
        public View View { get; set; }

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数

        #endregion


        #region private 関数

        #endregion
    }
}
