// 
// AdvText.cs  
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
    [RequireComponent(typeof(TextConfig))]
    public class AdvText : Text 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        private TextConfig _config = null;

        #endregion


        #region プロパティ

        #endregion


        #region public, protected 関数

        ///
        /// 描画するために頂点情報を生成するときに呼び出される
        ///
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            // 描画する範囲を格納する
        }

        public void SetDocument(/*Common.Document document*/)
        {
            //_config.Cmn
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
            _config = GetComponent<TextConfig>();
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