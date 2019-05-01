//
// Phase.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.04.30
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class Phase<T> where T : Enum
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        private object _instance = null;
        private Type _type;
        private Action[] _inits = null;
        private Action[] _procs = null;
        private Action[] _terms = null;

        #endregion


        #region プロパティ

        public int Current { get; private set; }
        public int Prev { get; private set; }

        #endregion


        #region コンストラクタ, デストラクタ

        private Phase(object instance, T type, int init)
        {
            _instance = instance;
            _type = typeof(T);
            Prev = -1;
            Current = init;

            var ids = Enum.GetValues(_type);
            _inits = new Action[ids.Length];
            _procs = new Action[ids.Length];
            _terms = new Action[ids.Length];

            var prefix = _type.Name;

            foreach(int elm in ids)
            {
                var name = prefix + GetName(elm);
                _inits[(int)elm] = CreateFunc(name + "Init");
                _procs[(int)elm] = CreateFunc(name + "Proc");
                _terms[(int)elm] = CreateFunc(name + "Term");
            }
        }

		#endregion


        #region public, protected 関数

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="instance">Instance.</param>
        /// <param name="init">Init.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static Phase<T> Create(object instance, T init)
        {
            return new Phase<T>(instance, init, (int)(ValueType)init);
        }


        /// <summary>
        /// 基本的にはすぐに切り替えたほうが良いきがする
        /// </summary>
        /// <param name="id">Identifier.</param>
        public void Set(T id)
        {
            Current = (int)(ValueType)id;
            Change(); 
        }

        /// <summary>
        /// ステータスを切り替える
        /// </summary>
        public void Change()
        {
            while (Current != Prev)
            {
                Term();
                Prev = -1;

                Init();
                Prev = Current;
            }
        }


        public void Update()
        {
            Proc();
        }

        #endregion


        #region private 関数

        /// <summary>
        /// リフレクションを指定して関数を作成。それをActionにする
        /// </summary>
        /// <returns>The func.</returns>
        /// <param name="name">Name.</param>
        private Action CreateFunc(string name)
        {
            return (Action)Delegate.CreateDelegate(typeof(Action), _instance, name);
        }

        /// <summary>
        /// 列挙の名前を作成する
        /// </summary>
        /// <returns>The name.</returns>
        /// <param name="id">Identifier.</param>
        private string GetName(int id)
        {
            return Enum.GetName(_type, id); 
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Init()
        { 
            if (Current < 0)
            {
                return; 
            }

            if (_inits[Current] == null)
            {
                return;
            }

            _inits[Current]();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        private void Proc()
        { 
            if (_procs[Current] == null)
            {
                return; 
            }

            _procs[Current]();
        }


        /// <summary>
        /// 終了処理
        /// </summary>
        private void Term()
        {
            if (Prev < 0)
            {
                return;
            }

            if (_terms[Prev] == null)
            {
                return;
            }

            _terms[Prev]();
        }


        #endregion
    }
}
