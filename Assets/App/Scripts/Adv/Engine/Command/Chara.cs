//
// Chara.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.09.09
//

using System;
using System.Collections;
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
    public class Chara : Base
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
        public override ScriptType Type { get { return ScriptType.CHARA_CMD; } }

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数

        public static Chara Create(Creator creator, Lexer lexer)
        {
            var cmn = creator.CmdManager.Cmn;

            var str = creator.Reader.GetString(isAddEnd: false);

            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            var instance = new Chara();

            var texts = cmn.WhiteSpaceParse(str);


            creator.AddCommand(instance);


            return instance;
        }

        /*
        /// <summary>
        /// コマンド実行
        /// </summary>
        public override IEnumerator Process()
        {
            yield break;
        }*/

        #endregion


        #region private 関数

        #endregion
    }
}
