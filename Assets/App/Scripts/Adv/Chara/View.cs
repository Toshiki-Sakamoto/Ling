// 
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.08.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv.Chara
{
    /// <summary>
    /// キャラクタView
    /// </summary>
    public class View : MonoBehaviour
    {
        #region 定数, class, enum

        /// <summary>
        /// 立ち位置
        /// </summary>
        [System.Serializable]
        public class Pos
        {
            [SerializeField] private Transform _trs = null; // 配置場所
            [SerializeField] private Image _img = null;     // 立ち絵
        }

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private Pos _left = null;     // 立ち位置左
        [SerializeField] private Pos _center = null;   // 立ち位置中央
        [SerializeField] private Pos _right = null;    // 立ち位置右

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