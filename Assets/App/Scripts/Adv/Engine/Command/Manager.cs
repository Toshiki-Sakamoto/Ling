//
// Manager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.24
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
    public class Manager
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        private Dictionary<string, System.Func<Lexer, Base>> _dictCreator = new Dictionary<string, Func<Lexer, Base>>();
        private List<Base> _commands = new List<Base>();

        #endregion


        #region プロパティ

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数


        /// <summary>
        /// タイプ登録
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="instance">Instance.</param>
        public void Set(string key, System.Func<Lexer, Base> funcCreator)
        {
            _dictCreator[key] = funcCreator;
        }

        /// <summary>
        /// Keyから作成。
        /// ない場合はNULL
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="key">Key.</param>
        public Base Create(string key, Lexer lexer)
        {
            if (!_dictCreator.ContainsKey(key))
            {
                return null;
            }

            return _dictCreator[key](lexer);
        }

        /// <summary>
        /// コマンドを追加する
        /// </summary>
        /// <param name="command">Command.</param>
        public void AddCommand(Base command)
        {
            _commands.Add(command);
        }



        #endregion


        #region private 関数

        #endregion
    }
}
