//
// ConfigInfo.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.05.16
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv.Window
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigInfo
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        #endregion


        #region プロパティ

        public TextConfig Config { get; private set; }
        public List<Data.Chara> CharaList { get; set; } = new List<Data.Chara>();


        #endregion


        #region コンストラクタ, デストラクタ

        public ConfigInfo(TextConfig config)
        {
            Config = config;
        }

        #endregion


        #region public, protected 関数

        public void BuildString(Document document)
        {
            CreateList(document);
        }

        public void BuildTextArea(RectTransform rectTransform)
        {
            // 描画範囲
            var rect = rectTransform.rect;
            float maxW = Mathf.Abs(rect.width);
            float maxH = Mathf.Abs(rect.height);

            // 文字のX座標
        }

        #endregion


        #region private 関数

        /// <summary>
        /// 一文字からひとつクラスを作成する
        /// </summary>
        /// <param name="document">Document.</param>
        private void CreateList(Document document)
        {
            CharaList.Clear();

            while (!document.IsEnd)
            {
                var chara = new Data.Chara(document.GetChar(), Config);

                CharaList.Add(chara);
            }
        }

        /// <summary>
        /// 幅を求める
        /// </summary>
        private void CalcXPosition(float maxWidth)
        { 
        }

        #endregion
    }
}
