//
// Text.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.26
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
    public class Text : Base
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
        public override ScriptType Type { get { return ScriptType.TEXT_CMD; } }

        public string Message { get; private set; }

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数

        public static Text Create(Creator creator, Lexer lexer)
        {
            if (!lexer.IsNextStringNullOrEmpty())
            {
                Log.Error("構文エラー(text)");
                return null;
            }

            var instance = new Text();

            string work = string.Empty;

            do
            {
                // 一行読み取る
                string str = creator.Reader.GetString();

                if (string.IsNullOrEmpty(str))
                {
                    break; 
                }

                // 何もなければ
                if (str[0] == '\0')
                {
                    break; 
                }

                work += str;
                work += '\n';

            } while (true);

            instance.Message = work;


            creator.AddCommand(instance);

            return instance;
        }

        /// <summary>
        /// コマンド実行
        /// </summary>
        public override IEnumerator Process() 
        {
            // Windowをクリア
            EventManager.SafeTrigger<EventWindowClear>();

            // 速度によって送るスピードが変わる

            yield break;
        }

        #endregion


        #region private 関数

        #endregion
    }
}
