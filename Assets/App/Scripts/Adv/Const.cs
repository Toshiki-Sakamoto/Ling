//
// Const.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.21
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv
{
	/// <summary>
	/// 
	/// </summary>
    public class Const
    {
        #region 定数, class, enum

        public const string PrefabPath = "Prefabs/Adv/";
        public const string ImagePath = "Images/Adv/";

        public const char Dash = '—';


        /// <summary>
        /// キャラクタの立ち位置
        /// </summary>
        public enum CharaPos
        {
            None,
            Left,
            Center,
            Right,
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

        public static CharaPos CharaPosFromStr(string str)
        {
            switch(str)
            {
                case "Left":
                    return CharaPos.Left;

                case "Center":
                    return CharaPos.Center;

                case "Right":
                    return CharaPos.Right;

                default:
                    return CharaPos.None;
            }
        }

        #endregion


        #region private 関数

        #endregion
    }
}
