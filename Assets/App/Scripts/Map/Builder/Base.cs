//
// Base.cs
// ProductName Ling
//
// Created by toshiki sakamoto on 2019.12.22
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Ling.Map.Builder
{
	/// <summary>
	/// 
	/// </summary>
    public abstract class Base
    {
        #region 定数, class, enum

        #endregion


        #region public, protected 変数

        #endregion


        #region private 変数

        protected TileData[] _tileData = null;  // タイル情報
        protected int _rectCount = 0;           // 区画の数

        #endregion


        #region プロパティ

        /// <summary>
        /// 幅
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; private set; }

        #endregion


        #region コンストラクタ, デストラクタ

        #endregion


        #region public, protected 関数

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Initialize(int width, int height)
        {
            Width = width;
            Height = height;

            _tileData = new TileData[width * height];
        }

        /// <summary>
        /// [x, y] から指定したタイル情報を返す
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ref TileData GetTile(int x, int y)
        {
            Utility.Log.Assert(x >= 0 && x <= Width && y >= 0 && y <= Height, "範囲から飛び出してます");

            return ref _tileData[y * Width + x];
        }

        /// <summary>
        /// 処理を実行する
        /// </summary>
        public void Execute()
        {
            // 最初はすべて壁にする

            ExecuteInternal();
        }

        #endregion


        #region private 関数

        protected abstract void ExecuteInternal();

        #endregion
    }
}
