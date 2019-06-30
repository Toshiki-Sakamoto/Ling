﻿// 
// MainView.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.04.21.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv.Window
{
    /// <summary>
    /// 
    /// </summary>
    public class MainView : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private AdvText _txtMain = null;
        [SerializeField] private Image _img = null;

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        public void Setup()
        {
            _txtMain.text = "";
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
            // テキストを追加
            Utility.Event.SafeAdd<EventAddText>(this, 
                (ev_) => 
                {
                    _txtMain.text += ev_.Text;
                });

            Utility.Event.SafeAdd<EventSetText>(this,
                (ev_) =>
                {
                    // 

                });

            // Window削除
            Utility.Event.SafeAdd<EventWindowClear>(this,
                (ev_) =>
                {
                    _txtMain.text = ""; 
                });
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