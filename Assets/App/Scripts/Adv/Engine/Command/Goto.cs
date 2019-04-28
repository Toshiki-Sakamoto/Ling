//
// Goto.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.26
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
    public class Goto : Base
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
        public override ScriptType Type { get { return ScriptType.GOTO_CMD; } }

        public Label Label { get; private set; }

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数

        /// <summary>
        /// コマンド作成
        /// </summary>
        /// <returns>The create.</returns>
        public static Goto Create(Creator creator, Lexer lexer)
        {
            var labeName = lexer.GetString();

            if (string.IsNullOrEmpty(labeName) || !string.IsNullOrEmpty(lexer.GetString()))
            {
                Log.Error("書式がおかしい(goto)");
                return null; 
            }

            var instance = new Goto();
            creator.AddCommand(instance);

            instance.Label = Label.Create(lexer);

            creator.FindLabel(labeName, instance.Label);

            return instance;
        }

        public override string ToString()
        {
            return string.Format("goto labelName:{0}", Label.Name);
        }

        #endregion


        #region private 関数

        #endregion
    }
}
