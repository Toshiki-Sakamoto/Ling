//
// PhaseObj.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.05.01
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Utility
{
    public class PhaseArgBase
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class PhaseObj<T> where T : Enum
    {
        #region 定数, class, enum

        public abstract class Base
        {
            public PhaseObj<T> PhaseObj { get; set; }
            public PhaseArgBase Arg { get; set; }

            /// <summary>
            /// 生成されたとき
            /// </summary>
            public virtual void Awake() { }

            public virtual void Init() { }
            public virtual void Proc() { }
            public virtual void Term() { }

            /// <summary>
            /// チェンジ
            /// </summary>
            /// <param name="type">Type.</param>
            public void Change(T type)
            {
                PhaseObj.Change(type, null);
            }
            public void Change(T type, PhaseArgBase arg)
            {
                PhaseObj.Change(type, arg); 
            }
        }

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        private Dictionary<T, Base> _dict = new Dictionary<T, Base>();
        private Base _current = null;

        #endregion


        #region プロパティ

        #endregion


		#region コンストラクタ, デストラクタ

		#endregion


        #region public, protected 関数

        /// <summary>
        /// 追加する
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="instance">Instance.</param>
        public void Add(T type, Base instance)
        {
            _dict[type] = instance; 
        }

        /// <summary>
        /// 開始処理
        /// </summary>
        /// <param name="type">Type.</param>
        public void Start(T type, PhaseArgBase arg = null)
        { 
            foreach(var elm in _dict)
            {
                elm.Value.PhaseObj = this;
                elm.Value.Awake();
            }

            Change(type, arg);
        }

        /// <summary>
        /// 入れ替える
        /// </summary>
        /// <param name="type">Type.</param>
        public void Change(T type, PhaseArgBase arg)
        {
            Base next = null;

            if (!_dict.TryGetValue(type, out next))
            {
                return;
            }

            if (_current != null)
            {
                _current.Term();
            }

            next.Arg = arg;
            next.Init();

            _current = next;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update()
        {
            if (_current != null)
            {
                _current.Proc(); 
            }
        }

        #endregion


        #region private 関数

        #endregion
    }
}
