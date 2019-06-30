//
// Chara.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.05.12
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv.Window.Data
{
	/// <summary>
	/// 
	/// </summary>
    public class Chara
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        private int _fontSize = 0;
        private FontStyle _fontStype;
        private CharacterInfo _info; // 

        #endregion


        #region プロパティ

        /// <summary>
        /// 表示文字
        /// </summary>
        /// <value></value>
        public char Char { get; set; }

        public int Width { get; set; }

        public float PosX { get; set; }
        public float PosY { get; set; }
        public float OffsetX { get; set; }
        public float OffsetY { get; set; }

        #endregion


		#region コンストラクタ, デストラクタ

        public Chara(char c, TextConfig config)
        {
            var text = config.Text;

            _fontSize = text.fontSize;
            _fontStype = text.fontStyle;
        }


		#endregion


        #region public, protected 関数

        public bool CreateInfo(Font font)
        {
            if (!font.GetCharacterInfo(Char, out _info, _fontSize, _fontStype))
            {
                return false; 
            }

            Width = _info.advance;

            return true; 
        }

        #endregion


        #region private 関数

        private void Init(char c, int fontSize)
        {
            Char = c;

            _fontSize = fontSize;

            
        }

        #endregion
    }
}
