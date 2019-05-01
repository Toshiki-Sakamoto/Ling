// 
// View.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.04.30.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Startup
{
    /// <summary>
    /// 
    /// </summary>
    public class View : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private Button _btnBack = null;

        #endregion


        #region プロパティ

        public System.Action ActOnClick { get; set; }

        #endregion


        #region public, protected 関数

        /// <summary>
        /// クリック処理
        /// </summary>
        public void OnClick()
        { 
            if (ActOnClick != null)
            {
                ActOnClick();
            }
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
        }

        #endregion
    }
}