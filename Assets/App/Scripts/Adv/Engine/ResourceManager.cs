//
// ResourceManager.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.09.09
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Adv.Engine
{
	/// <summary>
	/// 
	/// </summary>
    public class ResourceManager
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        private Dictionary<string, Sprite> _dictSprites = new Dictionary<string, Sprite>();

        #endregion


        #region プロパティ

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数

        /// <summary>
        /// Spriteを読み込む
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Sprite LoadSprite(string filename)
        {
            Sprite sprite = null;

            if (!_dictSprites.TryGetValue(filename, out sprite))
            {
                sprite = Resources.Load<Sprite>(filename);
            }

            return sprite;
        }

        #endregion


        #region private 関数

        #endregion
    }
}
