﻿// 
// View.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.04.30.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv
{
    /// <summary>
    /// 
    /// </summary>
    public class View : Utility.ObjCreator<View> 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private Window.View _window = null;
        [SerializeField] private Select.View _select = null;

        private System.Action<int> _actOnSelect = null;

        #endregion


        #region プロパティ

        public Window.View Win { get { return _window; } }

        #endregion


        #region public, protected 関数

        public static string PrefabName()
        {
            return Common.GetResourcePath("AdvMain");
        }

        public static bool IsAwakeActive()
        {
            return false; 
        }


        public override void Setup()
        {
            _window.Setup();
            _select.Setup();

            // 選択肢を出す
            Utility.Event.SafeAdd<Select.EventSelect>(this, 
                (ev_) => 
                {
                    _select.Show(ev_.SelectList);
                    _actOnSelect = ev_.ActOnSelect;
                });

            // 選択肢が選ばれた
            Utility.Event.SafeAdd<Select.EventSelected>(this,
                (ev_) => 
                {
                    _select.Hide();

                    _actOnSelect?.Invoke(ev_.SelectIndex);
                    _actOnSelect = null;
                });
        }

        #endregion


        #region private 関数

        #endregion


        #region MonoBegaviour

        /// <summary>
        /// 初期処理
        /// </summary>
        void Awake()
        {
        }

        /// <summary>
        /// 更新前処理
        /// </summary>
        void Start()
        {
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        void OnDestoroy()
        {
            Utility.Event.SafeAllRemove(this);
        }

        #endregion
    }
}