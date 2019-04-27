//
// Start.cs
// ProductName Ling
//
// Created by toshiki on 2019.04.24
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
 

namespace Ling
{
	/// <summary>
	/// 
	/// </summary>
    public class Start : MonoBehaviour
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

        public void Awake()
        {
            Adv.Engine.Manager.Instance.Load("test.txt");
        }

        #endregion
    }
}
