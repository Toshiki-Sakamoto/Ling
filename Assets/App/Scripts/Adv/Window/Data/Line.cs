//
// Line.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.05.12
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv.Window.Data
{
	/// <summary>
	/// 
	/// </summary>
    public class Line
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        #endregion


        #region プロパティ

        /// 保持する文字
        public List<Chara> CharaLists { get; private set; } = new List<Chara>();

        /// 行の幅
        public float Width { get; private set; }

        #endregion


		#region コンストラクタ, デストラクタ

		#endregion


        #region public, protected 関数

        public void Calc()
        {
            float right = 0.0f, left = 0.0f;

            foreach (var elm in CharaLists)
            {
                
            }

            Width = Mathf.Abs(right - left);
        }

        #endregion


        #region private 関数

        #endregion
    }
}
