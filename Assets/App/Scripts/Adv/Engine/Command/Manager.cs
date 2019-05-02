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

        private Dictionary<string, System.Func<Creator, Lexer, Base>> _dictCreator = new Dictionary<string, Func<Creator, Lexer, Base>>();

        #endregion


        #region プロパティ
        
        public List<Base> Command { get; private set; } = new List<Base>();

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数

        /// <summary>
        /// 初期化する
        /// </summary>
        public void Setup()
        {
            _dictCreator.Clear();
            Command.Clear();
            
            // コマンドを登録する
            SetCmd("set", (c_, l_) => Set.Create(c_, l_));
            SetCmd("calc", (c_, l_) => Set.Create(c_, l_));
            SetCmd("text", (c_, l_) => Text.Create(c_, l_));
            SetCmd("goto", (c_, l_) => Goto.Create(c_, l_));
            SetCmd("if", (c_, l_) => If.Create(c_, l_));
            SetCmd("else", (c_, l_) => Else.Create(c_, l_));
            SetCmd("endif", (c_, l_) => EndIf.Create(c_, l_));
            SetCmd("wait", (c_, l_) => Wait.Create(c_, l_));

            SetCmd("end", (c_, l_) => End.Create(c_, l_));
        }

        /// <summary>
        /// タイプ登録
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="instance">Instance.</param>
        public void SetCmd(string key, System.Func<Creator, Lexer, Base> funcCreator)
        {
            _dictCreator[key] = funcCreator;
        }

        /// <summary>
        /// Keyから作成。
        /// ない場合はNULL
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="key">Key.</param>
        public bool Create(string key, Creator creator, Lexer lexer)
        {
            if (!_dictCreator.ContainsKey(key))
            {
                return false;
            }

            var instance = _dictCreator[key](creator, lexer);
            return true;
        }

        /// <summary>
        /// コマンドを追加する
        /// </summary>
        /// <param name="command">Command.</param>
        public void AddCommand(Base command)
        {
            Command.Add(command);
        }
        /*
        /// <summary>
        /// コマンドを進める
        /// </summary>
        /// <returns>The step.</returns>
        public IEnumerator Step()
        {  
            if (Command.Count == 0)
            {
                yield break; 
            }

            yield return null;
        }*/


        #endregion


        #region private 関数

        #endregion
    }
}
