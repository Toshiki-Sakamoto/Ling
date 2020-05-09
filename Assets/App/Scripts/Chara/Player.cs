// 
// Player.cs  
// ProductName Ling
//  
// Create by toshiki sakamoto on 2019.09.22.
// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;


namespace Ling.Chara
{
    /// <summary>
    /// 
    /// </summary>
    public class Player : Base 
    {
        #region 定数, class, enum

        #endregion


        #region public 変数

        #endregion


        #region private 変数

        #endregion


        #region プロパティ


        #endregion


        #region public, protected 関数

        /// <summary>
        /// 現在位置から指定した数を足して移動する
        /// </summary>
        /// <param name="addCellPos"></param>
        /// <param name="moveFinish"></param>
        public System.IObservable<Unit> MoveByAddPos(Vector3Int addCellPos) =>
            MoveController.SetMoveCellPos(CellPos + addCellPos);

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