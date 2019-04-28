//
// End.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.28
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv.Engine.Command
{
	/// <summary>
	/// 
	/// </summary>
    public class End : Base
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        #endregion


        #region プロパティ
        /// <summary>
        /// コマンドタイプ
        /// </summary>
        /// <value>The type.</value>
        public override ScriptType Type { get { return ScriptType.END_CMD; } }

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数

        /// <summary>
        /// コマンド作成
        /// </summary>
        /// <returns>The create.</returns>
        public static End Create(Creator creator, Lexer lexer)
        {
            var instance = new End();

            creator.AddCommand(instance);

            return instance;
        }

        public override string ToString()
        {
            return "end";
        }

        #endregion


        #region private 関数

        #endregion
    }
}
