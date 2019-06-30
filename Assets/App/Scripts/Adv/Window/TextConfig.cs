// 
// TextConfig.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.05.12.
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
    public class TextConfig : MonoBehaviour 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        [SerializeField] private AdvText _text = null;

        #endregion


        #region プロパティ

        public AdvText Text { get { return _text; } }
        public Common Cmn { get; private set; } = new Common();

        #endregion


        #region public, protected 関数

        public void GetUIVertex(List<UIVertex> list)
        {

        }


        public void Process()
        {
            
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