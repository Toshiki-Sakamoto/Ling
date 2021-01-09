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
using Zenject;

namespace Ling.Common.Scene
{
    public class PhaseArgBase
    {
    }

    /// <summary>
    /// 一つの行動をPhase単位で管理するためのベース
    /// </summary>
    public class PhaseScene<T, TScene> 
		where T : Enum 
		where TScene : Common.Scene.Base
	{
        #region 定数, class, enum

        public abstract class Base
        {
            public PhaseScene<T, TScene> PhaseScene { get; set; }
            public PhaseArgBase Arg { get; set; }
			public TScene Scene { get; set; }
            public DiContainer DiContainer { get; set; }

            /// <summary>
            /// Phaseが開始されたとき
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
                PhaseScene.Change(type, null);
            }
            public void Change(T type, PhaseArgBase arg)
            {
                PhaseScene.Change(type, arg); 
            }

            public TContract Resolve<TContract>() => DiContainer.Resolve<TContract>();
        }

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        private Dictionary<T, Base> _dict = new Dictionary<T, Base>();
        private Base _current = null;
		private TScene _scene = null;

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
        public void Start(TScene scene, T type, PhaseArgBase arg = null)
        {
            _scene = scene;

            foreach (var elm in _dict)
            {
                elm.Value.PhaseScene = this;
                elm.Value.DiContainer = _scene.DiContainer;
                elm.Value.Scene = scene;
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

            _current = next;

            // 最後にInitすることでInit内でCurrentが入れ替わっても問題ないようにする
            next.Init();
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
