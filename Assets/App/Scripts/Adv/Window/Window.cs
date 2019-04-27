// 
// Window.cs  
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
    public class Window : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private MainView _main = null;
        [SerializeField] private MenuView _menu = null;
        [SerializeField] private NameView _name = null;

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

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
        }

        #endregion
    }
}