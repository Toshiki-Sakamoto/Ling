//
// Common.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.05.13
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
    public class Common
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

        public List<Data.Chara> CreateCharaList(Document document)
        {
            var list = new List<Data.Chara>();

            while (!document.IsEnd)
            {
                var c = document.GetChar();

                
            }

            return list;
        }

        #endregion


        #region private 関数

        #endregion
    }
}
