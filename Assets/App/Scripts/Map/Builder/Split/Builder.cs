//
// Builder.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.22
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Map.Builder.Split
{
	/// <summary>
	/// 
	/// </summary>
    public class Builder : Base
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        private ISplittable _splittable = null;     // 部屋の分割担当

        #endregion


        #region プロパティ

        #endregion


        #region コンストラクタ, デストラクタ

        public Builder(ISplittable splittable)
        {
            _splittable = splittable;
        }

        #endregion


        #region public, protected 関数

        /// <summary>
        /// 処理を実行する
        /// </summary>
        protected override void ExecuteInternal()
        {
		}

		#endregion


		#region private 関数

		#endregion
	}
}
